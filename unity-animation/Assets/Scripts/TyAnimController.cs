using UnityEngine;

public class TyAnimController : MonoBehaviour
{
    private Animator animator;
    private PlayerController playerScript;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        playerScript = playerObject.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GettingUpStart()
    {
        animator.SetBool("IsFalling", false);
        animator.SetBool("HasFallen", false);
        animator.SetBool("IsJumping", false);
        animator.SetBool("IsJumpFalling", false);
        animator.SetBool("IsWalking", false);
    }

    public void GettingUpEnded()
    {
        animator.SetBool("GettingUp", false);
        if (playerScript != null)
        {
            playerScript.enabled = true;
        }
    }
}
