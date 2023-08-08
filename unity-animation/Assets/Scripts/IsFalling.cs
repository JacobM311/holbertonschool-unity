using UnityEngine;

public class IsFalling : MonoBehaviour
{
    public Animator playerAnimator;
    public bool hasFallen = false;
    bool playerInputReceived = false;
    private string gettingUpAnimationName = "GettingUp";
    public PlayerController playerScript;



    void Start()
    {
        GameObject playerGameObject = GameObject.FindGameObjectWithTag("Player"); // Assumes player has tag "Player".
        if (playerGameObject != null)
        {
            playerScript = playerGameObject.GetComponent<PlayerController>();
        }
    }

    void Update()
    {
        if (hasFallen == true)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.Space))
            {
                playerInputReceived = true;
                if (playerInputReceived == true)
                {
                    playerAnimator.SetBool("GettingUp", true);
                    if (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName(gettingUpAnimationName) && playerAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
                    {
                        if (playerScript != null)
                        {
                            playerScript.enabled = true;
                        }
                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
            playerController.enabled = false;
            playerAnimator.SetBool("IsFalling", true);

            hasFallen = true;
        }
    }

    public void ResetHasFallen()
    {
        hasFallen = false;
    }
}
