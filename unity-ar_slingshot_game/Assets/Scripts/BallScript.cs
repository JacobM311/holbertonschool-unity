using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BallScript : MonoBehaviour
{
    private InitialBallSpawn spawner;

    void Start()
    {
        spawner = FindFirstObjectByType<InitialBallSpawn>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(collision.gameObject);

            if (spawner != null)
            {
                spawner.SpawnBall();
                spawner.OnBallCollision();
            }

            Destroy(gameObject);
        }
    }
}