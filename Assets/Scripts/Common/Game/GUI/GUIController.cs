using UnityEngine;

public class GUIController : MonoBehaviour {

    [SerializeField] private PauseMenu pauseMenu;
    public ResourcesGUI resourcesGui;

    void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            pauseMenu.Toggle();
        }
    }

}
