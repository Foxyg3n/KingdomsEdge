using UnityEngine;

public class GameManager : MonoBehaviour {

    [SerializeField] private VoidEventChannelSO startGameEvent;

    private GameState state = GameState.STOPPED;
    public bool isGamePaused => state == GameState.STOPPED;

    private void Awake() {
        startGameEvent.OnEventRaised += StartGame;
    }

    public void StartGame() {
        if(state != GameState.STOPPED) return;
        App.GameModeManager.LoadAdventureMode();
        // Test GameOptions
        GameOptions gameOptions = new(new IslandOptions[] {
            new(100, true),
            new(100, false)
        });
        App.IslandManager.GenerateIslands(gameOptions);
        state = GameState.RUNNING;
    }

    public void EndGame() {
        if(state != GameState.RUNNING) return;
        App.GameModeManager.LoadMainMenuMode();
        state = GameState.STOPPED;
    }

    public void PauseGame() {
        if(state == GameState.STOPPED) return;
        state = GameState.STOPPED;
        Time.timeScale = 0;
    }

    public void UnpauseGame() {
        if(state == GameState.RUNNING) return;
        state = GameState.RUNNING;
        Time.timeScale = 1;
    }
    
}
