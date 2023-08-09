using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody Player;
    private Collider playerCollider;
    private float speed = 8.0f;
    private float jumpVelocity = 8.0f;
    private float fallVelocity = 5.5f;
    private float lowJumpMultiplier = 9f;
    private float sprintSpeed = 12f;
    bool isGrounded;
    public Camera playerCamera;

    public LayerMask groundLayer;
    public float groundCheckDistance = 0.01f;

    Animator animator;
    private float jumpTimer = 0f;

    void Start()
    {
        Player = GetComponent<Rigidbody>();
        playerCollider = GetComponent<Collider>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {

        isGrounded = IsGrounded();

        Vector3 cameraForward = playerCamera.transform.forward;
        cameraForward.y = 0;  // now i ignore the y direction so the up and down movement of the camera dont effect the players movement
        cameraForward.Normalize();

        Vector3 cameraRight = playerCamera.transform.right;
        cameraRight.y = 0;
        cameraRight.Normalize();

        Vector3 moveDirection = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            moveDirection += cameraForward;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                Player.MovePosition(Player.position + cameraForward * sprintSpeed * Time.deltaTime);
            }
            else
            {
                Player.MovePosition(Player.position + cameraForward * speed * Time.deltaTime);
            }
        }

        if (Input.GetKey(KeyCode.S))
        {
            moveDirection -= cameraForward;
            Player.MovePosition(Player.position - cameraForward * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A))
        {
            moveDirection -= cameraRight;
            Player.MovePosition(Player.position - cameraRight * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            moveDirection += cameraRight;
            Player.MovePosition(Player.position + cameraRight * speed * Time.deltaTime);
        }

        bool isWalking = moveDirection != Vector3.zero;
        animator.SetBool("IsWalking", isWalking);

        if (moveDirection != Vector3.zero)
        {
            RotateTowards(moveDirection);
        }

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            Player.velocity = Vector3.up * jumpVelocity;
        }
        if (Player.velocity.y < 0)
        {
            Player.velocity += Vector3.up * Physics.gravity.y * (fallVelocity - 1) * Time.deltaTime;
        }
        else if (Player.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            Player.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.Space) && jumpTimer <= 0f)
        {
            animator.SetBool("IsJumping", true);
            jumpTimer = .08f;
        }

        if (jumpTimer >= 0f)
        {
            jumpTimer -= Time.deltaTime;
        }
        if (jumpTimer <= 0f && isGrounded)
        {
            animator.SetBool("IsJumping", false);
        }

        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (Player.velocity.y < 0 && !IsGrounded())
        {
            animator.SetBool("IsJumpFalling", true);
            Debug.Log("I am jump falling");
        }
        // Check if the "Jump" animation has finished playing
        else if (stateInfo.normalizedTime >= 1.0f && stateInfo.IsName("Jump"))
        {
            if (Player.velocity.y >= 0 || IsGrounded())
            {
                animator.SetBool("IsJumpFalling", false);
            }
        }
        // If not falling and not in the middle of a jump animation, reset the "JumpFalling" state.
        else
        {
            animator.SetBool("IsJumpFalling", false);
        }
    }

    public void GettingUpEnded()
    {
        Debug.Log("Im getting up");
        enabled = true;
        animator.SetBool("GettingUp", false);
        animator.SetBool("IsFalling", false);
        Debug.Log("IsFalling: " + animator.GetBool("IsFalling"));
        animator.SetBool("HasFallen", false);
        animator.SetBool("IsWalking", false);
        animator.SetBool("IsJumping", false);
        animator.SetBool("IsJumpFalling", false);
    }

    void RotateTowards(Vector3 direction)
    {
        Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 500f * Time.deltaTime);
    }

    bool IsGrounded()
    {
        // Cast a sphere slightly downwards to check if the player is grounded
        return Physics.SphereCast(transform.position, playerCollider.bounds.extents.x, Vector3.down, out RaycastHit hit, playerCollider.bounds.extents.y + groundCheckDistance, groundLayer);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Canoe"))
        {
            transform.parent = collision.transform;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Canoe"))
        {
            transform.parent = null;
        }
    }

    public void Bounce(float bounceForce)
    {
        Player.AddForce(Vector3.up * bounceForce, ForceMode.Impulse);
    }

}
