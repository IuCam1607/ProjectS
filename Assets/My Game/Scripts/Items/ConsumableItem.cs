using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableItem : Item
{
    [Header("Item Quantity")]
    public int maxItemAmount;
    public int currentItemAmount;

    [Header("Item Model")]
    public GameObject itemModel;

    [Header("Animations")]
    public string consumeAnimation;
    public bool isInteracting;

    public virtual void AttempToConsumeItem(PlayerAnimatorManager playerAnimator, WeaponSlotManager weaponSlotManager, PlayerEffectManager playerEffectManager)
    {
        if (currentItemAmount > 0)
        {
            playerAnimator.PlayTargetActionAnimation(consumeAnimation, isInteracting, false, true, true);
        }
        else
        {
            playerAnimator.PlayTargetActionAnimation("Shrug", true);
        }

    }
}
