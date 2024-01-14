using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    private PlayerInputManager playerInput;
    private PlayerManager player;

    int vertical;
    int horizontal;

    private void Awake()
    {
        player = GetComponent<PlayerManager>();
        playerInput = GetComponent<PlayerInputManager>();

        vertical = Animator.StringToHash("Vertical");
        horizontal = Animator.StringToHash("Horizontal");
    }

    private void OnAnimatorMove()
    {
        if(player.applyRootMotion)
        {
            Vector3 velocity = player.animator.deltaPosition;
            player.characterController.Move(velocity);
            player.transform.rotation *= player.animator.deltaRotation;
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
        player.animator.SetFloat(horizontal, horizontalAmount, 0.1f, Time.deltaTime);
        player.animator.SetFloat(vertical, verticalAmount, 0.1f, Time.deltaTime);
    }

    public void PlayTargetActionAnimation(string targetAnimation, bool isPerformingAction, bool applyRootMotion = true, bool canRotate = false, bool canMove = false)
    {
        player.applyRootMotion = applyRootMotion;
        player.animator.CrossFade(targetAnimation, 0.2f);
        player.isPerformingAction = isPerformingAction;
        player.canRotate = canRotate;
        player.canMove = canMove;
    }
}
