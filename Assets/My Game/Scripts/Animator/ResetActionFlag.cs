using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetActionFlag : StateMachineBehaviour
{
    PlayerManager player;
    EnemyManager enemy;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(player == null)
        {
            player = animator.GetComponentInParent<PlayerManager>();
        }

        if (enemy == null)
        {
            enemy = animator.GetComponentInParent<EnemyManager>();
        }

        if (player)
        {
            player.isPerformingAction = false;
            player.applyRootMotion = false;
            player.canMove = true;
            player.playerAnimationManager.animator.SetBool("canRotate", true);
            player.isJumping = false;
            player.canDoCombo = false;
            player.isRolling = false;
        }

        if (enemy)
        {
            enemy.isPerformingAction = false;
            enemy.applyRootMotion = false;
            enemy.canMove = true;
            enemy.isRotatingWithRootMotion = false;
            Debug.Log("Resetting Action Flag");
        }

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
