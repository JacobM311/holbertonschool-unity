using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    public AudioSource clickSound;

    public void LevelSelect(int Level)
    {
        PlayerPrefs.SetString("previousScene", SceneManager.GetActiveScene().name);

        StartCoroutine(PlaySoundAndLoadLevel(Level));
    }

    IEnumerator PlaySoundAndLoadLevel(int Level)
    {
        if (clickSound != null)
        {
            clickSound.Play();
            yield return new WaitForSeconds(clickSound.clip.length);
        }

        switch (Level)
        {
            case 1:
                SceneManager.LoadScene("Level01");
                Time.timeScale = 1f;
                break;
            case 2:
                SceneManager.LoadScene("Level02");
                Time.timeScale = 1f;
                break;
            case 3:
                SceneManager.LoadScene("Level03");
                Time.timeScale = 1f;
                break;
        }
    }

    public void Options()
    {
        StartCoroutine(PlaySoundAndLoadOptions());
    }

    IEnumerator PlaySoundAndLoadOptions()
    {
        if (clickSound != null)
        {
            clickSound.Play();
            yield return new WaitForSeconds(clickSound.clip.length);
        }

        SceneManager.LoadScene("Options");
    }

    public void Quit()
    {
        StartCoroutine(PlaySoundAndQuit());
    }

    IEnumerator PlaySoundAndQuit()
    {
        if (clickSound != null)
        {
            clickSound.Play();
            yield return new WaitForSeconds(clickSound.clip.length);
        }

        Application.Quit();
    }
}
