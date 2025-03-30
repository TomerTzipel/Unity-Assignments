using UnityEngine.UI;
using UnityEngine;


namespace HW2
{
    [CreateAssetMenu(fileName = "BarSettings", menuName = "Scriptable Objects/BarSettings")]
    public class BarSettings : ScriptableObject
    {
        [SerializeField] BarArgs values;
        [Header("Settings")]
        [SerializeField] private bool isSliderWholeNumbers;
        [SerializeField] private Slider.Direction sliderDirection;

        public void SetUpSlider(Slider slider)
        {
            slider.minValue = values.minValue;
            slider.maxValue = values.maxValue;
            slider.value = values.startValue;
            slider.direction = sliderDirection;
            slider.wholeNumbers = isSliderWholeNumbers;
        }
    }
}

