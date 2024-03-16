using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableItem : Item
{
    [Header("Flask Type")]
    public bool isFlask;
    public bool hpFlask;
    public bool fpFlask;


    [Header("Item Quantity")]
    public int maxItemAmount;
    public int currentItemAmount;

    [Header("Item Model")]
    public GameObject itemModel;

    [Header("Animations")]
    public string consumeAnimation;
    public bool isInteracting;

    public virtual void AttempToConsumeItem(PlayerManager player)
    {
        if (currentItemAmount > 0)
        {
            player.playerAnimationManager.PlayTargetActionAnimation(consumeAnimation, isInteracting, false, true, true);
        }
        else
        {
            player.playerAnimationManager.PlayTargetActionAnimation("Shrug", true);
        }

    }
}
