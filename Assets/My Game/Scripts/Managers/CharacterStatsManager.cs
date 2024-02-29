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

    public int soulCount = 0;

    public virtual void TakeDamage(int damage, string damageAnimation = "Damage_01")
    {

    }
}
