using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour
{
    private Vector2 touchStartPos;
    private Vector2 touchEndPos;
    public GameObject ballPrefab;
    private Rigidbody rb;
    private float forceMultiplier = .08f;
    private bool hasShot = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    touchStartPos = touch.position;
                    break;

                case TouchPhase.Ended:
                    touchEndPos = touch.position;
                    Shoot(touchStartPos + touchEndPos);
                    break;
            }
        }
    }

    private void Shoot(Vector3 force)
    {
        if (!hasShot)
        {
            rb.transform.SetParent(null);
            rb.isKinematic = false;

            // Calculate the swipe direction and magnitude in screen space
            Vector2 swipeDirection = (touchEndPos - touchStartPos).normalized;
            float swipeMagnitude = (touchEndPos - touchStartPos).magnitude;

            // Convert the swipe direction to world space using the camera's orientation
            Vector3 worldSwipeDirection = Camera.main.transform.TransformDirection(swipeDirection.x, 0, swipeDirection.y);

            // Apply the magnitude of the swipe as the force multiplier
            Vector3 forceToApply = worldSwipeDirection * swipeMagnitude * forceMultiplier;

            // Apply the force to the Rigidbody
            rb.AddForce(forceToApply, ForceMode.Impulse);
            hasShot = true;
        }
    }

}