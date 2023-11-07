using System.Collections;
using UnityEngine;

public class OptionsMenu : Menu {

    public override IEnumerator Show() {
        RectTransform rectTransform = GetComponent<RectTransform>();
        float screenSize = transform.parent.GetComponent<RectTransform>().rect.size.y;
        rectTransform.localPosition = new Vector3(rectTransform.localPosition.x, screenSize + rectTransform.rect.size.y, rectTransform.localPosition.z);
        isAnimating = false;
        gameObject.SetActive(true);
        rectTransform.LeanMoveY(0, 1f).setEaseInOutExpo().setOnComplete(() => isAnimating = true);
        yield return new WaitUntil(() => isAnimating);
    }

    public override IEnumerator Hide() {
        isAnimating = false;
        float screenSize = transform.parent.GetComponent<RectTransform>().rect.size.y;
        transform.LeanMoveLocalY(screenSize + GetComponent<RectTransform>().rect.size.y, 1f).setEaseInOutExpo().setOnComplete(() => {
            gameObject.SetActive(false);
            isAnimating = true;
        });
        yield return new WaitUntil(() => isAnimating);
    }

}
