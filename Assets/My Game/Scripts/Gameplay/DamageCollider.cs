using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollider : MonoBehaviour
{
    [Header("Collider")]
    protected Collider damageCollider;

    [Header("Damage")]
    public float physicalDamage = 0;
    public float magicDamage = 0;
    public float fireDamage = 0;
    public float lightningDamage = 0;
    public float holyDamage = 0;

    [Header("Contact Point")]
    protected Vector3 contactPoint;

    [Header("Characters Damaged")]
    protected List<PlayerManager> playerDamaged = new List<PlayerManager>();

    private void Awake()
    {
        damageCollider = GetComponent<Collider>();
        damageCollider.gameObject.SetActive(true);
        damageCollider.isTrigger = true;
        damageCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerManager damageTarget = other.GetComponent<PlayerManager>();

        if(damageTarget != null )
        {
            contactPoint = other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);


            DamageTarget(damageTarget);
        }
    }

    protected virtual void DamageTarget(PlayerManager damageTarget)
    {
        if (playerDamaged.Contains(damageTarget))
        {
            return;
        }

        playerDamaged.Add(damageTarget);

        TakeDamageEffect damageEffect = Instantiate(WorldCharacterEffectsManager.instance.takeDamageEffect);
        damageEffect.physicalDamage = physicalDamage;
        damageEffect.magicDamage = magicDamage;
        damageEffect.fireDamage = fireDamage;
        damageEffect.lightningDamage = lightningDamage;
        damageEffect.holyDamage = holyDamage;
        damageEffect.contactPoint = contactPoint;

        damageTarget.playerEffectManager.ProcessInstantEffect(damageEffect);
    }

    //private void OnTriggerEnter(Collider collision)
    //{
    //    if (collision.tag == "Player")
    //    {
    //        PlayerStatsManager playerStatsManager = collision.GetComponent<PlayerStatsManager>();


    //        if(playerStatsManager != null )
    //        {
    //            playerStatsManager.TakeDamage(currentWeaponDamage);
    //        }
    //    }

    //    if (collision.tag == "Enemy")
    //    {
    //        EnemyStatsManager enemyStats = collision.GetComponent<EnemyStatsManager>();

    //        if (enemyStats != null)
    //        {
    //            enemyStats.TakeDamage(currentWeaponDamage);
    //            Debug.Log("Enemy TakeDamage");
    //        }
    //    }
    //}


    public virtual void EnableDamageCollider()
    {
        damageCollider.enabled = true;
    }

    public virtual void DisableDamageCollider()
    {
        damageCollider.enabled = false;
        playerDamaged.Clear();
    }
}
