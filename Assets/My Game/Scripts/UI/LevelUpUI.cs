using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class LevelUpUI : MonoBehaviour
{
    public PlayerManager playerManager;

    public Button confirmLevelUpButton;

    [Header("Player Level")]
    public int currentPlayerLevel;
    public int projectedPlayerLevel;
    public TextMeshProUGUI currentPlayerLevelText;
    public TextMeshProUGUI projectedPlayerLevelText;

    [Header("Blood Cells")]
    public TextMeshProUGUI currentBloodCellsText;
    public TextMeshProUGUI bloodCelssRequiredToLevelUPText;
    private int bloodCelssRequiredToLevelUP;
    public int baseLevelUpCost = 5;

    [Header("Vitality")]
    public Slider heathSlider;
    public TextMeshProUGUI currentVitalityLevelText;
    public TextMeshProUGUI projectedVitalityLevelText;

    [Header("Attunement")]
    public Slider focusSlider;
    public TextMeshProUGUI currentAttunementLevelText;
    public TextMeshProUGUI projectedAttunementLevelText;

    [Header("Endurance")]
    public Slider enduranceSlider;
    public TextMeshProUGUI currentEnduranceLevelText;
    public TextMeshProUGUI projectedEnduranceLevelText;

    [Header("Strength")]
    public Slider strengthSlider;
    public TextMeshProUGUI currentStrengthLevelText;
    public TextMeshProUGUI projectedStrengthLevelText;

    [Header("Dexterity")]
    public Slider dexteritySlider;
    public TextMeshProUGUI currentDexterityLevelText;
    public TextMeshProUGUI projectedDexterityLevelText;

    [Header("Intelligence")]
    public Slider intelligenceSlider;
    public TextMeshProUGUI currentIntelligenceLevelText;
    public TextMeshProUGUI projectedIntelligenceLevelText;

    [Header("Faith")]
    public Slider faithSlider;
    public TextMeshProUGUI currentFaithLevelText;
    public TextMeshProUGUI projectedFaithLevelText;

    [Header("Poise")]
    public Slider poiseSlider;
    public TextMeshProUGUI currentPoiseLevelText;
    public TextMeshProUGUI projectedPoiseLevelText;

    private void Awake()
    {
        playerManager = FindAnyObjectByType<PlayerManager>();
    }

    private void OnEnable()
    {
        PlayerUIManager.instance.isUIActive = true;

        Debug.Log(PlayerUIManager.instance.isUIActive);

        currentPlayerLevel = playerManager.playerStatsManager.playerLevel;
        currentPlayerLevelText.text = currentPlayerLevel.ToString();

        projectedPlayerLevel = playerManager.playerStatsManager.playerLevel;
        projectedPlayerLevelText.text = projectedPlayerLevel.ToString();

        heathSlider.value = playerManager.playerStatsManager.vitalityLevel;
        heathSlider.minValue = playerManager.playerStatsManager.vitalityLevel;
        heathSlider.maxValue = 99;
        currentVitalityLevelText.text = playerManager.playerStatsManager.vitalityLevel.ToString();
        projectedVitalityLevelText.text = playerManager.playerStatsManager.vitalityLevel.ToString();


        focusSlider.value = playerManager.playerStatsManager.focusLevel;
        focusSlider.minValue = playerManager.playerStatsManager.focusLevel;
        focusSlider.maxValue = 99;
        currentAttunementLevelText.text = playerManager.playerStatsManager.focusLevel.ToString();
        projectedAttunementLevelText.text = playerManager.playerStatsManager.focusLevel.ToString();

        enduranceSlider.value = playerManager.playerStatsManager.enduranceLevel;
        enduranceSlider.minValue = playerManager.playerStatsManager.enduranceLevel;
        enduranceSlider.maxValue = 99;
        currentEnduranceLevelText.text = playerManager.playerStatsManager.enduranceLevel.ToString();
        projectedEnduranceLevelText.text = playerManager.playerStatsManager.enduranceLevel.ToString();

        strengthSlider.value = playerManager.playerStatsManager.strengthLevel;
        strengthSlider.minValue = playerManager.playerStatsManager.strengthLevel;
        strengthSlider.maxValue = 99;
        currentStrengthLevelText.text = playerManager.playerStatsManager.strengthLevel.ToString();
        projectedStrengthLevelText.text = playerManager.playerStatsManager.strengthLevel.ToString();

        dexteritySlider.value = playerManager.playerStatsManager.dexterityLevel;
        dexteritySlider.minValue = playerManager.playerStatsManager.dexterityLevel;
        dexteritySlider.maxValue = 99;
        currentDexterityLevelText.text = playerManager.playerStatsManager.dexterityLevel.ToString();
        projectedDexterityLevelText.text = playerManager.playerStatsManager.dexterityLevel.ToString();

        intelligenceSlider.value = playerManager.playerStatsManager.intelligenceLevel;
        intelligenceSlider.minValue = playerManager.playerStatsManager.intelligenceLevel;
        intelligenceSlider.maxValue = 99;
        currentIntelligenceLevelText.text = playerManager.playerStatsManager.intelligenceLevel.ToString();
        projectedIntelligenceLevelText.text = playerManager.playerStatsManager.intelligenceLevel.ToString();

        faithSlider.value = playerManager.playerStatsManager.faithLevel;
        faithSlider.minValue = playerManager.playerStatsManager.faithLevel;
        faithSlider.maxValue = 99;
        currentFaithLevelText.text = playerManager.playerStatsManager.faithLevel.ToString();
        projectedFaithLevelText.text = playerManager.playerStatsManager.faithLevel.ToString();

        poiseSlider.value = playerManager.playerStatsManager.poiseLevel;
        poiseSlider.minValue = playerManager.playerStatsManager.poiseLevel;
        poiseSlider.maxValue = 99;
        currentPoiseLevelText.text = playerManager.playerStatsManager.poiseLevel.ToString();
        projectedPoiseLevelText.text = playerManager.playerStatsManager.poiseLevel.ToString();

        currentBloodCellsText.text = playerManager.playerStatsManager.currentBlood.ToString();

        UpdateProjectedPlayerLevel();
    }

    public void ConfirmPlayerLevelUpStats()
    {
        playerManager.playerStatsManager.playerLevel = projectedPlayerLevel;
        playerManager.playerStatsManager.vitalityLevel = Mathf.RoundToInt(heathSlider.value);
        playerManager.playerStatsManager.focusLevel = Mathf.RoundToInt(focusSlider.value);
        playerManager.playerStatsManager.enduranceLevel = Mathf.RoundToInt(enduranceSlider.value);
        playerManager.playerStatsManager.strengthLevel = Mathf.RoundToInt(strengthSlider.value);
        playerManager.playerStatsManager.dexterityLevel = Mathf.RoundToInt(dexteritySlider.value);
        playerManager.playerStatsManager.intelligenceLevel = Mathf.RoundToInt(intelligenceSlider.value);
        playerManager.playerStatsManager.faithLevel = Mathf.RoundToInt(faithSlider.value);
        playerManager.playerStatsManager.poiseLevel = Mathf.RoundToInt(poiseSlider.value);

        playerManager.playerStatsManager.maxHealth = playerManager.playerStatsManager.SetMaxHealthFromHealthLevel();
        playerManager.playerStatsManager.maxFocusPoint = playerManager.playerStatsManager.SetMaxFocusFromFocusLevel();
        playerManager.playerStatsManager.maxStamina = playerManager.playerStatsManager.SetMaxStaminaFromEnduranceLevel();

        playerManager.playerStatsManager.currentBlood = playerManager.playerStatsManager.currentBlood - bloodCelssRequiredToLevelUP;
        PlayerUIManager.instance.bloodCount.text = playerManager.playerStatsManager.currentBlood.ToString();

        playerManager.playerStatsManager.healthBar.SetMaxHealth(playerManager.playerStatsManager.SetMaxHealthFromHealthLevel());
        playerManager.playerStatsManager.healthBar.SetCurrentHealth(playerManager.playerStatsManager.SetMaxHealthFromHealthLevel());

        playerManager.playerStatsManager.focusPointBar.SetMaxFocusPoint(playerManager.playerStatsManager.SetMaxFocusFromFocusLevel());
        playerManager.playerStatsManager.focusPointBar.SetCurrentFocusPoint(playerManager.playerStatsManager.SetMaxFocusFromFocusLevel());

        playerManager.playerStatsManager.staminaBar.SetMaxStamina(playerManager.playerStatsManager.SetMaxStaminaFromEnduranceLevel());
        playerManager.playerStatsManager.staminaBar.SetCurrentStamina(playerManager.playerStatsManager.SetMaxStaminaFromEnduranceLevel());

        playerManager.playerStatsManager.RefreshHUD();

        PlayerUIManager.instance.isUIActive = false;
        gameObject.SetActive(false);
    }

    private void CalculateBloodCostToLevelUp()
    {
        for (int i = 0; i < projectedPlayerLevel; i++)
        {
            bloodCelssRequiredToLevelUP = Mathf.RoundToInt((projectedPlayerLevel * baseLevelUpCost) * 1.5f);
        }
    }

    #region Button
    public void OnClickHealthRightArrowButton()
    {
        if (heathSlider != null)
        {
            heathSlider.value += 1f;
        }
    }

    public void OnClickAttunementRightArrowButton()
    {
        if (focusSlider != null)
        {
            focusSlider.value += 1f;
        }
    }

    public void OnClickEnduranceRightArrowButton()
    {
        if (enduranceSlider != null)
        {
            enduranceSlider.value += 1f;
        }
    }

    public void OnClickStrengthRightArrowButton()
    {
           if (strengthSlider != null)
        {
            strengthSlider.value += 1f;
        }
    }

    public void OnClickDexterityRightArrowButton()
    {
        if (dexteritySlider != null)
        {
            dexteritySlider.value += 1f;
        }
    }

    public void OnClickIntelligenceRightArrowButton()
    {
        if (intelligenceSlider != null)
        {
            intelligenceSlider.value += 1f;
        }
    }

    public void OnClickFaithRightArrowButton()
    {
        if (faithSlider != null)
        {
            faithSlider.value += 1f;
        }
    }

    public void OnClickPoiseRightArrowButton()
    {
        if (poiseSlider != null)
        {
            poiseSlider.value += 1f;
        }
    }

    public void OnClickHealthLeftArrowButton()
    {
        if (heathSlider != null)
        {
            heathSlider.value -= 1f;
        }
    }

    public void OnClickAttunementLeftArrowButton()
    {
        if (focusSlider != null)
        {
            focusSlider.value -= 1f;
        }
    }

    public void OnClickEnduranceLeftArrowButton()
    {
        if (enduranceSlider != null)
        {
            enduranceSlider.value -= 1f;
        }
    }

    public void OnClickStrengthLeftArrowButton()
    {
        if (strengthSlider != null)
        {
            strengthSlider.value -= 1f;
        }
    }

    public void OnClickDexterityLeftArrowButton()
    {
        if (dexteritySlider != null)
        {
            dexteritySlider.value -= 1f;
        }
    }

    public void OnClickIntelligenceLeftArrowButton()
    {
        if (intelligenceSlider != null)
        {
            intelligenceSlider.value -= 1f;
        }
    }

    public void OnClickFaithLeftArrowButton()
    {
        if (faithSlider != null)
        {
            faithSlider.value -= 1f;
        }
    }

    public void OnClickPoiseLeftArrowButton()
    {
        if (poiseSlider != null)
        {
            poiseSlider.value -= 1f;
        }
    }

    public void OnClickExitButton()
    {
        PlayerUIManager.instance.isUIActive = false;
        gameObject.SetActive(false);
    }

    #endregion

    #region Slider

    private void UpdateProjectedPlayerLevel()
    {
        bloodCelssRequiredToLevelUP = 0;

        projectedPlayerLevel = currentPlayerLevel;
        projectedPlayerLevel += Mathf.RoundToInt(heathSlider.value) - playerManager.playerStatsManager.vitalityLevel;
        projectedPlayerLevel += Mathf.RoundToInt(focusSlider.value) - playerManager.playerStatsManager.focusLevel;
        projectedPlayerLevel += Mathf.RoundToInt(enduranceSlider.value) - playerManager.playerStatsManager.enduranceLevel;
        projectedPlayerLevel += Mathf.RoundToInt(strengthSlider.value) - playerManager.playerStatsManager.strengthLevel;
        projectedPlayerLevel += Mathf.RoundToInt(dexteritySlider.value) - playerManager.playerStatsManager.dexterityLevel;
        projectedPlayerLevel += Mathf.RoundToInt(intelligenceSlider.value) - playerManager.playerStatsManager.intelligenceLevel;
        projectedPlayerLevel += Mathf.RoundToInt(faithSlider.value) - playerManager.playerStatsManager.faithLevel;
        projectedPlayerLevel += Mathf.RoundToInt(poiseSlider.value) - playerManager.playerStatsManager.poiseLevel;

        projectedPlayerLevelText.text = projectedPlayerLevel.ToString();

        CalculateBloodCostToLevelUp();
        bloodCelssRequiredToLevelUPText.text = bloodCelssRequiredToLevelUP.ToString();

        if (playerManager.playerStatsManager.currentBlood < bloodCelssRequiredToLevelUP)
        {
            confirmLevelUpButton.interactable = false;
        }
        else
        {
            confirmLevelUpButton.interactable = true;
        }
    }

    public void UpdateHealthLevelSlider()
    {
        projectedVitalityLevelText.text = heathSlider.value.ToString();
        UpdateProjectedPlayerLevel();
    }

    public void UpdateAttunementLevelSlider()
    {
        projectedAttunementLevelText.text = focusSlider.value.ToString();
        UpdateProjectedPlayerLevel();
    }

    public void UpdateEnduranceLevelSlider()
    {
        projectedEnduranceLevelText.text = enduranceSlider.value.ToString();
        UpdateProjectedPlayerLevel();
    }

    public void UpdateStrengthLevelSlider()
    {
        projectedStrengthLevelText.text = strengthSlider.value.ToString();
        UpdateProjectedPlayerLevel();
    }

    public void UpdateDexterityLevelSlider()
    {
        projectedDexterityLevelText.text = dexteritySlider.value.ToString();
        UpdateProjectedPlayerLevel();
    }

    public void UpdateIntelligenceLevelSlider()
    {
        projectedIntelligenceLevelText.text = intelligenceSlider.value.ToString();
        UpdateProjectedPlayerLevel();
    }

    public void UpdateFaithLevelSlider()
    {
        projectedFaithLevelText.text = faithSlider.value.ToString();
        UpdateProjectedPlayerLevel();
    }

    public void UpdatePoiseLevelSlider()
    {
        projectedPoiseLevelText.text = poiseSlider.value.ToString();
        UpdateProjectedPlayerLevel();
    }

    #endregion
}
