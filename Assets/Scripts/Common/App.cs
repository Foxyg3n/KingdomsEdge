using UnityEngine;

public class App : MonoBehaviour {

    public static App instance;

    [SerializeField] private Backdrop backdrop;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameModeManager gameModeManager;
    [SerializeField] private IslandManager islandManager;
    [SerializeField] private AudioManager audioManager;

    public static Backdrop Backdrop { get { return instance.backdrop; } }
    public static GameManager GameManager { get { return instance.gameManager; } }
    public static GameModeManager GameModeManager { get { return instance.gameModeManager; } }
    public static IslandManager IslandManager { get { return instance.islandManager; } }
    public static AudioManager AudioManager { get { return instance.audioManager; } }
    
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Bootstrap() {
        var app = Object.Instantiate(Resources.Load("App")) as GameObject;
        if(app == null) throw new System.ApplicationException();

        Object.DontDestroyOnLoad(app);
    }

    private void Awake() {
        if(instance != null) throw new System.ApplicationException();
        instance = this;
        
        Application.runInBackground = true;
    }

    public void QuitGame() {
        Application.Quit();
    }

}
