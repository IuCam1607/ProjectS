using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class LevelUpUI : MonoBehaviour
{
    public PlayerStatsManager playerStatsManager;

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

    private void OnEnable()
    {
        currentPlayerLevel = playerStatsManager.playerLevel;
        currentPlayerLevelText.text = currentPlayerLevel.ToString();

        projectedPlayerLevel = playerStatsManager.playerLevel;
        projectedPlayerLevelText.text = projectedPlayerLevel.ToString();

        heathSlider.value = playerStatsManager.vitalityLevel;
        heathSlider.minValue = playerStatsManager.vitalityLevel;
        heathSlider.maxValue = 99;
        currentVitalityLevelText.text = playerStatsManager.vitalityLevel.ToString();
        projectedVitalityLevelText.text = playerStatsManager.vitalityLevel.ToString();


        focusSlider.value = playerStatsManager.focusLevel;
        focusSlider.minValue = playerStatsManager.focusLevel;
        focusSlider.maxValue = 99;
        currentAttunementLevelText.text = playerStatsManager.focusLevel.ToString();
        projectedAttunementLevelText.text = playerStatsManager.focusLevel.ToString();

        enduranceSlider.value = playerStatsManager.enduranceLevel;
        enduranceSlider.minValue = playerStatsManager.enduranceLevel;
        enduranceSlider.maxValue = 99;
        currentEnduranceLevelText.text = playerStatsManager.enduranceLevel.ToString();
        projectedEnduranceLevelText.text = playerStatsManager.enduranceLevel.ToString();

        strengthSlider.value = playerStatsManager.strengthLevel;
        strengthSlider.minValue = playerStatsManager.strengthLevel;
        strengthSlider.maxValue = 99;
        currentStrengthLevelText.text = playerStatsManager.strengthLevel.ToString();
        projectedStrengthLevelText.text = playerStatsManager.strengthLevel.ToString();

        dexteritySlider.value = playerStatsManager.dexterityLevel;
        dexteritySlider.minValue = playerStatsManager.dexterityLevel;
        dexteritySlider.maxValue = 99;
        currentDexterityLevelText.text = playerStatsManager.dexterityLevel.ToString();
        projectedDexterityLevelText.text = playerStatsManager.dexterityLevel.ToString();

        intelligenceSlider.value = playerStatsManager.intelligenceLevel;
        intelligenceSlider.minValue = playerStatsManager.intelligenceLevel;
        intelligenceSlider.maxValue = 99;
        currentIntelligenceLevelText.text = playerStatsManager.intelligenceLevel.ToString();
        projectedIntelligenceLevelText.text = playerStatsManager.intelligenceLevel.ToString();

        faithSlider.value = playerStatsManager.faithLevel;
        faithSlider.minValue = playerStatsManager.faithLevel;
        faithSlider.maxValue = 99;
        currentFaithLevelText.text = playerStatsManager.faithLevel.ToString();
        projectedFaithLevelText.text = playerStatsManager.faithLevel.ToString();

        poiseSlider.value = playerStatsManager.poiseLevel;
        poiseSlider.minValue = playerStatsManager.poiseLevel;
        poiseSlider.maxValue = 99;
        currentPoiseLevelText.text = playerStatsManager.poiseLevel.ToString();
        projectedPoiseLevelText.text = playerStatsManager.poiseLevel.ToString();

        UpdateProjectedPlayerLevel();
    }

    public void ConfirmPlayerLevelUpStats()
    {
        playerStatsManager.playerLevel = projectedPlayerLevel;
        playerStatsManager.vitalityLevel = Mathf.RoundToInt(heathSlider.value);
        playerStatsManager.focusLevel = Mathf.RoundToInt(focusSlider.value);
        playerStatsManager.enduranceLevel = Mathf.RoundToInt(enduranceSlider.value);
        playerStatsManager.strengthLevel = Mathf.RoundToInt(strengthSlider.value);
        playerStatsManager.dexterityLevel = Mathf.RoundToInt(dexteritySlider.value);
        playerStatsManager.intelligenceLevel = Mathf.RoundToInt(intelligenceSlider.value);
        playerStatsManager.faithLevel = Mathf.RoundToInt(faithSlider.value);
        playerStatsManager.poiseLevel = Mathf.RoundToInt(poiseSlider.value);

        playerStatsManager.maxHealth = playerStatsManager.SetMaxHealthFromHealthLevel();
        playerStatsManager.maxFocusPoint = playerStatsManager.SetMaxFocusFromFocusLevel();
        playerStatsManager.maxStamina = playerStatsManager.SetMaxStaminaFromEnduranceLevel();
    }

    private void CalculateBloodCostToLevelUp()
    {
        for (int i = 0; i < projectedPlayerLevel; i++)
        {
            bloodCelssRequiredToLevelUP = Mathf.RoundToInt((projectedPlayerLevel * baseLevelUpCost) * 1.5f);
        }

        currentBloodCellsText.text = playerStatsManager.currentBlood.ToString();
        bloodCelssRequiredToLevelUPText.text = bloodCelssRequiredToLevelUP.ToString();
    }

    #region Button
    public void OnClickHealthButton()
    {
        if (heathSlider != null)
        {
            heathSlider.value += 1f;
        }
    }

    public void OnClickAttunementButton()
    {
        if (focusSlider != null)
        {
            focusSlider.value += 1f;
        }
    }

    public void OnClickEnduranceButton()
    {
        if (enduranceSlider != null)
        {
            enduranceSlider.value += 1f;
        }
    }

    public void OnClickStrengthButton()
    {
           if (strengthSlider != null)
        {
            strengthSlider.value += 1f;
        }
    }

    public void OnClickDexterityButton()
    {
        if (dexteritySlider != null)
        {
            dexteritySlider.value += 1f;
        }
    }

    public void OnClickIntelligenceButton()
    {
        if (intelligenceSlider != null)
        {
            intelligenceSlider.value += 1f;
        }
    }

    public void OnClickFaithButton()
    {
        if (faithSlider != null)
        {
            faithSlider.value += 1f;
        }
    }

    public void OnClickPoiseButton()
    {
        if (poiseSlider != null)
        {
            poiseSlider.value += 1f;
        }
    }

    #endregion

    #region Slider

    private void UpdateProjectedPlayerLevel()
    {
        bloodCelssRequiredToLevelUP = 0;

        projectedPlayerLevel = currentPlayerLevel;
        projectedPlayerLevel += Mathf.RoundToInt(heathSlider.value) - playerStatsManager.vitalityLevel;
        projectedPlayerLevel += Mathf.RoundToInt(focusSlider.value) - playerStatsManager.focusLevel;
        projectedPlayerLevel += Mathf.RoundToInt(enduranceSlider.value) - playerStatsManager.enduranceLevel;
        projectedPlayerLevel += Mathf.RoundToInt(strengthSlider.value) - playerStatsManager.strengthLevel;
        projectedPlayerLevel += Mathf.RoundToInt(dexteritySlider.value) - playerStatsManager.dexterityLevel;
        projectedPlayerLevel += Mathf.RoundToInt(intelligenceSlider.value) - playerStatsManager.intelligenceLevel;
        projectedPlayerLevel += Mathf.RoundToInt(faithSlider.value) - playerStatsManager.faithLevel;
        projectedPlayerLevel += Mathf.RoundToInt(poiseSlider.value) - playerStatsManager.poiseLevel;

        projectedPlayerLevelText.text = projectedPlayerLevel.ToString();

        CalculateBloodCostToLevelUp();

        if (playerStatsManager.currentBlood < bloodCelssRequiredToLevelUP)
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
