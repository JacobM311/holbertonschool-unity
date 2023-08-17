using UnityEngine;

public class IsFalling : MonoBehaviour
{
    public Animator playerAnimator;
    public bool hasFallen = false;
    public PlayerController playerScript;

    private string FlatImpactAnimation = "Falling Flat Impact";
    private string gettingUpAnimation = "Getting Up";

    void Start()
    {

    }

    void Update()
    {
        if (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName(FlatImpactAnimation) && playerAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            playerAnimator.SetBool("GettingUp", true);
        }

        if (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName(gettingUpAnimation) && playerAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            Debug.Log("Im getting up");
            playerScript.enabled = true;
            playerAnimator.SetBool("GettingUp", false);
            playerAnimator.SetBool("IsFalling", false);
            Debug.Log("IsFalling: " + playerAnimator.GetBool("IsFalling"));
            playerAnimator.SetBool("HasFallen", false);
            playerAnimator.SetBool("IsWalking", false);
            playerAnimator.SetBool("IsJumping", false);
            playerAnimator.SetBool("IsJumpFalling", false);
        }
    }

    public void GettingUpEnded()
    {
        Debug.Log("Im getting up");
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
}
