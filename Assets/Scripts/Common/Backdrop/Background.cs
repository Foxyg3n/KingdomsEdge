using UnityEngine;

public class Background : MonoBehaviour {

    public void PlayMusic() {
        App.AudioManager.PlayBackgroundMusic(0);
    }

    public void OnFadeFinish() {
        App.Backdrop.FinishedAnimating = true;
    }

}
