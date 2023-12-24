using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillStatusBar : MonoBehaviour
{
    public Image fillImage;
    private Slider slider;

    protected virtual void Awake()
    {
        slider = GetComponent<Slider>();
    }

    protected void UpdateFill(float currentValue, float maxValue)
    {
        if (slider.value <= slider.minValue)
        {
            fillImage.enabled = false;
        }
        if (slider.value > slider.minValue && !fillImage.enabled)
        {
            fillImage.enabled = true;
        }
        float fillValue = currentValue / maxValue;
        slider.value = fillValue;
    }
}
