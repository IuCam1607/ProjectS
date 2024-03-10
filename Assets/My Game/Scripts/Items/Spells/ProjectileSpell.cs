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
        PlayerWeaponSlotManager weaponSlotManager)
    {
        base.AttempToCastSpell(playerAnimator, playerStats, weaponSlotManager);
        GameObject instantiatedWarmUpSpellFX = Instantiate(spellWarmUpFX, weaponSlotManager.rightHandSlot.transform);
        instantiatedWarmUpSpellFX.gameObject.transform.localPosition = new Vector3(100, 100, 100);
        playerAnimator.PlayTargetActionAnimation(spellAnimation, true);
    }

    public override void SuccessfullyCastSpell(PlayerAnimatorManager playerAnimator, 
        PlayerStatsManager playerStats, 
        PlayerCamera playerCamera, 
        PlayerWeaponSlotManager weaponSlotManager)
    {
        base.SuccessfullyCastSpell(playerAnimator, playerStats, playerCamera, weaponSlotManager);
        GameObject instantiatedSpellFX = Instantiate(spellCastFX, weaponSlotManager.rightHandSlot.transform.position, playerCamera.cameraPivotTransform.rotation);
        rigidbody = instantiatedSpellFX.GetComponent<Rigidbody>();
        //spellDamageCollider = instantiatedSpellFX.GetComponent<SpellDamageCollider>();

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
}
