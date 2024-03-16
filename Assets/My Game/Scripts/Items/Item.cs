using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ScriptableObject
{
    [Header("Item Infomation")]
    public string itemName;
    public Sprite pickupIcon;
    public Sprite itemIcon;
    public int itemID;

    [TextArea] public string itemDescription;


}
