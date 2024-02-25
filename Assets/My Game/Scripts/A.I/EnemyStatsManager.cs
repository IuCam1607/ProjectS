using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatsManager : CharacterStatsManager
{
    CapsuleCollider capsuleCollider;
    EnemyAnimatorManager enemyAnimatorManager;

    public int soulsAwardedOnDeath = 50;

    private void Awake()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
        enemyAnimatorManager = GetComponent<EnemyAnimatorManager>();
    }

    private void Start()
    {
        maxHealth = SetMaxHealthFromHealthLevel();
        currentHealth = maxHealth;

    }

    private float SetMaxHealthFromHealthLevel()
    {
        maxHealth = vitalityLevel * 10;
        return maxHealth;
    }

    public void TakeDamageNoAnimation(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            isDead = true;
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead)
            return;

        // animation take Damage
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            HandleDeath();
        }
    }

    private void HandleDeath()
    {
        currentHealth = 0;
        enemyAnimatorManager.PlayTargetActionAnimation("Skeleton_Dead_01", true);
        isDead = true;

    }
}
