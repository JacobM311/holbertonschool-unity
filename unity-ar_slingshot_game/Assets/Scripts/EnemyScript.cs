using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class EnemyScript : MonoBehaviour
{
    public float moveSpeed = 0.1f; // Speed of movement
    private float changeDirectionTime = 0f;
    private Vector3 randomDirection;

    void Update()
    {
        // Check if it's time to change direction
        if (Time.time >= changeDirectionTime)
        {
            // Set a new random direction
            randomDirection = new Vector3(
                Random.Range(-moveSpeed, moveSpeed), // Random value for x
                0,                                    // Keep y unchanged
                Random.Range(-moveSpeed, moveSpeed)  // Random value for z
            );

            // Set the next time to change direction
            changeDirectionTime = Time.time + Random.Range(1f, 3f);
        }

        // Move the enemy in the random direction
        transform.position += randomDirection * Time.deltaTime;
    }
}
