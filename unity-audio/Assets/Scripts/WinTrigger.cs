using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinTrigger : MonoBehaviour
{
    public Text Timer;
    private Timer Script;
    public AudioSource Audio;

    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.Find("Player");
        Script = player.GetComponent<Timer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Script.StopTimer();
            Script.timerText.color = Color.green;
            Script.timerText.rectTransform.localScale = new Vector3(.5f, .5f, .5f);
            Audio.Stop();
        }

    }
}
