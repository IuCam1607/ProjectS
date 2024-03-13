using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffectManager : CharacterEffectManager
{
    PlayerInventoryManager playerInventory;
    PlayerStatsManager playerStats;

    public GameObject currentParticleFX;
    public GameObject instantiatedFXModel;
    public int amountToBeHealed;
    public int amountToBeManaRestored;

    private void Awake()
    {
        playerInventory = GetComponent<PlayerInventoryManager>();
        playerStats = GetComponent<PlayerStatsManager>();
    }

    public void HealPlayerFromEffect(int healAmount)
    {
        if (playerInventory.currentConsumable.isFlask)
        {
            if (playerInventory.currentConsumable.hpFlask)
            {
                playerStats.HealPlayer(amountToBeHealed);
            }
            else if (playerInventory.currentConsumable.fpFlask)
            {
                playerStats.RestoreFocusPointPlayer(amountToBeManaRestored);
            }

            GameObject healParticles = Instantiate(currentParticleFX, playerStats.transform);
            Destroy(instantiatedFXModel.gameObject);
        }
    }
}
