using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject optionsMenu;
    public Material trapMat;
    public Material goalMat;
    public Toggle colorblindMode;

    // Start is called before the first frame update
    void Start()
    {
        //this checks PlayerPrefs, which is a key value pair, for the key "ToggleState"
        //if there is no key named "ToggleState", then it will return a default value of 0
        colorblindMode.isOn = PlayerPrefs.GetInt("ToggleState", 0) == 1;
        //this calls the OnToggleValueChanged function everytime the toggle button is toggled
        colorblindMode.onValueChanged.AddListener(OnToggleValueChanged);
    }

    void OnToggleValueChanged(bool isOn)
    {
        //this updates the stored value in PlayerPrefs when the Toggle's state changes.
        PlayerPrefs.SetInt("ToggleState", isOn ? 1 : 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayMaze()
    {
        if (colorblindMode.isOn)
        {
            trapMat.color = new Color32(255, 112, 0, 1);
            goalMat.color = Color.blue;
        }
        else
        {
            trapMat.color = Color.red;
            goalMat.color = Color.green;
        }
        SceneManager.LoadScene("maze");
    }

    public void QuitMaze()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }

    public void OptionButtonPressed()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void BackButtonPressed()
    {
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
    }
}
