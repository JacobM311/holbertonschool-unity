using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    CameraController cameraController;

    public void Start()
    {
        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
        if (camera != null)
        {
            // Get a reference to the script
            cameraController = camera.GetComponent<CameraController>();
        }
    }

    public void Update()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu" && cameraController != null)
        {
            cameraController.enabled = false;
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
    public void LevelSelect(int Level)
    {
        PlayerPrefs.SetString("previousScene", SceneManager.GetActiveScene().name);

        if (Level == 1)
        {
            SceneManager.LoadScene("Level01");
        }
        if (Level == 2)
        {
            SceneManager.LoadScene("Level02");
        }
        if (Level == 3)
        {
            SceneManager.LoadScene("Level03");
        }
    }

    public void Options()
    {
        PlayerPrefs.SetString("previousScene", SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("Options");
    }

    public void Back()
    {
        // Check if there is a saved scene
        if (PlayerPrefs.HasKey("previousScene"))
        {
            string previousScene = PlayerPrefs.GetString("previousScene");
            SceneManager.LoadScene(previousScene);
        }
        else
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
}
