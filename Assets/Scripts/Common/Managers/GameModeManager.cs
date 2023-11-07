using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using KingdomsEdge.Common;

public class GameModeManager : MonoBehaviour {

    private IGameMode currentMode = null;
    private IGameMode mainMenuMode = new MainMenuMode();
    private IGameMode adventureMode = new AdventureMode();

    [SerializeField] private GameObject editorTools;

    private bool isSwitching = false;

    private void Awake() {
        switch(SceneManager.GetActiveScene().buildIndex) {
            case (int) SceneIndex.InitialScene:
                currentMode = mainMenuMode;
                StartCoroutine(currentMode.OnStart());
                StartCoroutine(App.Backdrop.GameStartFadeOut());
                break;
            case (int) SceneIndex.TestScene:
                currentMode = adventureMode;
                break;
            default:
                StartCoroutine(RouteToInitialScene());
                currentMode = mainMenuMode;
                StartCoroutine(currentMode.OnStart());
                StartCoroutine(App.Backdrop.FadeOut());
                editorTools.SetActive(true);
                break;
        }
    }

    private IEnumerator RouteToInitialScene() {
        int activeScene = SceneManager.GetActiveScene().buildIndex;
        yield return SceneManager.LoadSceneAsync((int) SceneIndex.InitialScene, LoadSceneMode.Single);
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex((int) SceneIndex.InitialScene));
        yield return SceneManager.UnloadSceneAsync(activeScene);
    }

    public IEnumerator SwitchMode(IGameMode mode) {
        yield return new WaitUntil(() => !isSwitching);
        if(currentMode == mode) yield break;

        isSwitching = true;
        if(currentMode != null) {
            yield return App.Backdrop.FadeIn();
            yield return currentMode.OnEnd();
        }
        
        currentMode = mode;
        yield return currentMode.OnStart();

        yield return App.Backdrop.FadeOut();
        isSwitching = false;
    }

    public void LoadAdventureMode() {
        StartCoroutine(SwitchMode(adventureMode));
    }

    public void LoadMainMenuMode() {
        StartCoroutine(SwitchMode(mainMenuMode));
    }

}
