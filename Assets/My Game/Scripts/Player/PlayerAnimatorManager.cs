using GLTF.Schema;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorManager : AnimatorManager
{
    private PlayerInputManager playerInput;
    private PlayerManager player;


    int vertical;
    int horizontal;

    private void Awake()
    {
        player = GetComponent<PlayerManager>();
        playerInput = GetComponent<PlayerInputManager>();
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

    public void EnebleCombo()
    {
        animator.SetBool("canDoCombo", true);
    }

    public void DisableCombo()
    {
        animator.SetBool("canDoCombo", false);
    }
}
