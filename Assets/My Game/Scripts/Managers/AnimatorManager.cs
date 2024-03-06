using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    public CharacterManager characterManager;
    public Animator animator;


    private void Awake()
    {
        //characterManager = GetComponentInParent<CharacterManager>();
    }

    public virtual void TakeCriticalDamageAnimationEvent()
    {

    }

    public virtual void PlayTargetActionAnimation(string targetAnimation, bool isPerformingAction, bool applyRootMotion = true, bool canRotate = false, bool canMove = false)
    {
        characterManager.applyRootMotion = applyRootMotion;
        animator.CrossFade(targetAnimation, 0.2f);
        characterManager.isPerformingAction = isPerformingAction;
        animator.SetBool("canRotate", canRotate);
        //characterManager.canRotate = canRotate;
        characterManager.canMove = canMove;
    }

    public void PlayTargetAnimationWithRootRotation(string targetAnimation, bool isPerformingAction, bool applyRootMotion = true)
    {
        characterManager.applyRootMotion = applyRootMotion;
        animator.CrossFade(targetAnimation, 0.2f);
        animator.SetBool("isRotatingWithRootMotion", true);
        animator.SetBool("isInteracting", true);
        characterManager.isPerformingAction = isPerformingAction;
    }
}
