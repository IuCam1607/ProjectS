using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatsManager : MonoBehaviour
{
    [Header("Team I.D")]
    public int teamIDNumber = 0;

    [Header("Levels")]
    public float vitalityLevel = 10;
    public int focusLevel = 10;
    public int enduranceLevel = 10;

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

    [Header("Status")]
    public bool isDead = false;

    [Header("Armor Absorptions")]
    public float physicalDamageAbsorptionHead;
    public float fireDamageAbsorptionHead;

    public int soulCount = 0;

    public virtual void Update()
    {
        HandlePoiseResetTimer();
    }

    private void Start()
    {
        totalPoiseDefence = armorPoiseBonus;
    }

    public virtual void TakeDamageNoAnimation(int physicalDamage, int fireDamage)
    {
        if (isDead)
            return;

        float totalPhysicalDamageAbsorption = 1 - (1 - physicalDamageAbsorptionHead / 50);
        float totalFireDamageAbsorption = 1 - (1 - fireDamageAbsorptionHead / 50);

        physicalDamage = Mathf.RoundToInt(physicalDamage - (physicalDamage * totalPhysicalDamageAbsorption));
        fireDamage = Mathf.RoundToInt(fireDamage - (fireDamage * totalFireDamageAbsorption));

        float finalDamage = physicalDamage + fireDamage;
        currentHealth -= finalDamage;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            isDead = true;
        }
    }

    public virtual void TakeDamage(int physicalDamage, int fireDamage, string damageAnimation = "Damage_01")
    {
        if (isDead)
            return;

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
            isDead = true;
        }
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
}
