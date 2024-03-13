using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStatsManager : CharacterStatsManager
{
    private PlayerManager playerManager;
    public StaminaBar staminaBar;
    public HealthBar healthBar;
    public FocusPointBar focusPointBar;

    [Header("Stamina Regeneration")]
    [SerializeField] private float staminaRegenerationTimer = 0.5f;
    [SerializeField] float staminaRegenerationAmount;

    protected override void Awake()
    {
        base.Awake();
        playerManager = GetComponent<PlayerManager>();
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
        else if (poiseResetTimer <= 0 && !playerManager.isPerformingAction)
        {
            totalPoiseDefence = armorPoiseBonus;
        }
    }

    public void RefreshHUD()
    {
        healthBar.gameObject.SetActive(false);
        healthBar.gameObject.SetActive(true);
        staminaBar.gameObject.SetActive(false);
        staminaBar.gameObject.SetActive(true);
    }

    public override void TakeDamageNoAnimation(int physicalDamage, int fireDamage)
    {
        if (playerManager.isInvulnerable)
            return;

        base.TakeDamageNoAnimation(physicalDamage, fireDamage);

        healthBar.SetCurrentHealth(currentHealth);

        if (currentHealth <= 0)
        {
            StartCoroutine(ProcessDeathEvent());
        }
    }

    public override void TakeDamage(int physicalDamage, int fireDamage, string damageAnimation)
    {
        if (playerManager.isInvulnerable)
            return;

        base.TakeDamage(physicalDamage, fireDamage, damageAnimation);

        healthBar.SetCurrentHealth(currentHealth);
        Debug.Log("Player Health: " + currentHealth);
        playerManager.playerAnimationManager.PlayTargetActionAnimation(damageAnimation, true);

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
        if (playerManager.isPerformingAction || playerManager.playerLocomotion.isSprinting || playerManager.isJumping)
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
        playerManager.playerStatsManager.isDead = true;

        if (!manuallySelectDeathAnimation)
        {
            playerManager.playerAnimationManager.PlayTargetActionAnimation("Dead_01", true);
        }

        yield return new WaitForSeconds(5);
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
