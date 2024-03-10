using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatsManager : CharacterStatsManager
{
    EnemyBossManager enemyBossManager;
    CapsuleCollider capsuleCollider;
    EnemyAnimatorManager enemyAnimatorManager;

    public UIEnemyHealthBar enemyHealthBar;

    public int soulsAwardedOnDeath = 50;

    public bool isBoss;

    private void Awake()
    {
        enemyBossManager = GetComponent<EnemyBossManager>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        enemyAnimatorManager = GetComponent<EnemyAnimatorManager>();

        maxHealth = SetMaxHealthFromHealthLevel();
        currentHealth = maxHealth;
    }

    private void Start()
    {
        if (!isBoss)
        {
            enemyHealthBar.SetMaxHealth((int)maxHealth);
        }
    }

    private float SetMaxHealthFromHealthLevel()
    {
        maxHealth = vitalityLevel * 10;
        return maxHealth;
    }

    public override void TakeDamageNoAnimation(int physicalDamage, int fireDamage)
    {
        base.TakeDamageNoAnimation(physicalDamage, fireDamage);

        if (!isBoss)
        {
            enemyHealthBar.SetHealth(Mathf.RoundToInt(currentHealth));
        }
        else if (isBoss && enemyBossManager != null)
        {
            enemyBossManager.UpdateBossHealthBar(Mathf.RoundToInt(currentHealth), Mathf.RoundToInt(maxHealth));
        }

        if (currentHealth <= 0)
        {
            HandleDeath();
        }

    }

    public override void TakeDamage(int physicalDamage,int fireDamage, string damageAnimation = "Damage_01")
    {
        if (isDead)
            return;

        base.TakeDamage(physicalDamage, fireDamage, damageAnimation = "Damage_01");

        if (!isBoss)
        {
            enemyHealthBar.SetHealth(Mathf.RoundToInt(currentHealth));
        }
        else if (isBoss && enemyBossManager != null)
        {
            enemyBossManager.UpdateBossHealthBar(Mathf.RoundToInt(currentHealth), Mathf.RoundToInt(maxHealth));
        }

        enemyAnimatorManager.PlayTargetActionAnimation(damageAnimation, true);

        if (currentHealth <= 0)
        {
            HandleDeath();
        }
    }

    public void GuardBreak()
    {
        enemyAnimatorManager.PlayTargetActionAnimation("Guard Break", true);
        totalPoiseDefence = armorPoiseBonus;
    }

    private void HandleDeath()
    {
        currentHealth = 0;
        enemyAnimatorManager.PlayTargetActionAnimation("Death", true);
        isDead = true;
    }
}
