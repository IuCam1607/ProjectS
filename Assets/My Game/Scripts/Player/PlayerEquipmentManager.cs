using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipmentManager : MonoBehaviour
{
    PlayerManager player;

    public int currentRightHandWeaponID;
    public int currentLeftHandWeaponID;

    public WeaponModelInstantiationSlot rightHandSlot;
    public WeaponModelInstantiationSlot leftHandSlot;

    [SerializeField] WeaponManager rightWeaponManager;
    [SerializeField] WeaponManager leftWeaponManager;

    public GameObject rightHandWeaponModel;
    public GameObject leftHandWeaponModel;

    private void Awake()
    {
        player = GetComponent<PlayerManager>();

    }

    private void Start()
    {
        //LoadWeaponsOnBothHands();
    }

    //private void InitializeWeaponSlots()
    //{
    //    WeaponModelInstantiationSlot[] weaponSlots = GetComponentsInChildren<WeaponModelInstantiationSlot>();

    //    foreach (var weaponSlot in weaponSlots)
    //    {
    //        if (weaponSlot.weaponSlot == WeaponModelSlot.RightHand)
    //        {
    //            rightHandSlot = weaponSlot;
    //        }
    //        else if (weaponSlot.weaponSlot == WeaponModelSlot.LeftHand)
    //        {
    //            leftHandSlot = weaponSlot;
    //        }
    //    }
    //}

    //    public void LoadWeaponsOnBothHands()
    //    {
    //        LoadRightWeapon();
    //        LoadLeftWeapon();
    //    }

    //    public void SwitchRightWeapon()
    //    {
    //        player.playerAnimationManager.PlayTargetActionAnimation("Swap_Right_Weapon_01", false, true, true, true);

    //        WeaponItem selectedWeapon = null;

    //        player.playerInventoryManager.rightHandWeaponIndex += 1;

    //        if (player.playerInventoryManager.rightHandWeaponIndex < 0 || player.playerInventoryManager.rightHandWeaponIndex > 2)
    //        {
    //            player.playerInventoryManager.rightHandWeaponIndex = 0;
    //        }

    //        foreach (WeaponItem weapon in player.playerInventoryManager.weaponsInRightHandSlots)
    //        {
    //            // If the next potential weapon does not Equal the Unarmed Weapon
    //            if (player.playerInventoryManager.weaponsInRightHandSlots[player.playerInventoryManager.rightHandWeaponIndex].itemID != WorldItemDatabase.instance.unarmedWeapon.itemID)
    //            {
    //                selectedWeapon = player.playerInventoryManager.weaponsInRightHandSlots[player.playerInventoryManager.rightHandWeaponIndex];
    //                return;
    //            }
    //        }

    //        if(selectedWeapon != null && player.playerInventoryManager.rightHandWeaponIndex < 2)
    //        {
    //            SwitchRightWeapon();
    //        }
    //        else
    //        {
    //            float weaponCount = 0;
    //            WeaponItem firstWeapon = null;
    //            int firstWeaponPosition = 0;

    //            for (int i = 0; i < player.playerInventoryManager.weaponsInRightHandSlots.Length; i++)
    //            {
    //                if (player.playerInventoryManager.weaponsInRightHandSlots[i].itemID != WorldItemDatabase.instance.unarmedWeapon.itemID)
    //                {
    //                    weaponCount += 1;

    //                    if (firstWeapon == null)
    //                    {
    //                        firstWeapon = player.playerInventoryManager.weaponsInRightHandSlots[i];
    //                        firstWeaponPosition = i;
    //                    }
    //                }
    //            }

    //            if (weaponCount <= 1)
    //            {
    //                player.playerInventoryManager.rightHandWeaponIndex = -1;
    //                selectedWeapon = WorldItemDatabase.instance.unarmedWeapon;
    //                player.currentRightHandWeaponID = selectedWeapon.itemID;
    //                player.newWeaponID = selectedWeapon.itemID;
    //            }
    //            else
    //            {
    //                player.playerInventoryManager.rightHandWeaponIndex = firstWeaponPosition;
    //                player.currentRightHandWeaponID = firstWeapon.itemID;
    //                player.newWeaponID = firstWeapon.itemID;
    //            }
    //        }
    //    }

    //    public void LoadRightWeapon()
    //    {
    //        if (player.playerInventoryManager.rightWeapon != null)
    //        {
    //            rightHandSlot.UnloadWeapon();
    //            rightHandWeaponModel = Instantiate(player.playerInventoryManager.rightWeapon.weaponModel);
    //            rightHandSlot.LoadWeapon(rightHandWeaponModel);
    //            rightWeaponManager = rightHandWeaponModel.GetComponent<WeaponManager>();
    //            rightWeaponManager.SetWeaponDamage(player, player.playerInventoryManager.rightWeapon);
    //            Debug.Log("LoadRightWeapon");
    //        }
    //    }

    //    public void SwitchLeftWeapon()
    //    {

    //    }

    //    public void LoadLeftWeapon()
    //    {
    //        if (player.playerInventoryManager.leftWeapon != null)
    //        {
    //            leftHandWeaponModel = Instantiate(player.playerInventoryManager.leftWeapon.weaponModel);
    //            leftHandSlot.LoadWeapon(leftHandWeaponModel);
    //            leftWeaponManager = leftHandWeaponModel.GetComponent<WeaponManager>();
    //            leftWeaponManager.SetWeaponDamage(player, player.playerInventoryManager.leftWeapon);
    //        }
    //    }
}
