using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class Follower : MonoBehaviour
{
    public PathCreator pathCreator;
    public float speed = 5;
    float distanceTravelled;
    bool startFollowing = false;
    

    void Update()
    {
        if (startFollowing)
        {
            distanceTravelled += speed * Time.deltaTime;
            transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled);
            transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the collision is with the object you want
        if (other.gameObject.CompareTag("Player"))
        {
            startFollowing = true;
        }
    }
}
