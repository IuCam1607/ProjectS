using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSoundFXManager : MonoBehaviour
{
    CharacterManager characterManager;
    AudioSource audioSource;

    [Header("Taking Damage Sounds")]
    public AudioClip[] takingDamageSounds;
    public List<AudioClip> potentialDamageSounds;
    private AudioClip lastDamageSoundPlayed;

    [Header("Weapon Whooshes")]
    private List<AudioClip> potentialWeaponWhooshes;
    private AudioClip lastWeaponWhoosh;

    //protected virtual void Awake()
    //{
    //    audioSource = GetComponent<AudioSource>();
    //    characterManager = GetComponent<CharacterManager>();
    //}

    //public virtual void PlayRandomDamageSoundFX()
    //{
    //    potentialDamageSounds = new List<AudioClip>();

    //    foreach (var damageSound in takingDamageSounds)
    //    {
    //        if (damageSound != lastDamageSoundPlayed)
    //        {
    //            potentialDamageSounds.Add(damageSound);
    //        }
    //    }

    //    int randomValue = Random.Range(0, potentialDamageSounds.Count);
    //    lastDamageSoundPlayed = takingDamageSounds[randomValue];
    //    audioSource.PlayOneShot(takingDamageSounds[randomValue], 0.4f);
    //}

    //public virtual void PlayRandomWeaponWhoosh()
    //{
    //    potentialWeaponWhooshes = new List<AudioClip>();

    //    if (characterManager.isUsingRightHand)
    //    {
    //        foreach (var whooshSound in characterManager.characterInventoryManager.rightWeapon.weaponWhooshes)
    //        {
    //            if (whooshSound != lastWeaponWhoosh)
    //            {
    //                potentialDamageSounds.Add(whooshSound);
    //            }
    //        }

    //        int randomValue = Random.Range(0, potentialWeaponWhooshes.Count);
    //        lastWeaponWhoosh = characterManager.characterInventoryManager.rightWeapon.weaponWhooshes[randomValue];
    //        audioSource.PlayOneShot(characterManager.characterInventoryManager.rightWeapon.weaponWhooshes[randomValue], 0.4f);
    //    }
    //    else
    //    {
    //        foreach (var whooshSound in characterManager.characterInventoryManager.leftWeapon.weaponWhooshes)
    //        {
    //            if (whooshSound != lastWeaponWhoosh)
    //            {
    //                potentialDamageSounds.Add(whooshSound);
    //            }
    //        }

    //        int randomValue = Random.Range(0, potentialWeaponWhooshes.Count);
    //        lastWeaponWhoosh = characterManager.characterInventoryManager.leftWeapon.weaponWhooshes[randomValue];
    //        audioSource.PlayOneShot(characterManager.characterInventoryManager.leftWeapon.weaponWhooshes[randomValue], 0.4f);
    //    }
    //}
}
