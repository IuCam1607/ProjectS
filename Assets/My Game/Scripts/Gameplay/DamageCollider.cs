using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollider : MonoBehaviour
{
    public CharacterManager characterManager;

    public GameObject weaponTrail;

    public bool enableDamageColliderOnStartUp = false;

    [Header("Team I.D")]
    public int teamIDNumber = 0;

    [Header("Collider")]
    protected Collider damageCollider;

    [Header("Poise")]
    public float poiseBreak;
    public float offensivePoiseDefence;

    [Header("Damage")]
    public int physicalDamage;
    public int fireDamage;
    public int magicDamage;
    public int lightningDamage;
    public int darkDamage;

    [Header("Stamina Damage")]
    public int staminaDamage;

    protected virtual void Awake()
    {
        damageCollider = GetComponent<Collider>();
        damageCollider.gameObject.SetActive(true);
        damageCollider.isTrigger = true;
        damageCollider.enabled = enableDamageColliderOnStartUp;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Character")
        {
            CharacterStatsManager enemyStats = collision.GetComponent<CharacterStatsManager>();
            CharacterManager enemyCharacterManager = collision.GetComponent<CharacterManager>();
            CharacterEffectManager enemyEffectManager = collision.GetComponent<CharacterEffectManager>();
            BlockingCollider shield = collision.GetComponentInChildren<BlockingCollider>();

            if (enemyStats.isDead)
                return;

            if (enemyCharacterManager != null)
            {
                if (shield != null && enemyCharacterManager.isBlocking)
                {
                    float physicalDamageAfterBlock = physicalDamage - (physicalDamage * shield.blockingPhysicalDamageAbsorption) / 100;
                    float fireDamageAfterBlock = fireDamage - (fireDamage * shield.blockingFireDamageAbsorption) / 100;

                    if (enemyStats != null)
                    {
                        enemyStats.TakeDamage(Mathf.RoundToInt(physicalDamageAfterBlock), 0, "Block Guard");
                        return;
                    }
                }
            }

            if (enemyStats != null)
            {
                if (enemyStats.teamIDNumber == teamIDNumber)
                    return;

                enemyStats.poiseResetTimer = enemyStats.totalPoiseResetTime;
                enemyStats.totalPoiseDefence = enemyStats.totalPoiseDefence - poiseBreak;
                Debug.Log("Player's Poise is currently: " + enemyStats.totalPoiseDefence);

                Vector3 contactPoint = collision.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);
                enemyEffectManager.PlayBloodSplatterFX(contactPoint);

                if (enemyStats.totalPoiseDefence > poiseBreak)
                {
                    enemyStats.TakeDamageNoAnimation(physicalDamage, fireDamage);
                }
                else
                {
                    enemyStats.TakeDamage(physicalDamage, 0);
                }
                Debug.Log("Player Take Damage");

            }
        }
    }
    //    else if (collision.tag == "Enemy")
    //    {
    //        EnemyStatsManager enemyStats = collision.GetComponent<EnemyStatsManager>();
    //        CharacterManager enemyCharacterManager = collision.GetComponent<CharacterManager>();
    //        CharacterEffectManager enemyEffectManager = collision.GetComponent<CharacterEffectManager>();
    //        BlockingCollider shield = collision.GetComponentInChildren<BlockingCollider>();
            
    //        if (enemyStats.isDead)
    //            return;

    //        if (enemyCharacterManager != null)
    //        {
    //            if (shield != null && enemyCharacterManager.isBlocking)
    //            {
    //                float physicalDamageAfterBlock = physicalDamage - (physicalDamage * shield.blockingPhysicalDamageAbsorption) / 100;

    //                if (enemyStats != null)
    //                {
    //                    enemyStats.TakeDamage(Mathf.RoundToInt(physicalDamageAfterBlock), "Block Guard");
    //                    return;
    //                }
    //            }
    //        }

    //        if (enemyStats != null)
    //        {
    //            enemyStats.poiseResetTimer = enemyStats.totalPoiseResetTime;
    //            enemyStats.totalPoiseDefence = enemyStats.totalPoiseDefence - poiseBreak;
    //            Debug.Log("Enemy's Poise is currently: " + enemyStats.totalPoiseDefence);

    //            Vector3 contactPoint = collision.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);
    //            enemyEffectManager.PlayBloodSplatterFX(contactPoint);

    //            if (enemyStats.isBoss)
    //            {
    //                if (enemyStats.totalPoiseDefence > poiseBreak)
    //                {
    //                    enemyStats.TakeDamageNoAnimation(physicalDamage);
    //                }
    //                else
    //                {
    //                    enemyStats.TakeDamageNoAnimation(physicalDamage);
    //                    enemyStats.GuardBreak();
    //                }
    //            }
    //            else
    //            {
    //                if (enemyStats.totalPoiseDefence > poiseBreak)
    //                {
    //                    enemyStats.TakeDamageNoAnimation(physicalDamage);
    //                }
    //                else
    //                {
    //                    enemyStats.TakeDamage(physicalDamage);
    //                }
    //            }

    //            Debug.Log("Enemy Take Damage");
    //        }
    //    }
    //}

    public virtual void EnableDamageCollider()
    {
        if (damageCollider != null)
        {
            damageCollider.enabled = true;

            if (weaponTrail != null)
            {
                weaponTrail.SetActive(true);
            }
        }
    }

    public virtual void DisableDamageCollider()
    {
        if (damageCollider != null)
        {
            damageCollider.enabled = false;

            if (weaponTrail != null)
            {
                weaponTrail.SetActive(false);
            }
        } 
    }


}
