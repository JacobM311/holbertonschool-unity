using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour
{
    private Vector2 touchStartPos;
    private Vector2 touchEndPos;
    public GameObject ballPrefab;
    private Rigidbody rb;
    private float forceMultiplier = .03f;
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
                    Shoot();
                    break;
            }
        }
    }

    private void Shoot()
    {
        if (!hasShot)
        {
            rb.transform.SetParent(null);
            rb.isKinematic = false;

            Vector2 swipeDirection = (touchStartPos - touchEndPos).normalized;
            float swipeMagnitude = (touchStartPos - touchEndPos).magnitude;
            Vector3 worldSwipeDirection = Camera.main.transform.TransformDirection(swipeDirection.x, 0, swipeDirection.y);
            Vector3 forceToApply = worldSwipeDirection * swipeMagnitude * forceMultiplier;

            rb.AddForce(forceToApply, ForceMode.Impulse);
            hasShot = true;
        }
    }

}