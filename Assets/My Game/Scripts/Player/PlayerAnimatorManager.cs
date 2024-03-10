using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorManager : AnimatorManager
{
    public PassThroughFogWall passThroughFogWall;
    private PlayerManager player;

    int vertical;
    int horizontal;

    protected override void Awake()
    {
        base.Awake();
        characterManager = GetComponent<CharacterManager>();
        player = GetComponent<PlayerManager>();
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
    public override void PlayTargetActionAnimation(string targetAnimation, bool isPerformingAction, bool applyRootMotion = true, bool canRotate = false, bool canMove = false)
    {
        base.PlayTargetActionAnimation(targetAnimation, isPerformingAction, applyRootMotion, canRotate, canMove);
        //player.applyRootMotion = applyRootMotion;
        //animator.CrossFade(targetAnimation, 0.2f);
        //player.isPerformingAction = isPerformingAction;
        //player.canRotate = canRotate;
        //player.canMove = canMove;
    }

    public void ApplyJumpingVelocity()
    {
        player.playerLocomotion.yVelocity.y = Mathf.Sqrt(player.playerLocomotion.jumpHeight * -2 * player.playerLocomotion.gravityForce);
    }


    public void EnableCollision()
    {
        passThroughFogWall.fogEntranceCollider.enabled = true;
        passThroughFogWall.fogWallCollider.enabled = true;
    }

    public void DisableCollision()
    {
        passThroughFogWall.fogEntranceCollider.enabled = false;
        passThroughFogWall.fogWallCollider.enabled = false;
    }
}
