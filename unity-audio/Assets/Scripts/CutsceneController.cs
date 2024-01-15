using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneController : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject Player;
    public Canvas timerCanvas;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera.enabled = false;
        timerCanvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnAnimationEnd()
    {
        mainCamera.enabled = true;
        gameObject.SetActive(false);
        PlayerController playerscript = Player.gameObject.GetComponent<PlayerController>();
        playerscript.enabled = true;
        timerCanvas.enabled = true;
    }
}
