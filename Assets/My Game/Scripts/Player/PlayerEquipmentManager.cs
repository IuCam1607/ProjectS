using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipmentManager : MonoBehaviour
{
    PlayerManager player;

    [Header("Equipment Model Changers")]
    HelmetModelChanger helmetModelChanger;

    [Header("Default UnEquipMent Models")]
    public GameObject nakedHeadModel;

    public BlockingCollider blockingCollider;

    private void Awake()
    {
        player = GetComponent<PlayerManager>();
        helmetModelChanger = GetComponentInChildren<HelmetModelChanger>();

    }

    private void Start()
    {
        EquipAllEquipmentModelsOnStart();
    }

    public void EquipAllEquipmentModelsOnStart()
    {     
        if (player.playerInventoryManager.currentHelmetEquipment != null)
        {
            helmetModelChanger.EquipHelmetModelByName(player.playerInventoryManager.currentHelmetEquipment.helmetModelName);
            player.playerStatsManager.physicalDamageAbsorptionHead = player.playerInventoryManager.currentHelmetEquipment.physicalDefense;
        }
        else
        {
            helmetModelChanger.UnEquipAllHelmetModels();
            player.playerStatsManager.physicalDamageAbsorptionHead = 0;
        }
    }

    public void OpenBlockingCollider()
    {
        if (player.playerInput.twoHandFlag)
        {
            blockingCollider.SetColliderDamageAbsorption(player.playerInventoryManager.rightWeapon);
        }
        else
        {
            blockingCollider.SetColliderDamageAbsorption(player.playerInventoryManager.leftWeapon);
        }

        blockingCollider.EnableBlockingCollider();
    }

    public void CloseBlockingCollider()
    {
        blockingCollider.DisableBlockingCollider();
    }
}
