using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void Start()
    {
        
    }

    public void Update()
    {
        
    }

    public void LevelSelect(int Level)
    {
        PlayerPrefs.SetString("previousScene", SceneManager.GetActiveScene().name);

        if (Level == 1)
        {
            SceneManager.LoadScene("Level01");
            Time.timeScale = 1f;
        }
        if (Level == 2)
        {
            SceneManager.LoadScene("Level02");
            Time.timeScale = 1f;
        }
        if (Level == 3)
        {
            SceneManager.LoadScene("Level03");
            Time.timeScale = 1f;
        }
    }

    public void Options()
    {
        SceneManager.LoadScene("Options");
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Exited");
    }
}
