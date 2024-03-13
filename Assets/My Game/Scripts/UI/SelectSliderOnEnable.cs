using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectSliderOnEnable : MonoBehaviour
{
    public Slider statsSlider;

    private void OnEnable()
    {
        statsSlider.Select();
        //statsSlider.OnSelect(null);
    }
}
