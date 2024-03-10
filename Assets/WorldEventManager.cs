using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldEventManager : MonoBehaviour
{
    public List<FogWall> fogWalls;
    public UIBossHealthBar bossHealthBar;
    public EnemyBossManager boss;

    public bool bossFightActive;
    public bool bossHasBeenAwakened;
    public bool bossHasBeenDefeated;

    private void Awake()
    {
        bossHealthBar = FindObjectOfType<UIBossHealthBar>();
    }

    public void ActivateBossFight()
    {
        bossFightActive = true;
        bossHasBeenAwakened = true;
        bossHealthBar.SetUIHealthBarToActive();

        foreach (var fogWall in fogWalls)
        {
            fogWall.ActivateFogWall();
        }
    }

    public void BossHasBeenDefeated()
    {
        bossHasBeenDefeated = true;
        bossFightActive = false;

        foreach (var fogWall in fogWalls)
        {
            fogWall.DeactivateFogWall();
        }
    }
}
