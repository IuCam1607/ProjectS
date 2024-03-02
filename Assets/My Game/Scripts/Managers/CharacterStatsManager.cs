using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatsManager : MonoBehaviour
{
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

    [Header("Status")]
    public bool isDead = false;

    [Header("Armor Absorptions")]
    public float physicalDamageAbsorptionHead;

    public int soulCount = 0;

    public virtual void TakeDamage(int physicalDamage, string damageAnimation = "Damage_01")
    {
        if (isDead)
            return;

        float totalPhysicalDamageAbsorption = 1 - (1 - physicalDamageAbsorptionHead / 50);

        physicalDamage = Mathf.RoundToInt(physicalDamage - (physicalDamage * totalPhysicalDamageAbsorption));

        Debug.Log("Total Physical Damage Absorption: " + totalPhysicalDamageAbsorption);

        float finalDamage = physicalDamage;
        currentHealth -= finalDamage;

        Debug.Log("Final Damage: " + finalDamage);

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            isDead = true;
        }
    }
}
