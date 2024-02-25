using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollider : MonoBehaviour
{
    CharacterManager characterManager;

    [Header("Collider")]
    protected Collider damageCollider;

    [Header("Damage")]
    public int currentWeaponDamage = 20;

    private void Awake()
    {
        damageCollider = GetComponent<Collider>();
        damageCollider.gameObject.SetActive(true);
        damageCollider.isTrigger = true;
        damageCollider.enabled = false;

        characterManager = GetComponentInParent<CharacterManager>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
        {
            PlayerStatsManager playerStats = collision.GetComponent<PlayerStatsManager>();
            CharacterManager enemyCharacterManager = collision.GetComponent<CharacterManager>();

            if (enemyCharacterManager != null)
            {
                if (enemyCharacterManager.isParrying)
                {
                    //characterManager.GetComponentInChildren<AnimatorManager>()./*PlayTar*/
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

            if (enemyStats != null)
            {
                enemyStats.TakeDamage(currentWeaponDamage);
                Debug.Log("Enemy Take Damage");
            }
        }
    }

    public virtual void EnableDamageCollider()
    {
        damageCollider.enabled = true;
    }

    public virtual void DisableDamageCollider()
    {
        if (damageCollider != null)
        {
            damageCollider.enabled = false;
        } 
    }
}
