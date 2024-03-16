using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatsManager : MonoBehaviour
{
    CharacterManager characterManager;

    [Header("Team I.D")]
    public string characterName;
    public int teamIDNumber = 0;

    [Header("Character Levels")]
    public int playerLevel = 1;

    [Header("Stats Levels")]
    public int vitalityLevel = 10;
    public int focusLevel = 10;
    public int enduranceLevel = 10;
    public int poiseLevel = 10;
    public int strengthLevel = 10;
    public int dexterityLevel = 10;
    public int intelligenceLevel = 10;
    public int faithLevel = 10;

    [Header("Stats")]
    public float currentHealth;
    public float maxHealth;

    public float currentFocusPoint;
    public float maxFocusPoint;

    public float currentStamina;
    public float maxStamina;

    [Header("Poise")]
    public float totalPoiseDefence;
    public float offensivePoiseBonus;
    public float armorPoiseBonus;
    public float totalPoiseResetTime = 10;
    public float poiseResetTimer = 0;

    [Header("Armor Absorptions")]
    public float physicalDamageAbsorptionHead;
    public float fireDamageAbsorptionHead;

    [Header("Blood Point")]
    public int currentBlood = 0;

    protected virtual void Awake()
    {
        characterManager = GetComponent<CharacterManager>();
    }

    private void Start()
    {
        totalPoiseDefence = armorPoiseBonus;
    }

    public virtual void Update()
    {
        HandlePoiseResetTimer();
    }

    public virtual void TakeDamageNoAnimation(int physicalDamage, int fireDamage)
    {
        if (characterManager.isDead)
        {
            return;
        }

        float totalPhysicalDamageAbsorption = 1 - (1 - physicalDamageAbsorptionHead / 50);
        float totalFireDamageAbsorption = 1 - (1 - fireDamageAbsorptionHead / 50);

        physicalDamage = Mathf.RoundToInt(physicalDamage - (physicalDamage * totalPhysicalDamageAbsorption));
        fireDamage = Mathf.RoundToInt(fireDamage - (fireDamage * totalFireDamageAbsorption));

        float finalDamage = physicalDamage + fireDamage;
        currentHealth -= finalDamage;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            characterManager.isDead = true;
        }
    }

    public virtual void TakeDamage(int physicalDamage, int fireDamage, string damageAnimation)
    {
        if (characterManager.isDead)
        {
            return;
        }



        float totalPhysicalDamageAbsorption = 1 - (1 - physicalDamageAbsorptionHead / 50);
        float totalFireDamageAbsorption = 1 - (1 - fireDamageAbsorptionHead / 50);

        physicalDamage = Mathf.RoundToInt(physicalDamage - (physicalDamage * totalPhysicalDamageAbsorption));
        fireDamage = Mathf.RoundToInt(fireDamage - (fireDamage * totalFireDamageAbsorption));

        Debug.Log("Total Physical Damage Absorption: " + totalPhysicalDamageAbsorption);

        float finalDamage = physicalDamage + fireDamage;
        currentHealth -= finalDamage;

        Debug.Log("Final Damage: " + finalDamage);

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            characterManager.isDead = true;
        }

        //characterManager.characterSoundFXManager.PlayRandomDamageSoundFX();
    }


    public virtual void HandlePoiseResetTimer()
    {
        if (poiseResetTimer > 0)
        {
            poiseResetTimer -= Time.deltaTime;
        }
        else
        {
            totalPoiseDefence = armorPoiseBonus;
        }
    }

    public float SetMaxHealthFromHealthLevel()
    {
        maxHealth = vitalityLevel * 10;
        return maxHealth;
    }

    public float SetMaxStaminaFromEnduranceLevel()
    {
        maxStamina = enduranceLevel * 10;
        return maxStamina;
    }

    public float SetMaxFocusFromFocusLevel()
    {
        maxFocusPoint = focusLevel * 10;
        return maxFocusPoint;
    }
}
