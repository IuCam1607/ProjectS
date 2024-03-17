using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScarletSerenityManager : Interactable
{
    public override void Interact(PlayerManager playerManager)
    {
        WorldSaveGameManager.instance.SaveGame();
        playerManager.playerAnimationManager.PlayTargetActionAnimation("Born Fire", true);
        playerManager.playerWeaponSlotManager.rightHandSlot.UnloadWeaponAndDestroy();
        playerManager.PlaySFX(playerManager.feedBackManager.bornfireSFX);

        Debug.Log("Game Saved!");
    }
}
