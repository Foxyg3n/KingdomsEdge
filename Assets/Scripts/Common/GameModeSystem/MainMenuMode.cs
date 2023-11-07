using KingdomsEdge.Common;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenuMode : IGameMode {

    private GameModeState state = GameModeState.Ended;
    private int mainMenuScene = (int) SceneIndex.MainMenu;
    
    public IEnumerator OnStart() {
        if(state != GameModeState.Ended) yield break;
        state = GameModeState.Starting;
        
        yield return SceneManager.LoadSceneAsync(mainMenuScene, LoadSceneMode.Additive);
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(mainMenuScene));
        
        state = GameModeState.Started;
    }

    public IEnumerator OnEnd() {
        if(state != GameModeState.Started) yield break;
        state = GameModeState.Ending;
        
        yield return SceneManager.UnloadSceneAsync(mainMenuScene);
        
        state = GameModeState.Ended;
    }

}