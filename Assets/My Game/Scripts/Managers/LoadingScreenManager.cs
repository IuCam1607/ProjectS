using MoreMountains.Feedbacks;
using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreenManager : MonoBehaviour
{
    public static LoadingScreenManager instance;

    public GameObject loadingScreen;
    public Slider loadingSlider;
    private MMF_Player feedback;
    private PlayerManager player;

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

        feedback = GetComponent<MMF_Player>();
        player = FindAnyObjectByType<PlayerManager>();
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void ToggleLoadingScreen(bool active)
    {
        loadingScreen.SetActive(active);
    }

    public void UpdateLoadingProgress(float progress)
    {
        loadingSlider.value = progress;
    }

    public void LoadScene(string sceneName)
    {
        MMF_LoadScene loadScene = feedback.GetFeedbackOfType<MMF_LoadScene>();
        loadScene.DestinationSceneName = sceneName;
        feedback.PlayFeedbacks();
        player.playerStatsManager.RefreshHUD();
    }
}
