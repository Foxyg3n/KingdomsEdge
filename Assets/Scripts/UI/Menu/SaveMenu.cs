using System.Collections;
using UnityEngine;

public class SaveMenu : Menu {

    public override IEnumerator Show() {
        isAnimating = false;
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        gameObject.SetActive(true);
        canvasGroup.LeanAlpha(1, 1f).setEaseInOutExpo().setOnComplete(() => isAnimating = true);
        yield return new WaitUntil(() => isAnimating);
    }

    public override IEnumerator Hide() {
        isAnimating = false;
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.LeanAlpha(0, 1f).setEaseOutExpo().setOnComplete(() => {
            canvasGroup.alpha = 1;
            gameObject.SetActive(false);
            isAnimating = true;
        });
        yield return new WaitUntil(() => isAnimating);
    }


}
