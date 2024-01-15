using UnityEngine;

public class KillZoneTrigger : MonoBehaviour
{
    private Rigidbody playerRigidbody;

    private void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            playerRigidbody = playerObject.GetComponent<Rigidbody>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && playerRigidbody != null)
        {
            playerRigidbody.transform.position = new Vector3(-221, 10, 62);
            playerRigidbody.transform.rotation = Quaternion.Euler(0, 180, 0);
            PlayerController playerController = other.gameObject.GetComponent<PlayerController>();

            Vector3 currentVelocity = playerRigidbody.velocity;
            currentVelocity.x = 0;
            playerRigidbody.velocity = currentVelocity;
        }
    }
}

