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
    public int explosiveDamage = 50;

    public override void AttempToConsumeItem(PlayerManager player)
    {
        if (currentItemAmount > 0)
        {
            player.playerWeaponSlotManager.rightHandSlot.UnloadWeapon();
            player.playerAnimationManager.PlayTargetActionAnimation(consumeAnimation, true);
            GameObject bombModel = Instantiate(itemModel, player.playerWeaponSlotManager.rightHandSlot.transform.position, Quaternion.identity, player.playerWeaponSlotManager.rightHandSlot.transform);
            player.playerEffectManager.instantiatedFXModel = bombModel;
        }
        else
        {
            player.playerAnimationManager.PlayTargetActionAnimation("Shrug", true);
        }
    }
}
