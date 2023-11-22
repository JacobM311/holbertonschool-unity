using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    // Static instance of the UIManager
    public static UIManager Instance { get; private set; }

    // List to hold Image components
    public List<Image> BallUIElements;

    void Awake()
    {
        // Check if an instance already exists
        if (Instance == null)
        {
            // Assign the current instance
            Instance = this;

            // Make sure this instance persists between scene loads
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // Destroy this instance because it's a duplicate
            Destroy(gameObject);
        }
    }

    // You can add methods here to interact with your UI elements
}
