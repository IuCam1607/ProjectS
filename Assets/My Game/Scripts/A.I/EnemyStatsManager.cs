using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyStatsManager : CharacterStatsManager
{
    EnemyManager enemy;
    CapsuleCollider capsuleCollider;

    public UIEnemyHealthBar enemyHealthBar;

    public int soulsAwardedOnDeath = 50;

    public bool isBoss;

    protected override void Awake()
    {
        base.Awake();
        capsuleCollider = GetComponent<CapsuleCollider>();
        enemy = GetComponent<EnemyManager>();

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

    public override void TakeDamageNoAnimation(int physicalDamage, int fireDamage)
    {
        if (enemy.isInvulnerable)
            return;

        base.TakeDamageNoAnimation(physicalDamage, fireDamage);
        enemy.PlaySFX(enemy.feedBackManager.hitSFX);

        if (!isBoss)
        {
            enemyHealthBar.SetHealth(Mathf.RoundToInt(currentHealth));
        }
        else if (isBoss && enemy.enemyBossManager != null)
        {
            enemy.enemyBossManager.UpdateBossHealthBar(Mathf.RoundToInt(currentHealth), Mathf.RoundToInt(maxHealth));
        }

        if (currentHealth <= 0)
        {
            if (isBoss)
            {
                StartCoroutine(ProcessDeathEvent());
            }
            else
            {
                HandleDeath();
            }
        }
    }

    public override void TakeDamage(int physicalDamage,int fireDamage, string damageAnimation)
    {
        if (enemy.isInvulnerable)
            return;

        base.TakeDamage(physicalDamage, fireDamage, damageAnimation);
        enemy.PlaySFX(enemy.feedBackManager.hitSFX);

        if (!isBoss)
        {
            enemyHealthBar.SetHealth(Mathf.RoundToInt(currentHealth));
        }
        else if (isBoss && enemy.enemyBossManager != null)
        {
            enemy.enemyBossManager.UpdateBossHealthBar(Mathf.RoundToInt(currentHealth), Mathf.RoundToInt(maxHealth));
        }

        enemy.enemyAnimator.PlayTargetActionAnimation(damageAnimation, true);

        if (currentHealth <= 0)
        {
            if (isBoss)
            {
                StartCoroutine(ProcessDeathEvent());
            }
            else
            {
                HandleDeath();
            }
        }
    }

    public IEnumerator ProcessDeathEvent(bool manuallySelectDeathAnimation = false)
    {
        PlayerUIManager.instance.playerUIPopUPManager.SendBossDefeatedPopUp();

        currentHealth = 0;
        enemy.isDead = true;

        if (!manuallySelectDeathAnimation)
        {
            enemy.enemyAnimator.PlayTargetActionAnimation("Death", true);
        }

        AudioManager.instance.StopBGM();
        enemy.enemyBossManager.bossHealthBar.gameObject.SetActive(false);
        enemy.footStepManager.SetActive(false);

        List<FogWall> fogWall = FindObjectsOfType<FogWall>().ToList();

        if (fogWall != null)
        {
            foreach (var fog in fogWall)
            {
                fog.DeactivateFogWall();
            }
        }

        yield return null;
    }

    public void GuardBreak()
    {
        enemy.enemyAnimator.PlayTargetActionAnimation("Guard Break", true);
        totalPoiseDefence = armorPoiseBonus;
    }

    private void HandleDeath()
    {
        currentHealth = 0;
        enemy.enemyAnimator.PlayTargetActionAnimation("Death", true);
        enemy.PlaySFX(enemy.feedBackManager.deadSFX);
        enemy.footStepManager.SetActive(false);
        enemy.isDead = true;
    }
}
