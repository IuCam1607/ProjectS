using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollider : MonoBehaviour
{
    CharacterManager characterManager;

    public bool enableDamageColliderOnStartUp = false;

    [Header("Collider")]
    protected Collider damageCollider;

    [Header("Damage")]
    public int currentWeaponDamage = 20;

    private void Awake()
    {
        damageCollider = GetComponent<Collider>();
        damageCollider.gameObject.SetActive(true);
        damageCollider.isTrigger = true;
        damageCollider.enabled = enableDamageColliderOnStartUp;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
        {
            PlayerStatsManager playerStats = collision.GetComponent<PlayerStatsManager>();
            CharacterManager enemyCharacterManager = collision.GetComponent<CharacterManager>();
            BlockingCollider shield = collision.GetComponentInChildren<BlockingCollider>();

            if (enemyCharacterManager != null)
            {
                if (shield != null && enemyCharacterManager.isBlocking)
                {
                    float physicalDamageAfterBlock = currentWeaponDamage - (currentWeaponDamage * shield.blockingPhysicalDamageAbsorption) / 100;

                    if (playerStats != null)
                    {
                        playerStats.TakeDamage(Mathf.RoundToInt(physicalDamageAfterBlock), "Block Guard");
                        return;
                    }
                }
            }

            if (playerStats != null)
            {
                playerStats.TakeDamage(currentWeaponDamage);
            }
        }
        else if (collision.tag == "Enemy")
        {
            EnemyStatsManager enemyStats = collision.GetComponent<EnemyStatsManager>();
            CharacterManager enemyCharacterManager = collision.GetComponent<CharacterManager>();
            BlockingCollider shield = collision.GetComponentInChildren<BlockingCollider>();

            if (enemyCharacterManager != null)
            {
                if (shield != null && enemyCharacterManager.isBlocking)
                {
                    float physicalDamageAfterBlock = currentWeaponDamage - (currentWeaponDamage * shield.blockingPhysicalDamageAbsorption) / 100;

                    if (enemyStats != null)
                    {
                        enemyStats.TakeDamage(Mathf.RoundToInt(physicalDamageAfterBlock), "Block Guard");
                        return;
                    }
                }
            }

            if (enemyStats != null)
            {
                enemyStats.TakeDamage(currentWeaponDamage);
                Debug.Log("Enemy Take Damage");
            }
        }
    }

    public virtual void EnableDamageCollider()
    {
        if (damageCollider != null)
        {
            damageCollider.enabled = true;
        }
    }

    public virtual void DisableDamageCollider()
    {
        if (damageCollider != null)
        {
            damageCollider.enabled = false;
        } 
    }
}
