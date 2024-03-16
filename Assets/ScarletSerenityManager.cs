using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScarletSerenityManager : Interactable
{
    public override void Interact(PlayerManager playerManager)
    {
        WorldSaveGameManager.instance.SaveGame();

        Debug.Log("Game Saved!");
    }
}
