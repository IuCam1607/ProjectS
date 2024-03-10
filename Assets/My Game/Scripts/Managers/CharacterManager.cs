using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
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


    [Header("Combat Flags")]
    public bool canBeRiposted;
    public bool canbeParried;
    public bool isBlocking;
    public bool isInvulnerable;
    public bool canDoCombo = false;
    public bool isUsingRightHand;
    public bool isUsingLeftHand;

    [Header("Movement Flags")]
    public bool isRotatingWithRootMotion;

    [Header("Spells")]
    public bool isFiringSpell;

    public int pendingCriticalDamage;
}
