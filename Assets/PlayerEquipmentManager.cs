using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipmentManager : MonoBehaviour
{
    PlayerManager player;

    [Header("Equipment Model Changers")]
    HelmetModelChanger helmetModelChanger;



    public BlockingCollider blockingCollider;

    private void Awake()
    {
        player = GetComponentInParent<PlayerManager>();
        helmetModelChanger = GetComponentInChildren<HelmetModelChanger>();
    }

    private void Start()
    {
        helmetModelChanger.UnEquipAllHelmetModels();
        helmetModelChanger.EquipHelmetModelByName(player.playerInventoryManager.currentHelmetEquipment.helmetModelName);
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
