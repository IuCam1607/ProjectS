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
    [SerializeField] private float staminaRegenerationTimer = 1f;
    [SerializeField] float staminaRegenerationAmount;

    private void Awake()
    {
        playerManager = GetComponent<PlayerManager>();
        staminaBar = FindAnyObjectByType<StaminaBar>();
        healthBar = FindAnyObjectByType<HealthBar>();
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
        focusPointBar.SetCurrentStamina(currentFocusPoint);
    }

    public void RefreshHUD()
    {
        healthBar.gameObject.SetActive(false);
        healthBar.gameObject.SetActive(true);
        staminaBar.gameObject.SetActive(false);
        staminaBar.gameObject.SetActive(true);
    }

    private float SetMaxHealthFromHealthLevel()
    {
        maxHealth = vitalityLevel * 10;
        return maxHealth;
    }

    private float SetMaxStaminaFromEnduranceLevel()
    {
        maxStamina = enduranceLevel * 10;
        return maxStamina;
    }

    private float SetMaxFocusFromFocusLevel()
    {
        maxFocusPoint = focusLevel * 10;
        return maxFocusPoint;
    }

    public void TakeDamageNoAnimation(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            isDead = true;
        }
    }

    public void TakeDamage(int damage)
    {
        if (playerManager.isInvulnerable)
            return;

        if (isDead)
            return;

        currentHealth -= damage;
        healthBar.SetCurrentHealth(currentHealth);

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

    public void HealPlay(int healAmount)
    {
        currentHealth += healAmount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        healthBar.SetCurrentHealth(currentHealth);
    }

    public void DeductFocusPoints(int focusPoints)
    {
        currentFocusPoint -= focusPoints;
        if (currentFocusPoint < 0)
        {
            currentFocusPoint = 0;
        }

        focusPointBar.SetCurrentStamina(currentFocusPoint);
    }

    public void AddSouls(int souls)
    {
        soulCount += souls;
    }
}
