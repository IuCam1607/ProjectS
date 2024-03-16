using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffectManager : CharacterEffectManager
{
    PlayerManager player;

    public GameObject currentParticleFX;
    public GameObject instantiatedFXModel;
    public int amountToBeHealed;
    public int amountToBeManaRestored;

    protected override void Awake()
    {
        player = GetComponent<PlayerManager>();
    }

    public void HealPlayerFromEffect(int healAmount)
    {
        if (player.playerInventoryManager.currentConsumable.isFlask)
        {
            if (player.playerInventoryManager.currentConsumable.hpFlask)
            {
                player.playerStatsManager.HealPlayer(amountToBeHealed);
            }
            else if (player.playerInventoryManager.currentConsumable.fpFlask)
            {
                player.playerStatsManager.RestoreFocusPointPlayer(amountToBeManaRestored);
            }

            GameObject healParticles = Instantiate(currentParticleFX, player.playerStatsManager.transform);
            Destroy(instantiatedFXModel.gameObject);
        }
    }
}
