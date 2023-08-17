using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeCannon : MonoBehaviour
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

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(WaitAndBounce(collision.gameObject));
        }
    }

    IEnumerator WaitAndBounce(GameObject playerGameObject)
    {
        yield return new WaitForSeconds(3);

        PlayerController playerController = playerGameObject.GetComponent<PlayerController>();
        playerController.Bounce(bounceForce);
    }
}
