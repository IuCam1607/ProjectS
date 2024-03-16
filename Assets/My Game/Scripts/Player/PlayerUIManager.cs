using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class PlayerUIManager : MonoBehaviour
{
    public static PlayerUIManager instance;

    public PlayerManager player;
    public EquipmentWindowUI equipmentWindowUI;

    public QuickSlotsUI quickSlotsUI;

    public TextMeshProUGUI bloodCount;

    public bool isUIActive = false;

    [Header("UI Windows")]
    public GameObject hudWindow;
    public GameObject selectWindow;
    public GameObject equipmentScreenWindow;
    public GameObject weaponInventoryWindow;
    public GameObject levelUpWindow;
    public GameObject doorWindow;

    [Header("Equipment Window Slot Selected")]
    public bool rightHandSlot01Selected;
    public bool rightHandSlot02Selected;
    public bool leftHandSlot01Selected;
    public bool leftHandSlot02Selected;

    [Header("Weapon Inventory")]
    public GameObject weaponInventorySlotPrefab;
    public Transform weaponInventorySlotsParent;
    private WeaponInventorySlot[] weaponInventorySlots;

    [HideInInspector] public PlayerUIHudManager playerUIHudManager;
    [HideInInspector] public PlayerUIPopUPManager playerUIPopUPManager;

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

        player = FindAnyObjectByType<PlayerManager>();
        weaponInventorySlots = weaponInventorySlotsParent.GetComponentsInChildren<WeaponInventorySlot>();
        playerUIHudManager = GetComponentInChildren<PlayerUIHudManager>();
        playerUIPopUPManager = GetComponentInChildren<PlayerUIPopUPManager>();
        quickSlotsUI = GetComponentInChildren<QuickSlotsUI>();
    }

    private void Start()
    {

    }

    private void Update()
    {
        if (!isUIActive)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (player == null)
        {
            player = FindAnyObjectByType<PlayerManager>();
        }
    }

    public void Setup()
    {
        quickSlotsUI.UpdateCurrentSpellIcon(player.playerInventoryManager.currentSpell);
        quickSlotsUI.UpdateCurrentConsumableIcon(player.playerInventoryManager.currentConsumable);
        equipmentWindowUI.LoadWeaponOnEquipmentScreen(player.playerInventoryManager);
        bloodCount.text = player.playerStatsManager.currentBlood.ToString();
    }

    public void UpdateUI()
    {
        #region Weapon Inventory Slots
        for (int i = 0; i < weaponInventorySlots.Length; i++)
        {
            if (i < player.playerInventoryManager.weaponInventory.Count)
            {
                if (weaponInventorySlots.Length < player.playerInventoryManager.weaponInventory.Count)
                {
                    Instantiate(weaponInventorySlotPrefab, weaponInventorySlotsParent);
                    weaponInventorySlots = weaponInventorySlotsParent.GetComponentsInChildren<WeaponInventorySlot>();
                }
                weaponInventorySlots[i].AddItem(player.playerInventoryManager.weaponInventory[i]);
            }
            else
            {
                weaponInventorySlots[i].ClearInventorySlot();
            }
        }
        #endregion
    }

    public void OpenSelectWindow()
    {
        isUIActive = true;
        selectWindow.SetActive(true);
    }

    public void CloseSelectWindow()
    {
        isUIActive = false;
        selectWindow.SetActive(false);
    }

    public void CloseAllInventoryWindows()
    {
        ResetAllSelectedSlots();
        weaponInventoryWindow.SetActive(false);
        equipmentScreenWindow.SetActive(false);
    }

    public void ResetAllSelectedSlots()
    {
        rightHandSlot01Selected = false;
        rightHandSlot02Selected = false;
        leftHandSlot01Selected = false;
        leftHandSlot02Selected = false;
    }
}
