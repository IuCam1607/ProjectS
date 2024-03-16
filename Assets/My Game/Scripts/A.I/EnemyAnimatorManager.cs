using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimatorManager : AnimatorManager
{
    EnemyManager enemyManager;

    protected override void Awake()
    {
        base.Awake();
        enemyManager = GetComponent<EnemyManager>();
        character = GetComponent<CharacterManager>();
    }

    private void OnAnimatorMove()
    {        
        float delta = Time.deltaTime;
        enemyManager.enemyRigidbody.drag = 0;
        Vector3 deltaPosition = enemyManager.animator.deltaPosition;
        deltaPosition.y = 0;
        Vector3 velocity = deltaPosition / delta;
        enemyManager.enemyRigidbody.velocity = velocity;

        if (enemyManager.isRotatingWithRootMotion)
        {
            enemyManager.transform.rotation *= enemyManager.animator.deltaRotation;
        }
    }

    public override void PlayTargetActionAnimation(string targetAnimation, bool isPerformingAction, bool applyRootMotion = true, bool canRotate = false, bool canMove = false, bool mirrorAnim = false)
    {
        base.PlayTargetActionAnimation(targetAnimation, isPerformingAction, applyRootMotion, canRotate, canMove, mirrorAnim);
    }

    public void InstantiateBossParticleFX()
    {
        BossFXTransform bossFXTransform = GetComponentInChildren<BossFXTransform>();

        GameObject phaseFX = Instantiate(enemyManager.enemyBossManager.particleFX, bossFXTransform.transform);
    }

    public void AwardSoulsOnDeath()
    {
        PlayerStatsManager playerStats = FindObjectOfType<PlayerStatsManager>();
        SoulCountBar soulCountBar = FindObjectOfType<SoulCountBar>();

        if (playerStats != null)
        {
            playerStats.AddSouls(enemyManager.enemyStats.soulsAwardedOnDeath);

            if (soulCountBar != null)
            {
                soulCountBar.SetSoulCountText(playerStats.currentBlood);
            }
        }
    }
}
