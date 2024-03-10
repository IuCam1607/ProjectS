using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellDamageCollider : DamageCollider
{
    public GameObject impactParticles;
    public GameObject projectileParticles;
    public GameObject muzzleParticles;

    private bool hasCollided = false;

    CharacterStatsManager spellTarget;
    Rigidbody rigidbody;
    private Vector3 impactNormal;

    private void Awake()
    {
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

            if (spellTarget != null && spellTarget.teamIDNumber != teamIDNumber)
            {
                spellTarget.TakeDamage(0, fireDamage);
            }

            hasCollided = true;
            impactParticles = Instantiate(impactParticles, transform.position, Quaternion.FromToRotation(Vector3.up, impactNormal));

            Destroy(projectileParticles);
            Destroy(impactParticles, 5f);
            Destroy(gameObject, 5f);
        }
    }
}
