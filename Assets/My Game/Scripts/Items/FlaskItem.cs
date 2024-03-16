using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Consumables/Flask")]
public class FlaskItem : ConsumableItem
{
    [Header("Recovery Amount")]
    public int healthRecoveryAmount;
    public int focusPointRecoveryAmount;

    [Header("Recovery FX")]
    public GameObject recoveryFX;

    public override void AttempToConsumeItem(PlayerManager player)
    {
        if (currentItemAmount > 0)
        { 
            currentItemAmount--;
            player.playerAnimationManager.PlayTargetActionAnimation(consumeAnimation, isInteracting, false, true, true);
            player.PlaySFX(player.feedBackManager.drinkPotionSFX);
            GameObject flask = Instantiate(itemModel, player.playerWeaponSlotManager.rightHandSlot.transform);
            player.playerEffectManager.currentParticleFX = recoveryFX;
            player.playerEffectManager.amountToBeHealed = healthRecoveryAmount;
            player.playerEffectManager.amountToBeManaRestored = focusPointRecoveryAmount;
            player.playerEffectManager.instantiatedFXModel = flask;
            player.playerWeaponSlotManager.rightHandSlot.UnloadWeapon();
        }
        else
        {
            player.playerAnimationManager.PlayTargetActionAnimation("Flask Empty", isInteracting, false, true, true);
        }

    }
}
