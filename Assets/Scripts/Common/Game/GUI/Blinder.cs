using UnityEngine;
using UnityEngine.UI;

public class Blinder : MonoBehaviour {

    private Image image;
    private bool toggled = false;

    public void Start() {
        image = GetComponent<Image>();
    }

    public void Show() {
        toggled = true;
        LeanTween.color(image.rectTransform, new Color(0, 0, 0, 0.4f), 0.5f).setIgnoreTimeScale(true);
    }

    public void Hide() {
        toggled = false;
        LeanTween.color(image.rectTransform, new Color(0, 0, 0, 0), 0.5f).setIgnoreTimeScale(true);
    }

    public void Toggle() {
        if(toggled) Hide();
        else Show();
    }

}
