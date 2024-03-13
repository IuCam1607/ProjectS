using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpInteractable : Interactable
{
    public override void Interact(PlayerManager playerManager)
    {
        PlayerUIManager.instance.levelUpWindow.SetActive(true);
    }
}
