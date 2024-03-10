using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffectManager : CharacterEffectManager
{
    PlayerStatsManager playerStats;
    PlayerWeaponSlotManager weaponSlotManager;
    public GameObject currentParticleFX;
    public GameObject instantiatedFXModel;
    public int amountToBeHealed;

    private void Awake()
    {
        playerStats = GetComponent<PlayerStatsManager>();
        weaponSlotManager = GetComponent<PlayerWeaponSlotManager>();
    }

    public void HealPlayerFromEffect(int healAmount)
    {
        playerStats.HealPlayer(amountToBeHealed);
        GameObject healParticles = Instantiate(currentParticleFX, playerStats.transform);
        Destroy(instantiatedFXModel.gameObject);
        //weaponSlotManager.LoadBothWeaponOnSlots();
    }
}
