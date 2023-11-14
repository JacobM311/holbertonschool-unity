using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour
{
    private Vector2 touchStartPos;
    private Vector2 touchEndPos;
    public GameObject ballPrefab;
    private GameObject currentBall;
    private Rigidbody rb;
    private float forceMultiplier = 1f;
    private bool hasShot = false;

    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Check for touch input
        if (Input.touchCount > 0)
        {
            
            // Get the first touch
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    touchStartPos = touch.position;
                    Debug.Log("start" + touchStartPos);
                    break;

                case TouchPhase.Ended:
                    touchEndPos = touch.position;
                    Debug.Log("end" + touchEndPos);
                    Shoot(touchStartPos - touchEndPos);
                    break;
            }
        }
    }

    private void Shoot(Vector2 force)
    {
        if (!hasShot)
        {
            rb.transform.SetParent(null);
            rb.isKinematic = false;

            // Apply force to the Rigidbody
            rb.AddForce(new Vector3(force.x, force.y, force.y) * forceMultiplier);
            hasShot = true;
        }
    }
}
