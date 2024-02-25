using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character Effects/Instant Effects/Take Damage")]
public class TakeDamageEffect : InstantCharacterEffect
{
    public HealthBar healthBar;

    [Header("Player Causing Damage")]
    public PlayerManager playerCausingDamage;

    [Header("Damage")]
    public float physicalDamage = 0;
    public float magicDamage = 0;
    public float fireDamage = 0;
    public float lightningDamage = 0;
    public float holyDamage = 0;

    [Header("Final Damage")]
    private int finalDamageDealt = 0;

    [Header("Poise")]
    public float poiseDamage = 0;
    public bool poiseIsBroken = false;

    [Header("Animation")]
    public bool playDamageAnimation = true;
    public bool manuallySelectDamageAnimation = false;
    public string damageAnimation;

    [Header("Sound FX")]
    public bool willPlayDamageSFX = true;
    public AudioClip elementalDamageSoundFX;

    [Header("Direction Damage Taken From")]
    public float angleHitFrom;
    public Vector3 contactPoint;

    private void Awake()
    {
        healthBar = FindAnyObjectByType<HealthBar>();
    }

    public override void ProcessEffect(PlayerManager player)
    {
        base.ProcessEffect(player);
        if (player.playerStatsManager.isDead)
            return;

        CalculateDamage(player);
    }

    private void CalculateDamage(PlayerManager player)
    {
        if (playerCausingDamage != null)
        {

        }

        finalDamageDealt = Mathf.RoundToInt(physicalDamage + magicDamage + fireDamage + lightningDamage + holyDamage);

        if (finalDamageDealt <= 0)
        {
            finalDamageDealt = 1;
        }

        Debug.Log("Final Damage Given:" + finalDamageDealt);

        player.playerStatsManager.currentHealth -= finalDamageDealt;

        Debug.Log("Player Current Health" + player.playerStatsManager.currentHealth);

        healthBar.SetCurrentHealth(player.playerStatsManager.currentHealth);
    }
}
