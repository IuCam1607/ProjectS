using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatsManager : MonoBehaviour
{
    public int CalculateStaminaBasedOnEnduranceLevel(int endurance)
    {
        float stamina = 0;

        stamina = endurance * 10;

        return Mathf.RoundToInt(stamina);
    }
}
