using System.Collections;
using UnityEngine;

public class PauseMenu : Menu {

    [SerializeField] private Blinder blinder;

    public override IEnumerator Hide() {
        blinder.Hide();

        App.GameManager.UnpauseGame();

        isAnimating = true;
        float screenSize = transform.parent.GetComponent<RectTransform>().rect.size.y;
        var task = transform.LeanMoveLocalY(screenSize + GetComponent<RectTransform>().rect.size.y, 1f).setIgnoreTimeScale(true).setEaseInOutExpo().setOnComplete(() => {
            gameObject.SetActive(false);
            isAnimating = false;
        });
        yield return new WaitUntil(() => isAnimating);
    }

    public override IEnumerator Show() {
        blinder.Show();

        App.GameManager.PauseGame();
        
        RectTransform rectTransform = GetComponent<RectTransform>();
        float screenSize = transform.parent.GetComponent<RectTransform>().rect.size.y;
        rectTransform.localPosition = new Vector3(rectTransform.localPosition.x, screenSize + rectTransform.rect.size.y, rectTransform.localPosition.z);
        isAnimating = true;
        gameObject.SetActive(true);
        rectTransform.LeanMoveY(0, 1f).setIgnoreTimeScale(true).setEaseInOutExpo().setOnComplete(() => isAnimating = false);
        yield return new WaitUntil(() => isAnimating);
    }

}
