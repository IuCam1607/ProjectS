using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FocusPointBar : MonoBehaviour
{
    private Slider slider;
    private RectTransform rectTransform;

    [Header("Bar Options")]
    [SerializeField] protected bool scaleBarLengthWithStats = true;
    [SerializeField] protected float widthScaleMultiplier = 1f;

    private void Awake()
    {
        slider = GetComponent<Slider>();
        rectTransform = GetComponent<RectTransform>();
    }

    public void SetMaxFocusPoint(float maxFocusPoint)
    {
        slider.maxValue = maxFocusPoint;
        slider.value = maxFocusPoint;

        if (scaleBarLengthWithStats)
        {
            rectTransform.sizeDelta = new Vector2(maxFocusPoint * widthScaleMultiplier, rectTransform.sizeDelta.y);
        }
    }

    public void SetCurrentStamina(float maxFocusPoint)
    {
        slider.value = maxFocusPoint;
    }
}
