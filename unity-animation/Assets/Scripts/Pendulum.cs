using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pendulum : MonoBehaviour
{
    public float speed = 2.0f;     // speed of the swing
    public float maxRotation = 15.0f; // max rotation from the start in degrees

    private Quaternion start, end;

    void Start()
    {
        start = PendulumRotation(maxRotation);
        end = PendulumRotation(-maxRotation);
    }

    void Update()
    {
        float angle = maxRotation * Mathf.Sin(Time.time * speed);
        transform.rotation = Quaternion.Euler(angle, 0, 0);
    }


    Quaternion PendulumRotation(float angle)
    {
        var pos = transform.rotation;
        var target = pos * Quaternion.Euler(angle, 0, 0);
        return target;
    }
}
