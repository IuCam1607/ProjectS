using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WorldItemDataBase : MonoBehaviour
{
    public static WorldItemDataBase instance;

    public List<WeaponItem> WeaponItems = new List<WeaponItem>();

    public List<EquipmentItem> equipmentItems = new List<EquipmentItem>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public WeaponItem GetWeaponItemByItemID(int weaponID)
    {
        return WeaponItems.FirstOrDefault(weapon => weapon.itemID == weaponID);
    }

    public EquipmentItem GetEquipmentItemByItemID(int equipmentID)
    {
        return equipmentItems.FirstOrDefault(equipment => equipment.itemID == equipmentID);
    }


}
