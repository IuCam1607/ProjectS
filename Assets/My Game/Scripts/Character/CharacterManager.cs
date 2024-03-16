using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public CharacterWeaponSlotManager characterWeaponSlotManager;
    public CharacterStatsManager characterStatsManager;
    public AnimatorManager animatorManager;
    public CharacterInventoryManager characterInventoryManager;
    public CharacterSoundFXManager characterSoundFXManager;
    public Animator animator;

    public MMF_Player feedBack;
    public MMF_MMSoundManagerSound sfx;

    public GameObject footStepManager;

    [Header("Lock On Transform")]
    public Transform lockOnTransform;

    [Header("Combat Colliders")]
    public CriticalDamageCollider backStabCollider;
    public CriticalDamageCollider riposteCollider;

    [Header("Character Flags")]
    public bool isPerformingAction = false;
    public bool canRotate = true;
    public bool canMove = true;
    public bool applyRootMotion = false;
    public bool isInteracting = false;
    public bool isJumping = false;
    public bool isGrounded = true;
    public bool inventoryFlag;
    public bool isRolling = false;
    public bool isTwoHandingWeapon;


    [Header("Combat Flags")]
    public bool canBeRiposted;
    public bool canbeParried;
    public bool isBlocking;
    public bool isInvulnerable;
    public bool canDoCombo = false;
    public bool isUsingRightHand;
    public bool isUsingLeftHand;

    [Header("Status")]
    public bool isDead = false;

    [Header("Movement Flags")]
    public bool isRotatingWithRootMotion;

    [Header("Spells")]
    public bool isFiringSpell;

    public int pendingCriticalDamage;

    protected virtual void Awake()
    {
        characterSoundFXManager = GetComponent<CharacterSoundFXManager>();
        characterInventoryManager = GetComponent<CharacterInventoryManager>();
        characterWeaponSlotManager = GetComponent<CharacterWeaponSlotManager>();
        animatorManager = GetComponent<AnimatorManager>();
        characterStatsManager = GetComponent<CharacterStatsManager>();
    }

    public virtual void PlaySFX(AudioClip audio, float delay = 0)
    {
        sfx.Timing.InitialDelay = delay;
        sfx.Sfx = audio;
        feedBack?.PlayFeedbacks();
    }

    public virtual void PlaySFXNoDelay(AudioClip audio)
    {
        sfx.Sfx = audio;
        feedBack?.PlayFeedbacks();
    }

    public virtual void PlaySFX(AudioFeedBack audioClip)
    {
        sfx.Timing.InitialDelay = audioClip.delay;
        sfx.MinVolume = audioClip.minVolume;
        sfx.MaxVolume = audioClip.maxVolume;
        sfx.Sfx = audioClip.audio;
        feedBack?.PlayFeedbacks();
    }

    //protected virtual void FixedUpdate()
    //{

    //}

    //public virtual void UpdateWhichHandCharacterIsUsing(bool usingRightHand)
    //{
    //    if (usingRightHand)
    //    {
    //        isUsingRightHand = true;
    //        isUsingLeftHand = false;
    //    }
    //    else
    //    {
    //        isUsingLeftHand = true;
    //        isUsingRightHand = false;
    //    }
    //}
}
