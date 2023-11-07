using UnityEngine;
using UnityEngine.UI;

public class SliderValueSnap : MonoBehaviour {

    public float snapInterval = 5;
    private Slider sliderUI;

    public void Start() {
        sliderUI = GetComponent<Slider>();
        sliderUI.onValueChanged.AddListener(delegate { ShowSliderValue(); });
        ShowSliderValue();
    }

    public void ShowSliderValue() {
        float value = sliderUI.value;
        value = Mathf.Round(value / snapInterval) * snapInterval;
        sliderUI.value = value;
    }
}