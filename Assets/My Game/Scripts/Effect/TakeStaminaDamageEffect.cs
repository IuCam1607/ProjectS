using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character Effects/Instant Effects/Take Stamina Damage")]
public class TakeStaminaDamageEffect : InstantCharacterEffect
{
    public float staminaDamage;

    public override void ProcessEffect(PlayerManager player)
    {
        CalculateStaminaDamage(player);
    }

    private void CalculateStaminaDamage(PlayerManager player)
    {
        Debug.Log("Character is Taking: " +  staminaDamage + " Stamina Damage");
        player.playerStatsManager.currentStamina -= staminaDamage;
    }
}
