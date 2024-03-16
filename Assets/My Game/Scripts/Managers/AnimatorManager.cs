using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AnimatorManager : MonoBehaviour
{
    protected CharacterManager character;

    protected virtual void Awake()
    {
        character = GetComponent<CharacterManager>();
    }

    public virtual void TakeCriticalDamageAnimationEvent()
    {
        character.characterStatsManager.TakeDamageNoAnimation(character.pendingCriticalDamage, 0);
        character.pendingCriticalDamage = 0;
    }

    public virtual void PlayTargetActionAnimation(string targetAnimation, bool isPerformingAction, bool applyRootMotion = true, bool canRotate = false, bool canMove = false, bool mirrorAnim = false)
    {
        character.applyRootMotion = applyRootMotion;
        character.animator.CrossFade(targetAnimation, 0.2f);
        character.isPerformingAction = isPerformingAction;
        character.animator.SetBool("canRotate", canRotate);
        character.animator.SetBool("isMirrored", mirrorAnim);
        //characterManager.canRotate = canRotate;
        character.canMove = canMove;
    }

    public void PlayTargetAnimationWithRootRotation(string targetAnimation, bool isPerformingAction, bool applyRootMotion = true)
    {
        character.applyRootMotion = applyRootMotion;
        character.animator.CrossFade(targetAnimation, 0.2f);
        character.animator.SetBool("isRotatingWithRootMotion", true);
        character.animator.SetBool("isInteracting", true);
        character.isPerformingAction = isPerformingAction;
    }

    public virtual void CanRotate()
    {
        character.animator.SetBool("canRotate", true);
    }

    public virtual void StopRotate()
    {
        character.animator.SetBool("canRotate", false);
    }

    public virtual void EnebleCombo()
    {
        character.animator.SetBool("canDoCombo", true);
    }

    public virtual void DisableCombo()
    {
        character.animator.SetBool("canDoCombo", false);
    }

    public virtual void EnableIsInvulnerable()
    {
        character.animator.SetBool("isInvulnerable", true);
    }

    public virtual void DisableIsInvulnerable()
    {
        character.animator.SetBool("isInvulnerable", false);
    }

    public virtual void EnableCanBeRiposted()
    {
        character.canBeRiposted = true;
    }

    public virtual void DisableCanBeRiposted()
    {
        character.canBeRiposted = false;
    }
}
