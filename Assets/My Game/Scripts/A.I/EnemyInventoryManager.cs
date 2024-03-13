using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInventoryManager : CharacterInventoryManager
{
    EnemyWeaponSlotManager enemyWeaponSlotManager;

    private void Awake()
    {
        enemyWeaponSlotManager = GetComponent<EnemyWeaponSlotManager>();
    }

    protected override void Start()
    {
        base.Start();
        enemyWeaponSlotManager.LoadBothWeaponOnSlots();
    }
}
