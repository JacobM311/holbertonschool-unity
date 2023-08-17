using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public Toggle invertYToggle;

    private bool temporaryInvertYState;

    private void Start()
    {
        // Initialize the toggle with the current setting value
        invertYToggle.isOn = SettingsManager.instance.isInverted;

        // Store the temporary state
        temporaryInvertYState = invertYToggle.isOn;
    }

    public void Apply()
    {
        // Set the SettingsManager's value to the temporary value.
        SettingsManager.instance.isInverted = temporaryInvertYState;

        string previousSceneName = SceneManagerHelper.instance.PreviousScene;
        if (!string.IsNullOrEmpty(previousSceneName))
        {
            SceneManager.LoadScene(previousSceneName);
        }
        else
        {
            Debug.LogWarning("Previous scene name is null or empty.");
        }
    }

    public void Back()
    {
        string previousSceneName = SceneManagerHelper.instance.PreviousScene;
        if (!string.IsNullOrEmpty(previousSceneName))
        {
            SceneManager.LoadScene(previousSceneName);
        }
        else
        {
            Debug.LogWarning("Previous scene name is null or empty.");
        }
    }

    public void OnInvertYToggleChanged(bool isOn)
    {
        temporaryInvertYState = isOn;
    }
}
