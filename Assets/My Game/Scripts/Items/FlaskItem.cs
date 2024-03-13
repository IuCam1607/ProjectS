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

    public override void AttempToConsumeItem(PlayerAnimatorManager playerAnimator, PlayerWeaponSlotManager weaponSlotManager, PlayerEffectManager playerEffectManager)
    {
        if (currentItemAmount > 0)
        {
            currentItemAmount--;
            playerAnimator.PlayTargetActionAnimation(consumeAnimation, isInteracting, false, true, true);
            GameObject flask = Instantiate(itemModel, weaponSlotManager.rightHandSlot.transform);
            playerEffectManager.currentParticleFX = recoveryFX;
            playerEffectManager.amountToBeHealed = healthRecoveryAmount;
            playerEffectManager.amountToBeManaRestored = focusPointRecoveryAmount;
            playerEffectManager.instantiatedFXModel = flask;
            weaponSlotManager.rightHandSlot.UnloadWeapon();
        }
        else
        {
            playerAnimator.PlayTargetActionAnimation("Flask Empty", isInteracting, false, true, true);
        }

    }
}
