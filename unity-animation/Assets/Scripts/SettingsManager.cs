using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    private static SettingsManager _instance;
    public static SettingsManager instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject sm = new GameObject("SettingsManager");
                _instance = sm.AddComponent<SettingsManager>();
                DontDestroyOnLoad(sm);
            }
            return _instance;
        }
    }

    public bool isInverted;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void HandleYAxisInversionToggle(bool isToggled)
    {
        isInverted = isToggled;
    }
}
