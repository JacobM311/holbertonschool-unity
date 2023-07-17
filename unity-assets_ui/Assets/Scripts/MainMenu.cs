using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadScene01()
    {
        SceneManager.LoadScene("Level01");
    }

    public void LoadScene02()
    {
        SceneManager.LoadScene("Level02");
    }

    public void LoadScene03()
    {
        SceneManager.LoadScene("Level03");
    }

    public void LoadOptions()
    {
        SceneManager.LoadScene("OptionsMenu");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
