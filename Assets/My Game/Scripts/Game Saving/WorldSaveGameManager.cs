using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldSaveGameManager : MonoBehaviour
{
    public static WorldSaveGameManager instance;

    [SerializeField] int worldSceneIndex = 1;

    public PlayerManager player;

    [Header("Save/Load")]
    [SerializeField] bool saveGame;
    [SerializeField] bool loadGame;

    [Header("Save Data Writer")]
    private SaveFileDataWriter saveFileDataWriter;

    [Header("Current Character Data")]
    public CharacterSlot currentCharacterSlotBeingUsed;
    public CharacterSaveData currentCharacterData;
    private string saveFileName;

    [Header("Character Slots")]
    public CharacterSaveData characterSlot01;
    public CharacterSaveData characterSlot02;
    public CharacterSaveData characterSlot03;
    public CharacterSaveData characterSlot04;
    public CharacterSaveData characterSlot05;
    public CharacterSaveData characterSlot06;
    public CharacterSaveData characterSlot07;
    public CharacterSaveData characterSlot08;
    public CharacterSaveData characterSlot09;
    public CharacterSaveData characterSlot10;

    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    private void Start()
    {
        player = FindObjectOfType<PlayerManager>();
        DontDestroyOnLoad(gameObject);
        LoadAllCharacterProfiles();
    }

    //private void Update()
    //{

    //    if (saveGame)
    //    {
    //        saveGame = false;
    //        SaveGame();
    //    }

    //    if (loadGame)
    //    {
    //        loadGame = false;
    //        LoadGame();
    //    }
    //}



    public string DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlot characterSlot)
    {
        string fileName = "";
        switch(characterSlot)
        {
            case CharacterSlot.CharacterSlot_01:
                fileName = "characterSlot_01";
                break;
            case CharacterSlot.CharacterSlot_02:
                fileName = "characterSlot_02";
                break;
            case CharacterSlot.CharacterSlot_03:
                fileName = "characterSlot_03";
                break;
            case CharacterSlot.CharacterSlot_04:
                fileName = "characterSlot_04";
                break;
            case CharacterSlot.CharacterSlot_05:
                fileName = "characterSlot_05";
                break;
            case CharacterSlot.CharacterSlot_06:
                fileName = "characterSlot_06";
                break;
            case CharacterSlot.CharacterSlot_07:
                fileName = "characterSlot_07";
                break;
            case CharacterSlot.CharacterSlot_08:
                fileName = "characterSlot_08";
                break;
            case CharacterSlot.CharacterSlot_09:
                fileName = "characterSlot_09";
                break;
            case CharacterSlot.CharacterSlot_10:
                fileName = "characterSlot_10";
                break;
            default: 
                break;

        }
        return fileName;
    }

    //public void AttemptToCreateNewGame()
    //{
    //    saveFileDataWriter = new SaveFileDataWriter();

    //    // Check to see if we can Create a new save file (Check for other exisiting files first)
    //    saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot_01);

    //    if (!saveFileDataWriter.CheckToSeeIfFileExists())
    //    {
    //        // If this profile slot is not Taken, Make a new one using this slot
    //        currentCharacterSlotBeingUsed = CharacterSlot.CharacterSlot_01;
    //        currentCharacterData = new CharacterSaveData();
    //        StartCoroutine(LoadWorldScene());
    //        return;
    //    }

    //    // Check to see if we can Create a new save file (Check for other exisiting files first)
    //    saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot_02);

    //    if (!saveFileDataWriter.CheckToSeeIfFileExists())
    //    {
    //        // If this profile slot is not Taken, Make a new one using this slot
    //        currentCharacterSlotBeingUsed = CharacterSlot.CharacterSlot_02;
    //        currentCharacterData = new CharacterSaveData();
    //        StartCoroutine(LoadWorldScene());
    //        return;
    //    }

    //    // If there are no Free slots, Notify the Player
    //    TitleScreenManager.Instance.DisplayNoFreeCharacterSlotPopUp();

    //}

    //public void LoadGame()
    //{
    //    // Load a Previous file, with a file name depending on which slot we are using
    //    saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(currentCharacterSlotBeingUsed);

    //    saveFileDataWriter = new SaveFileDataWriter();
    //    // Generally works on multiple machine types (Application.persistentDataPath)
    //    saveFileDataWriter.saveDataDirectoryPath = Application.persistentDataPath;
    //    saveFileDataWriter.saveFileName = saveFileName;
    //    currentCharacterData = saveFileDataWriter.LoadSaveFile();

    //    StartCoroutine(LoadWorldScene());
    //}

    //public void SaveGame()
    //{
    //    // Save the cuurent file under a file Name depending on which slot we are using
    //    saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(currentCharacterSlotBeingUsed);

    //    saveFileDataWriter = new SaveFileDataWriter();
    //    // Generally works on multiple machine types (Application.persistentDataPath)
    //    saveFileDataWriter.saveDataDirectoryPath = Application.persistentDataPath;
    //    saveFileDataWriter.saveFileName = saveFileName;

    //    // Pass the Player info, from game, t their save file
    //    player.SaveGameDataToCurrentCharacterData(ref currentCharacterData);

    //    // Write that info onto a Json file, saved to this machine
    //    saveFileDataWriter.CreateNewCharacterSaveFile(currentCharacterData);
    //}

    private void LoadAllCharacterProfiles()
    {
        saveFileDataWriter = new SaveFileDataWriter();
        saveFileDataWriter.saveDataDirectoryPath = Application.persistentDataPath;

        saveFileDataWriter.saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot_01);
        characterSlot01 = saveFileDataWriter.LoadSaveFile();

        saveFileDataWriter.saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot_02);
        characterSlot02 = saveFileDataWriter.LoadSaveFile();

        saveFileDataWriter.saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot_03);
        characterSlot03 = saveFileDataWriter.LoadSaveFile();

        saveFileDataWriter.saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot_04);
        characterSlot04 = saveFileDataWriter.LoadSaveFile();

        saveFileDataWriter.saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot_05);
        characterSlot05 = saveFileDataWriter.LoadSaveFile();

        saveFileDataWriter.saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot_06);
        characterSlot06 = saveFileDataWriter.LoadSaveFile();

        saveFileDataWriter.saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot_07);
        characterSlot07 = saveFileDataWriter.LoadSaveFile();

        saveFileDataWriter.saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot_08);
        characterSlot08 = saveFileDataWriter.LoadSaveFile();

        saveFileDataWriter.saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot_09);
        characterSlot09 = saveFileDataWriter.LoadSaveFile();

        saveFileDataWriter.saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot_10);
        characterSlot10 = saveFileDataWriter.LoadSaveFile();
    }

    //public IEnumerator LoadWorldScene()
    //{
    //    AsyncOperation loadOperation = SceneManager.LoadSceneAsync(worldSceneIndex);

    //    player = FindObjectOfType<PlayerManager>();
    //    player.LoadGameDataFromCurrentCharacterData(ref currentCharacterData);

    //    yield return null;
    //}

    public int GetWorldSceneIndex()
    {
        return worldSceneIndex;
    }
}
