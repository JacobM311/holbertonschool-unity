using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialBallSpawn : MonoBehaviour
{
    public GameObject ballPrefab;
    private GameObject currentBall;
    private Vector3 offset = new Vector3 (0, -0.2f, .8f);
    // Start is called before the first frame update
    void Start()
    {
        Vector3 ballPositionInFrontOfCamera = Camera.main.transform.position + offset;
        currentBall = Instantiate(ballPrefab, ballPositionInFrontOfCamera, Quaternion.identity);

        // Set the ball as a child of the camera
        currentBall.transform.SetParent(Camera.main.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
