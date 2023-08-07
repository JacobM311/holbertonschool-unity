using UnityEngine;

public class IsFalling : MonoBehaviour
{
    public Animator playerAnimator;
    public bool hasFallen = false;

    void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
            playerController.enabled = false;
            playerAnimator.SetBool("IsFalling", true);

            Vector3 adjustedPosition = other.transform.position;
            adjustedPosition.y -= 1f;
            other.transform.position = adjustedPosition;

            hasFallen = true;
        }
    }

    public void ResetHasFallen()
    {
        hasFallen = false;
    }
}
