using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldSaveGameManager : MonoBehaviour
{
    public static WorldSaveGameManager instance;

    [SerializeField] int worldSceneIndex = 1;
    public PlayerManager player;

    [Header("Save Data Writer")]
    SaveGameDataWriter saveGameDataWriter;

    [Header("Current Character Data")]
    public CharacterSaveData currentCharacterSaveData;
    [SerializeField] private string fileName;

    [Header("SAVE/LOAD")]
    public bool saveGame;
    public bool loadGame;

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

        if (player == null)
        {
            player = FindAnyObjectByType<PlayerManager>();
        }
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject); // cai nay co can dontdestroy ko
    }

    private void Update()
    {
        if (saveGame)
        {
            saveGame = false;
            SaveGame();
        }

        if (loadGame)
        {
            loadGame = false;
            LoadGame();
        }
    }


    public void SaveGame()
    {
        //saveGameDataWriter = new SaveGameDataWriter();
        //saveGameDataWriter.saveDataDirectoryPath = Application.persistentDataPath;
        //saveGameDataWriter.dataSaveFileName = fileName;

        player.SaveCharacterDataToCurrentSaveData(ref currentCharacterSaveData);

        //saveGameDataWriter.WriteCharacterDataToSaveFile(currentCharacterSaveData);

        Debug.Log("Saving game...");
        Debug.Log("File saved as: " + fileName);
    }

    public void LoadGame()
    {
        //saveGameDataWriter = new SaveGameDataWriter();
        //saveGameDataWriter.saveDataDirectoryPath = Application.persistentDataPath;
        //saveGameDataWriter.dataSaveFileName = fileName;
        //currentCharacterSaveData = saveGameDataWriter.LoadCharacterDataFormJson();

        StartCoroutine(LoadWorldScreenAsynchronously());
    }

    public IEnumerator LoadWorldScreenAsynchronously()
    {
        if (player == null)
        {
            player = FindAnyObjectByType<PlayerManager>();
        }

        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(worldSceneIndex);


        while (!loadOperation.isDone)
        {
            float loadProgress = Mathf.Clamp01(loadOperation.progress / 0.9f);
            LoadingScreenManager.instance.ToggleLoadingScreen(true);
            LoadingScreenManager.instance.UpdateLoadingProgress(loadProgress);
            yield return null;
        }
        AudioManager.instance.PlayBGM(false);

        player.LoadCharacterDataFromCurrentCharacterSaveData(ref currentCharacterSaveData);
        LoadingScreenManager.instance.ToggleLoadingScreen(false);

    }
}
