using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimatorManager : AnimatorManager
{
    EnemyManager enemyManager;

    private void Awake()
    {
        enemyManager = GetComponent<EnemyManager>();    
        animator = GetComponent<Animator>();
        animator.applyRootMotion = true;
    }

    private void OnAnimatorMove()
    {        
        float delta = Time.deltaTime;
        enemyManager.enemyRigidbody.drag = 0;
        Vector3 deltaPosition = animator.deltaPosition;
        deltaPosition.y = 0;
        Vector3 velocity = deltaPosition / delta;
        enemyManager.enemyRigidbody.velocity = velocity;
    }

    public void PlayTargetActionAnimation(string targetAnimation, bool isPerformingAction, bool applyRootMotion = true, bool canRotate = false, bool canMove = false)
    {
        enemyManager.applyRootMotion = applyRootMotion;
        animator.CrossFade(targetAnimation, 0.2f);
        enemyManager.isPerformingAction = isPerformingAction;
        enemyManager.canRotate = canRotate;
        enemyManager.canMove = canMove;
    }

    public override void TakeCriticalDamageAnimationEvent()
    {
        enemyManager.enemyStats.TakeDamageNoAnimation(enemyManager.pendingCriticalDamage);
        enemyManager.pendingCriticalDamage = 0;
    }

    public void CanRotate()
    {
        enemyManager.canRotate = true;
    }

    public void StopRotate()
    {
        enemyManager.canRotate = false;
    }

    public void EnebleCombo()
    {
        animator.SetBool("canDoCombo", true);
    }

    public void DisableCombo()
    {
        animator.SetBool("canDoCombo", false);
    }

    public void EnableIsInvulnerable()
    {
        animator.SetBool("isInvulnerable", true);
    }

    public void DisableIsInvulnerable()
    {
        animator.SetBool("isInvulnerable", false);
    }

    public void EnableCanBeRiposted()
    {
        enemyManager.canBeRiposted = true;
    }

    public void DisableCanBeRiposted()
    {
        enemyManager.canBeRiposted = false;
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
                soulCountBar.SetSoulCountText(playerStats.soulCount);
            }
        }


    }
}
