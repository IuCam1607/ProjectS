using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorManager : AnimatorManager
{
    private PlayerManager player;

    int vertical;
    int horizontal;

    private void Awake()
    {
        player = GetComponentInParent<PlayerManager>();
        animator = GetComponent<Animator>();

        vertical = Animator.StringToHash("Vertical");
        horizontal = Animator.StringToHash("Horizontal");
    }

    private void OnAnimatorMove()
    {
        if(player.applyRootMotion)
        {
            Vector3 velocity = animator.deltaPosition;
            player.characterController.Move(velocity);
            player.transform.rotation *= animator.deltaRotation;
        }
    }

    public void UpdateAnimatorMovementParameters(float horizontalMovement, float verticalMovement, bool isSprinting)
    {
        float horizontalAmount = horizontalMovement;
        float verticalAmount = verticalMovement;

        if(isSprinting)
        {
            verticalAmount = 2;
        }
        animator.SetFloat(horizontal, horizontalAmount, 0.1f, Time.deltaTime);
        animator.SetFloat(vertical, verticalAmount, 0.1f, Time.deltaTime);
    }
    public void PlayTargetActionAnimation(string targetAnimation, bool isPerformingAction, bool applyRootMotion = true, bool canRotate = false, bool canMove = false)
    {
        player.applyRootMotion = applyRootMotion;
        animator.CrossFade(targetAnimation, 0.2f);
        player.isPerformingAction = isPerformingAction;
        player.canRotate = canRotate;
        player.canMove = canMove;
    }

    public void ApplyJumpingVelocity()
    {
        player.playerLocomotion.yVelocity.y = Mathf.Sqrt(player.playerLocomotion.jumpHeight * -2 * player.playerLocomotion.gravityForce);
    }

    public void CanRotate()
    {
        player.canRotate = true;
    }

    public void StopRotate()
    {
        player.canRotate = false;
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
    public void EnableIsParrying()
    {
        player.isParrying = true;
    }

    public void DisableIsParrying()
    {
        player.isParrying = false;
    }

    public override void TakeCriticalDamageAnimationEvent()
    {
        player.playerStatsManager.TakeDamageNoAnimation(player.pendingCriticalDamage);
        player.pendingCriticalDamage = 0;
    }
}
