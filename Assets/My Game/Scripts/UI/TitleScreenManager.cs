using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleScreenManager : MonoBehaviour
{
    public static TitleScreenManager Instance;

    [Header("Menu")]
    [SerializeField] GameObject titleScreenMainMenu;
    [SerializeField] GameObject titleScreenLoadMenu;

    [Header("Buttons")]
    [SerializeField] Button loadMenuReturnButton;
    [SerializeField] Button mainMenuNewGameButton;
    [SerializeField] Button mainMenuReturnButton;

    [Header("Pop Ups")]
    [SerializeField] GameObject noCharacterSlotsPopUp;
    [SerializeField] Button noCharacterSlotsOkayButton;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }

    //public void StartNewGame()
    //{
    //    WorldSaveGameManager.instance.AttemptToCreateNewGame();
    //}

    public void OpenLoadGameMenu()
    {
        // Close Main Menu
        titleScreenMainMenu.SetActive(false);

        // Open Load Menu
        titleScreenLoadMenu.SetActive(true);

        // Select the return Button first
        loadMenuReturnButton.Select();
    }

    public void CloseLoadGameMenu()
    {
        // Close Load Menu
        titleScreenLoadMenu.SetActive(false);

        // Open Main Menu
        titleScreenMainMenu.SetActive(true);


        // Select the Load Button
        mainMenuReturnButton.Select();
    }

    public void DisplayNoFreeCharacterSlotPopUp()
    {
        noCharacterSlotsPopUp.SetActive(true);
        noCharacterSlotsOkayButton.Select();
    }

    public void CloseNoFreeCharacterSlotsPopUp()
    {
        noCharacterSlotsPopUp.SetActive(false);
        mainMenuNewGameButton.Select();
    }
}
