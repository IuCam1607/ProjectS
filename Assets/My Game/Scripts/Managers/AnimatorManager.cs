using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XftWeapon;

public class AnimatorManager : MonoBehaviour
{
    protected CharacterStatsManager characterStatsManager;
    protected CharacterManager characterManager;
    public Animator animator;
    


    protected virtual void Awake()
    {
        characterManager = GetComponent<CharacterManager>();

    }

    public virtual void TakeCriticalDamageAnimationEvent()
    {
        characterStatsManager.TakeDamageNoAnimation(characterManager.pendingCriticalDamage, 0);
        characterManager.pendingCriticalDamage = 0;
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

    public virtual void CanRotate()
    {
        animator.SetBool("canRotate", true);
    }

    public virtual void StopRotate()
    {
        animator.SetBool("canRotate", false);
    }

    public virtual void EnebleCombo()
    {
        animator.SetBool("canDoCombo", true);
    }

    public virtual void DisableCombo()
    {
        animator.SetBool("canDoCombo", false);
    }

    public virtual void EnableIsInvulnerable()
    {
        animator.SetBool("isInvulnerable", true);
    }

    public virtual void DisableIsInvulnerable()
    {
        animator.SetBool("isInvulnerable", false);
    }

    public virtual void EnableCanBeRiposted()
    {
        characterManager.canBeRiposted = true;
    }

    public virtual void DisableCanBeRiposted()
    {
        characterManager.canBeRiposted = false;
    }

}
