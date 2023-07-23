using System.Collections;
using System.Collections.Generic;
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
    public float groundCheckDistance = 0.1f;

    void Start()
    {
        Player = GetComponent<Rigidbody>();
        playerCollider = GetComponent<Collider>();
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
    }

    void RotateTowards(Vector3 direction)
    {
        Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 360f * Time.deltaTime);
    }

    bool IsGrounded()
    {
        // Cast a sphere slightly downwards to check if the player is grounded
        return Physics.SphereCast(transform.position, playerCollider.bounds.extents.x, Vector3.down, out RaycastHit hit, playerCollider.bounds.extents.y + groundCheckDistance, groundLayer);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Kill Zone"))
        {
            Player.transform.position = new Vector3(-221, -15, 62);
            Player.transform.rotation = new Quaternion(0, 180, 0, 0);
            Player.velocity = new Vector3(0, 0, 0);
        }

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
