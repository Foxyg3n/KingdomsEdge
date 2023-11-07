using KingdomsEdge.Common;
using Player;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdventureMode : IGameMode {

    private GameModeState state = GameModeState.Ended;
    private Scene activeScene;

    public IEnumerator OnStart() {
        if(state != GameModeState.Ended) yield break;
        state = GameModeState.Starting;

        App.GameManager.StartGame();
        activeScene = App.IslandManager.playerIsland.activeScene;
        GameObject display = PrefabUtils.InstantiatePrefab("Display", activeScene);
        display.transform.position = new Vector3(0, 0.42f, -3.1f);
        GameObject player = PrefabUtils.InstantiatePrefab("Player", activeScene);
        player.GetComponent<PlayerController>().display = display.GetComponent<Display>();
        SceneManager.SetActiveScene(activeScene);

        // Fetch save data and load the scene
        // var saveData = SaveData.Load();
        // _activeScene = saveData.SceneIndex;
        // yield return SceneManager.LoadSceneAsync(_activeScene.buildIndex, LoadSceneMode.Additive);
        // SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(_activeScene));

        state = GameModeState.Started;
    }

    public IEnumerator OnEnd() {
        if(state != GameModeState.Started) yield break;
        state = GameModeState.Ending;
        
        yield return SceneManager.UnloadSceneAsync(activeScene);
        
        state = GameModeState.Ended;
    }

}