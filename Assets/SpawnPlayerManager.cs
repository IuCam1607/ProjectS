using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnPlayerManager : MonoBehaviour
{
    public FlaskItem flaskItem;
    public CharacterSaveData characterSaveData;
    public GameObject player;
    public GameObject interactableUI;
    public GameObject itemInteractableUI;

    public Transform spawnPoint;

    private void Start()
    {
        SpawnPlayer();
    }

    public void SpawnPlayer()
    {
        Vector3 spawnPosition = spawnPoint.position;
        if (characterSaveData.currentPosition != Vector3.zero)
        {
            spawnPosition = characterSaveData.currentPosition;
        }

        flaskItem.currentItemAmount = flaskItem.maxItemAmount;

        PlayerManager playerManager = Instantiate(player, spawnPosition, transform.rotation).GetComponent<PlayerManager>();
        playerManager.playerStatsManager.healthBar = FindObjectOfType<HealthBar>();
        playerManager.playerStatsManager.staminaBar = FindObjectOfType<StaminaBar>();
        playerManager.playerStatsManager.focusPointBar = FindObjectOfType<FocusPointBar>();

        playerManager.interactableUIGameObject = interactableUI;
        playerManager.itemInteractableGameObject = itemInteractableUI;
        PlayerUIManager.instance.player = playerManager;
        PlayerUIManager.instance.Setup();
    }


}
