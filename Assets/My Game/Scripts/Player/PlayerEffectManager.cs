using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffectManager : MonoBehaviour
{
    PlayerStatsManager playerStats;
    WeaponSlotManager weaponSlotManager;
    public GameObject currentParticleFX;
    public GameObject instantiatedFXModel;
    public int amountToBeHealed;

    private void Awake()
    {
        playerStats = GetComponentInParent<PlayerStatsManager>();
        weaponSlotManager = GetComponent<WeaponSlotManager>();
    }

    public void HealPlayerFromEffect(int healAmount)
    {
        playerStats.HealPlayer(amountToBeHealed);
        GameObject healParticles = Instantiate(currentParticleFX, playerStats.transform);
        Destroy(instantiatedFXModel.gameObject);
        //weaponSlotManager.LoadBothWeaponOnSlots();
    }
}
