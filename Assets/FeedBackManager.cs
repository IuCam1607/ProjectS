using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedBackManager : MonoBehaviour
{
    private MMF_Player feedBack;
    private MMF_MMSoundManagerSound sfx;
    public MMF_Player footFeedBack;

    public AudioFeedBack rollSFX;
    public AudioFeedBack landSFX;
    public AudioFeedBack hitSFX;
    public AudioFeedBack drinkPotionSFX;
    public AudioFeedBack deadSFX;
    public AudioFeedBack roarSFX;


    private void Start()
    {
        feedBack = GetComponent<MMF_Player>();
        sfx = feedBack.GetFeedbackOfType<MMF_MMSoundManagerSound>();
    }

    public void PlaySFX(AudioClip audio)
    {
        sfx.Sfx = audio;
        feedBack?.PlayFeedbacks();
    }

    public void PlayFootStepSFX()
    {
        footFeedBack?.PlayFeedbacks();
    }


}
