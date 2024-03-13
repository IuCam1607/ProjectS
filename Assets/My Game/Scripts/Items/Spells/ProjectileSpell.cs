using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Spells/Projectile Spell")]
public class ProjectileSpell : SpellItem
{
    [Header("Projectile Damage")]
    public float baseDamage;

    [Header("Projectile Physics")]
    public float projectileForwardVelocity;
    public float projectileUpwwardVelocity;
    public float projectileMass;
    public bool isEffectedByGravity;
    Rigidbody rigidbody;

    public override void AttempToCastSpell(PlayerAnimatorManager playerAnimator, 
        PlayerStatsManager playerStats, 
        PlayerWeaponSlotManager weaponSlotManager,
        bool isLeftHanded)
    {
        base.AttempToCastSpell(playerAnimator, playerStats, weaponSlotManager, isLeftHanded);
        if (isLeftHanded)
        {
            GameObject instantiatedWarmUpSpellFX = Instantiate(spellWarmUpFX, weaponSlotManager.leftHandSlot.transform);
            instantiatedWarmUpSpellFX.gameObject.transform.localPosition = new Vector3(100, 100, 100);
            playerAnimator.PlayTargetActionAnimation(spellAnimation, true, true, false, false, isLeftHanded);
        }
        else
        {
            GameObject instantiatedWarmUpSpellFX = Instantiate(spellWarmUpFX, weaponSlotManager.rightHandSlot.transform);
            instantiatedWarmUpSpellFX.gameObject.transform.localPosition = new Vector3(100, 100, 100);
            playerAnimator.PlayTargetActionAnimation(spellAnimation, true, true, false, false, isLeftHanded);
        }

    }

    public override void SuccessfullyCastSpell(PlayerAnimatorManager playerAnimator, 
        PlayerStatsManager playerStats, 
        PlayerCamera playerCamera, 
        PlayerWeaponSlotManager weaponSlotManager,
        bool isLeftHanded)
    {
        base.SuccessfullyCastSpell(playerAnimator, playerStats, playerCamera, weaponSlotManager, isLeftHanded);
        GameObject instantiatedSpellFX;

        if (isLeftHanded)
        {
            instantiatedSpellFX = Instantiate(spellCastFX, weaponSlotManager.leftHandSlot.transform.position, playerCamera.cameraPivotTransform.rotation);
            SpellDamageCollider spellDamageCollider = instantiatedSpellFX.GetComponent<SpellDamageCollider>();
            spellDamageCollider.teamIDNumber = playerStats.teamIDNumber;
            rigidbody = instantiatedSpellFX.GetComponent<Rigidbody>();

            if (playerCamera.currentLockOnTarget != null)
            {
                instantiatedSpellFX.transform.LookAt(playerCamera.currentLockOnTarget.transform);
            }
            else
            {
                instantiatedSpellFX.transform.rotation = Quaternion.Euler(playerCamera.cameraPivotTransform.eulerAngles.x, playerStats.transform.eulerAngles.y, 0);
            }


            rigidbody.AddForce(instantiatedSpellFX.transform.forward * projectileForwardVelocity);
            rigidbody.AddForce(instantiatedSpellFX.transform.up * projectileUpwwardVelocity);
            rigidbody.useGravity = isEffectedByGravity;
            rigidbody.mass = projectileMass;
            instantiatedSpellFX.transform.parent = null;
        }
        else
        {
            instantiatedSpellFX = Instantiate(spellCastFX, weaponSlotManager.rightHandSlot.transform.position, playerCamera.cameraPivotTransform.rotation);
            SpellDamageCollider spellDamageCollider = instantiatedSpellFX.GetComponent<SpellDamageCollider>();
            spellDamageCollider.teamIDNumber = playerStats.teamIDNumber;
            rigidbody = instantiatedSpellFX.GetComponent<Rigidbody>();

            if (playerCamera.currentLockOnTarget != null)
            {
                instantiatedSpellFX.transform.LookAt(playerCamera.currentLockOnTarget.transform);
            }
            else
            {
                instantiatedSpellFX.transform.rotation = Quaternion.Euler(playerCamera.cameraPivotTransform.eulerAngles.x, playerStats.transform.eulerAngles.y, 0);
            }


            rigidbody.AddForce(instantiatedSpellFX.transform.forward * projectileForwardVelocity);
            rigidbody.AddForce(instantiatedSpellFX.transform.up * projectileUpwwardVelocity);
            rigidbody.useGravity = isEffectedByGravity;
            rigidbody.mass = projectileMass;
            instantiatedSpellFX.transform.parent = null;
        }

        //spellDamageCollider = instantiatedSpellFX.GetComponent<SpellDamageCollider>();


    }
}
