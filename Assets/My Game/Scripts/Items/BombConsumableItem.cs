using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Consumables/Bomb Item")]
public class BombConsumableItem : ConsumableItem
{
    [Header("Velocity")]
    public int upwardVelocity = 50;
    public int forwardVelocity = 50;
    public int bombMass = 1;

    [Header("Live Bomb Model")]
    public GameObject liveBombModel;

    [Header("Base Damage")]
    public int baseDamage = 100;

    public override void AttempToConsumeItem(PlayerAnimatorManager playerAnimator, PlayerWeaponSlotManager weaponSlotManager, PlayerEffectManager playerEffectManager)
    {
        if (currentItemAmount > 0)
        {
            playerAnimator.PlayTargetActionAnimation(consumeAnimation, true);
            GameObject bombModel = Instantiate(itemModel, weaponSlotManager.rightHandSlot.transform.position, Quaternion.identity, weaponSlotManager.rightHandSlot.transform);
            playerEffectManager.instantiatedFXModel = bombModel;
        }
        else
        {
            playerAnimator.PlayTargetActionAnimation("Shrug", true);
        }
    }
}
