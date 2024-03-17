using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    public override void Interact(PlayerManager playerManager)
    {
        base.Interact(playerManager);
        PlayerUIManager.instance.isUIActive = true;
        PlayerUIManager.instance.doorWindow.SetActive(true);
        Debug.Log("Interact");
    }
}
