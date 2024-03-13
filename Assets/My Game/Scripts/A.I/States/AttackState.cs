using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    public RotateTowardsTargetState rotateTowardsTargetState;
    public CombatStanceState combatStanceState;
    public PursueTargetState pursueTargetState;
    public EnemyAttackAction currentAttack;

    bool willDoComboOnNextAttack = false;
    public bool hasPerformedAttack = false;

    public override State Tick(EnemyManager enemyManager, EnemyStatsManager enemyStats, EnemyAnimatorManager enemyAnimator)
    {
        if (enemyStats.isDead)
            return this;

        float distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, enemyManager.transform.position);
        RotateTowardsTargetWhilstAttacking(enemyManager);

        if (distanceFromTarget > enemyManager.maximumAggroRadius)
        {
            return pursueTargetState;
        }

        if (willDoComboOnNextAttack && enemyManager.canDoCombo)
        {
            AttackTargetWithCombo(enemyAnimator, enemyManager);
        }

        if (!hasPerformedAttack)
        {
            AttackTarget(enemyAnimator, enemyManager);
            RollForComboChance(enemyManager);
        }

        if (willDoComboOnNextAttack && hasPerformedAttack)
        {
            return this;
        }

        return rotateTowardsTargetState;
    }

    private void AttackTarget(EnemyAnimatorManager enemyAnimator, EnemyManager enemyManager)
    {
        enemyAnimator.animator.SetBool("isUsingRightHand", currentAttack.rightHandedAction);
        enemyAnimator.animator.SetBool("isUsingLeftHand", !currentAttack.rightHandedAction);
        enemyAnimator.PlayTargetActionAnimation(currentAttack.actionAnimation, true, true, true);
        enemyAnimator.animator.SetBool("isInteracting", true);
        enemyManager.currentRecoveryTime = currentAttack.recoveryTime;
        hasPerformedAttack = true;
        Debug.Log("Not Combo");
    }

    private void AttackTargetWithCombo(EnemyAnimatorManager enemyAnimator, EnemyManager enemyManager)
    {
        enemyAnimator.animator.SetBool("isUsingRightHand", currentAttack.rightHandedAction);
        enemyAnimator.animator.SetBool("isUsingLeftHand", !currentAttack.rightHandedAction);
        willDoComboOnNextAttack = false;
        enemyAnimator.PlayTargetActionAnimation(currentAttack.actionAnimation, true, true, true);
        enemyAnimator.animator.SetBool("isInteracting", true);
        enemyManager.currentRecoveryTime = currentAttack.recoveryTime;
        currentAttack = null;
        Debug.Log("Combo");
    }

    private void RotateTowardsTargetWhilstAttacking(EnemyManager enemyManager)
    {
        if (enemyManager.canRotate && enemyManager.isInteracting)
        {
            Vector3 direction = enemyManager.currentTarget.transform.position - transform.position;
            direction.y = 0;
            direction.Normalize();

            if (direction == Vector3.zero)
            {
                direction = transform.forward;
            }

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            enemyManager.transform.rotation = Quaternion.Slerp(enemyManager.transform.rotation, targetRotation, enemyManager.rotationSpeed / Time.deltaTime);
        }
    }

    private void RollForComboChance(EnemyManager enemyManager)
    {
        float comboChance = Random.Range(0, 100);

        if (enemyManager.allowAIToPerformCombos && comboChance <= enemyManager.comboLikelyHood)
        {
            if(currentAttack.comboAction != null)
            {
                willDoComboOnNextAttack = true;
                currentAttack = currentAttack.comboAction;
            }
            else
            {
                willDoComboOnNextAttack = false;
                currentAttack = null;
            }
        }
    }
}
