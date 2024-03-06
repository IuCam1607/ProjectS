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

    public bool isPerformingAction = false;
    public bool canRotate = true;
    public bool canMove = true;
    public bool applyRootMotion = false;
    public bool isInteracting = false;

    [Header("Combat Flags")]
    public bool canBeRiposted;
    public bool canbeParried;
    public bool isBlocking;

    [Header("Movement Flags")]
    public bool isRotatingWithRootMotion;

    [Header("Spells")]
    public bool isFiringSpell;

    public int pendingCriticalDamage;
}
