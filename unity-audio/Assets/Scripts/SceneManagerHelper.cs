using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerHelper : MonoBehaviour
{
    public static SceneManagerHelper instance;

    // Variable to hold the name of the previous scene
    public string PreviousScene { get; private set; }
    private string lastLoadedScene;
    public string CurrentScene { get; private set; }


    private void Awake()
    {
        if (instance == null)
        {
            CurrentScene = SceneManager.GetActiveScene().name;
            SceneManager.sceneLoaded += OnSceneLoaded;
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // Set an event to capture scene changes
        SceneManager.activeSceneChanged += OnActiveSceneChanged;
    }

    private void OnActiveSceneChanged(Scene previousScene, Scene newScene)
    {
        PreviousScene = previousScene.name;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PreviousScene = CurrentScene;
        CurrentScene = scene.name;

        // For debugging purposes:
        Debug.Log($"Previous Scene: {PreviousScene}, Current Scene: {CurrentScene}");
    }
}
