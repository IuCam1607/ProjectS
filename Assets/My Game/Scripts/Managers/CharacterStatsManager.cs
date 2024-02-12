using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatsManager : MonoBehaviour
{
    [Header("Levels")]
    public float vitalityLevel = 10;
    public int enduranceLevel = 10;

    [Header("Stats")]
    public float currentStamina;
    public float maxStamina;

    public float currentHealth;
    public float maxHealth;
}
