using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStatsManager : CharacterStatsManager
{
    private PlayerManager player;
    public StaminaBar staminaBar;
    public HealthBar healthBar;
    public FocusPointBar focusPointBar;

    [Header("Stamina Regeneration")]
    [SerializeField] private float staminaRegenerationTimer = 0.5f;
    [SerializeField] float staminaRegenerationAmount;

    protected override void Awake()
    {
        base.Awake();
        player = GetComponent<PlayerManager>();
        healthBar = FindAnyObjectByType<HealthBar>();
        staminaBar = FindAnyObjectByType<StaminaBar>();
        focusPointBar = FindAnyObjectByType<FocusPointBar>();
    }

    private void Start()
    {
        maxHealth = SetMaxHealthFromHealthLevel();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        healthBar.SetCurrentHealth(currentHealth);

        maxStamina = SetMaxStaminaFromEnduranceLevel();
        currentStamina = maxStamina;
        staminaBar.SetMaxStamina(maxStamina);
        staminaBar.SetCurrentStamina(currentStamina);

        maxFocusPoint = SetMaxFocusFromFocusLevel();
        currentFocusPoint = maxFocusPoint;
        focusPointBar.SetMaxFocusPoint(maxFocusPoint);
        focusPointBar.SetCurrentFocusPoint(currentFocusPoint);
    }

    public override void HandlePoiseResetTimer()
    {
        if (poiseResetTimer > 0)
        {
            poiseResetTimer -= Time.deltaTime;
        }
        else if (poiseResetTimer <= 0 && !player.isPerformingAction)
        {
            totalPoiseDefence = armorPoiseBonus;
        }
    }

    public void RefreshHUD()
    {
        healthBar.gameObject.SetActive(false);
        healthBar.gameObject.SetActive(true);
        focusPointBar.gameObject.SetActive(false);
        focusPointBar.gameObject.SetActive(true);
        staminaBar.gameObject.SetActive(false);
        staminaBar.gameObject.SetActive(true);
    }

    public override void TakeDamageNoAnimation(int physicalDamage, int fireDamage)
    {
        if (player.isInvulnerable)
            return;

        base.TakeDamageNoAnimation(physicalDamage, fireDamage);
        player.PlaySFX(player.feedBackManager.hitSFX);

        healthBar.SetCurrentHealth(currentHealth);

        if (currentHealth <= 0)
        {
            StartCoroutine(ProcessDeathEvent());
        }
    }

    public override void TakeDamage(int physicalDamage, int fireDamage, string damageAnimation)
    {
        if (player.isInvulnerable)
            return;

        base.TakeDamage(physicalDamage, fireDamage, damageAnimation);

        if (player.isBlocking)
        {
            player.PlaySFX(player.feedBackManager.blockSFX);
        }
        else
        {
            player.PlaySFX(player.feedBackManager.hitSFX);
        }


        healthBar.SetCurrentHealth(currentHealth);
        player.playerAnimationManager.PlayTargetActionAnimation(damageAnimation, true);

        if (currentHealth <= 0)
        {
            StartCoroutine(ProcessDeathEvent());
        }
    }

    public void TakeStaminaCost(float cost)
    {
        currentStamina = currentStamina - cost;
        staminaBar.SetCurrentStamina(currentStamina);
    }

    public void RegenerateStamina()
    {
        if (player.isPerformingAction || player.playerLocomotion.isSprinting || player.isJumping)
        {
            staminaRegenerationTimer = 1f;
            return;
        }

        staminaRegenerationTimer -= Time.deltaTime;

        if(staminaRegenerationTimer < 0)
        {
            staminaRegenerationTimer = 0;

            if (currentStamina < maxStamina && staminaRegenerationTimer == 0)
            {
                currentStamina += staminaRegenerationAmount * Time.deltaTime;
                staminaBar.SetCurrentStamina(currentStamina);
            }
        } 
    }

    public IEnumerator ProcessDeathEvent(bool manuallySelectDeathAnimation = false)
    {
        PlayerUIManager.instance.playerUIPopUPManager.SendYouDiedPopUp();

        currentHealth = 0;
        player.isDead = true;
        player.PlaySFX(player.feedBackManager.deadSFX);
        AudioManager.instance.StopBGM();

        if (!manuallySelectDeathAnimation)
        {
            player.playerAnimationManager.PlayTargetActionAnimation("Dead_01", true);
        }

        yield return new WaitForSeconds(5);
        WorldSaveGameManager.instance.LoadGame();
    }

    public void HealPlayer(int healAmount)
    {
        currentHealth += healAmount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        healthBar.SetCurrentHealth(currentHealth);
    }

    public void RestoreFocusPointPlayer(int restoreFpAmount)
    {
        currentFocusPoint += restoreFpAmount;
        if (currentFocusPoint > maxFocusPoint)
        {
            currentFocusPoint = maxFocusPoint;
        }

        focusPointBar.SetCurrentFocusPoint(currentFocusPoint);
    }

    public void DeductFocusPoints(int focusPoints)
    {
        currentFocusPoint -= focusPoints;
        if (currentFocusPoint < 0)
        {
            currentFocusPoint = 0;
        }

        focusPointBar.SetCurrentFocusPoint(currentFocusPoint);
    }

    public void AddSouls(int souls)
    {
        currentBlood += souls;
    }

}
