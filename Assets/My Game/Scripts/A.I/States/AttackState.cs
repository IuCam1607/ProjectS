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

    public override State Tick(EnemyManager enemy)
    {
        if (enemy.isDead)
            return this;

        float distanceFromTarget = Vector3.Distance(enemy.currentTarget.transform.position, enemy.transform.position);
        RotateTowardsTargetWhilstAttacking(enemy);

        if (distanceFromTarget > enemy.maximumAggroRadius)
        {
            return pursueTargetState;
        }

        if (willDoComboOnNextAttack && enemy.canDoCombo)
        {
            AttackTargetWithCombo(enemy);
        }

        if (!hasPerformedAttack)
        {
            AttackTarget(enemy);
            RollForComboChance(enemy);
        }

        if (willDoComboOnNextAttack && hasPerformedAttack)
        {
            return this;
        }

        return rotateTowardsTargetState;
    }

    private void AttackTarget(EnemyManager enemy)
    {
        enemy.animator.SetBool("isUsingRightHand", currentAttack.rightHandedAction);
        enemy.animator.SetBool("isUsingLeftHand", !currentAttack.rightHandedAction);
        enemy.enemyAnimator.PlayTargetActionAnimation(currentAttack.actionAnimation, true, true, true);
        enemy.animator.SetBool("isInteracting", true);
        enemy.currentRecoveryTime = currentAttack.recoveryTime;
        hasPerformedAttack = true;
        Debug.Log("Not Combo");
    }

    private void AttackTargetWithCombo(EnemyManager enemy)
    {
        enemy.animator.SetBool("isUsingRightHand", currentAttack.rightHandedAction);
        enemy.animator.SetBool("isUsingLeftHand", !currentAttack.rightHandedAction);
        willDoComboOnNextAttack = false;
        enemy.enemyAnimator.PlayTargetActionAnimation(currentAttack.actionAnimation, true, true, true);
        enemy.animator.SetBool("isInteracting", true);
        enemy.currentRecoveryTime = currentAttack.recoveryTime;
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
