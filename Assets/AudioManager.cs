using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource audioSource;
    public AudioClip bgm;
    public AudioClip menuBgm;
    public AudioClip bossBgm;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        MenuScreenBGM();
    }

    public void PlayBGM(bool isBoss = false)
    {
        if (isBoss)
        {
            while(audioSource.volume > 0)
            {
                audioSource.volume -= Time.deltaTime;

                if (audioSource.volume <= 0)
                {
                    audioSource.volume = 0.4f;
                    audioSource.clip = bossBgm;
                    audioSource.Play();
                    break;
                }
            }
        }
        else
        {
            audioSource.clip = bgm;
            audioSource.volume = 0.4f;
            audioSource.Play();
        }
    }

    public void StopBGM()
    {
        while (audioSource.volume > 0)
        {
            audioSource.volume -= Time.deltaTime;

            if (audioSource.volume <= 0)
            {
                audioSource.Stop();
                break;
            }
        }
    }

    public void MenuScreenBGM()
    {
        while (audioSource.volume > 0)
        {
            audioSource.volume -= Time.deltaTime;

            if (audioSource.volume <= 0)
            {
                audioSource.clip = menuBgm;
                audioSource.volume = 0.4f;
                audioSource.Play();
            }
        }
    }
}
