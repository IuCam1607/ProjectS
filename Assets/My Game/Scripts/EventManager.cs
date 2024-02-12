using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    public delegate void WeaponIDChangeEventHandle(int oldID, int newID);
    public static event WeaponIDChangeEventHandle OnWeaponIDChange;
}
