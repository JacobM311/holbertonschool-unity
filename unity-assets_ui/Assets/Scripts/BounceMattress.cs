using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceMattress : MonoBehaviour
{
    public float bounceForce = 150f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            return;
        }
        else
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            playerController.Bounce(bounceForce);
        }
    }
}
