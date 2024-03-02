using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatsManager : CharacterStatsManager
{
    CapsuleCollider capsuleCollider;
    EnemyAnimatorManager enemyAnimatorManager;

    public UIEnemyHealthBar enemyHealthBar;

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
        enemyHealthBar.SetMaxHealth((int)maxHealth);

    }

    private float SetMaxHealthFromHealthLevel()
    {
        maxHealth = vitalityLevel * 10;
        return maxHealth;
    }

    public void TakeDamageNoAnimation(int damage)
    {
        currentHealth -= damage;

        enemyHealthBar.SetHealth((int)currentHealth);

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            isDead = true;
        }
    }

    public override void TakeDamage(int damage, string damageAnimation = "Damage_01")
    {
        base.TakeDamage(damage, damageAnimation = "Damage_01");

        enemyHealthBar.SetHealth((int)currentHealth);

        //enemyAnimatorManager.PlayTargetActionAnimation(damageAnimation, true);

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
