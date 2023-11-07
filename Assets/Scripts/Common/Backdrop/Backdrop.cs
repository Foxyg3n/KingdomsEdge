using UnityEngine;
using System.Collections;

public class Backdrop : MonoBehaviour {

    [SerializeField] private Animator animator;

    public bool FinishedAnimating { get; set; } = false;

    public IEnumerator FadeIn() {
        FinishedAnimating = false;
        animator.Play("FadeIn");
        yield return new WaitUntil(() => FinishedAnimating);
        FinishedAnimating = true;
    }

    public IEnumerator FadeOut() {
        FinishedAnimating = false;
        animator.Play("FadeOut");
        yield return new WaitUntil(() => FinishedAnimating);
        FinishedAnimating = true;
    }

    public IEnumerator GameStartFadeOut() {
        FinishedAnimating = false;
        animator.Play("GameStartFadeOut");
        yield return new WaitUntil(() => FinishedAnimating);
        FinishedAnimating = true;
    }

}
