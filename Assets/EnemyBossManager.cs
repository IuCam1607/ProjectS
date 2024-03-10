using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBossManager : MonoBehaviour
{
    public string bossName;

    EnemyManager enemyManager;
    UIBossHealthBar bossHealthBar;
    public EnemyStatsManager enemyStats;
    public EnemyAnimatorManager enemyAnimator;
    public BossCombatStanceState bossCombatStanceState;

    [Header("Second Phase FX")]
    public GameObject particleFX;

    private void Awake()
    {
        bossHealthBar = FindObjectOfType<UIBossHealthBar>();
        enemyManager = GetComponent<EnemyManager>();
        enemyStats = GetComponent<EnemyStatsManager>();
        enemyAnimator = GetComponent<EnemyAnimatorManager>();
        bossCombatStanceState = GetComponentInChildren<BossCombatStanceState>();
    }

    private void Start()
    {
        bossHealthBar.SetBossName(bossName);
        bossHealthBar.SetBossMaxHealth(Mathf.RoundToInt(enemyStats.maxHealth));
    }

    public void UpdateBossHealthBar(int currentHealth, int maxHealth)
    {
        bossHealthBar.SetBossCurrentHealth(currentHealth);

        if (currentHealth <= maxHealth / 2 && !bossCombatStanceState.hasPhaseShifted)
        {
            bossCombatStanceState.hasPhaseShifted = true;
            ShiftToSecondPhase();
        }
    }

    public void ShiftToSecondPhase()
    {
        enemyAnimator.animator.SetBool("isInvulnerable", true);
        enemyAnimator.animator.SetBool("isPhaseShifting", true);
        enemyAnimator.PlayTargetActionAnimation("Phase Shift", true);
        bossCombatStanceState.hasPhaseShifted = true;
    }


}
