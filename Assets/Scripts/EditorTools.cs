using UnityEngine;

public class EditorTools : MonoBehaviour {

    private bool speedup = false;

    public void QuickPlay() {
        
    }

    public void Update() {
        if(Input.GetKeyDown(KeyCode.Z)) {
            if(speedup) {
                Time.timeScale = 1;
                speedup = false;
            } else {
                Time.timeScale = 3;
                speedup = true;
            }
        }

        if(Input.GetKeyDown(KeyCode.C)) {
            App.IslandManager.playerIsland.village.RecalculateBounds();
        }
    }

}
