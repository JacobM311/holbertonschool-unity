using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timerText;
    private float startTime;
    private bool timerActive = true;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (!timerActive)
            return;

        float t = Time.time - startTime;

        string minutes = ((int)t / 60).ToString();
        string seconds = ((int)t % 60).ToString("D2");
        string milliseconds = ((int)(t * 100) % 100).ToString("D2");

        timerText.text = minutes + ":" + seconds + "." + milliseconds;
    }

    public void StopTimer()
    {
        timerActive = false;
    }
}
