using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BarHandler : MonoBehaviour
{
    [SerializeField] BarSettings barSettings;

    [SerializeField] private Slider slider;
    [SerializeField] private TMP_Text titleText;

    private void Awake()
    {
        barSettings.SetUpSlider(slider);
        titleText.text = $"{slider.value}/{slider.maxValue}";
    }

    public void modifyValue(int value)
    {
        float newValue = slider.value + value;
        slider.value = Mathf.Clamp(newValue,slider.minValue,slider.maxValue);
        titleText.text = $"{slider.value}/{slider.maxValue}";
    }

    public void SetUpSlider(BarArgs barArgs)
    {
        slider.minValue = barArgs.minValue;
        slider.maxValue = barArgs.maxValue;
        slider.value = barArgs.startValue;
        titleText.text = $"{slider.value}/{slider.maxValue}";
    }
}

[Serializable]
public struct BarArgs
{
    public float minValue;
    public float maxValue;
    public float startValue;
}
