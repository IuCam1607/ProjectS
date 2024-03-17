using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellDamageCollider : DamageCollider
{
    public GameObject impactParticles;
    public GameObject projectileParticles;
    public GameObject muzzleParticles;
    public AudioSource fireBallSFX;

    private bool hasCollided = false;

    CharacterStatsManager spellTarget;
    Rigidbody rigidbody;
    private Vector3 impactNormal;

    protected override void Awake()
    {
        damageCollider = GetComponent<Collider>();
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        projectileParticles = Instantiate(projectileParticles, transform.position, transform.rotation);
        projectileParticles.transform.parent = transform;

        if (muzzleParticles)
        {
            muzzleParticles = Instantiate(muzzleParticles, transform.position, transform.rotation);
            Destroy(muzzleParticles, 2f);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!hasCollided)
        {
            spellTarget = other.transform.GetComponent<CharacterStatsManager>();

            if (spellTarget != null)
            {
                if (spellTarget.teamIDNumber != teamIDNumber)
                {
                    spellTarget.TakeDamage(0, fireDamage, currentDamageAnimation);
                }
            }
 

            hasCollided = true;
            impactParticles = Instantiate(impactParticles, transform.position, Quaternion.FromToRotation(Vector3.up, impactNormal));
            fireBallSFX.PlayOneShot(fireBallSFX.clip);

            Destroy(projectileParticles);
            Destroy(impactParticles, 5f);
            Destroy(gameObject, 5f);
        }
    }
}
