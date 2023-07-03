using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody Player;
    private float speed = 8.0f;
    private float jumpVelocity = 8.0f;
    private float fallVelocity = 5.5f;
    private float lowJumpMultiplier = 9f;
    bool isGrounded;
    public Camera playerCamera;

    void Start()
    {
        Player = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            Player.MovePosition(Player.position + playerCamera.transform.forward * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S))
        {
            Player.MovePosition(Player.position - playerCamera.transform.forward * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A))
        {
            Player.MovePosition(Player.position - playerCamera.transform.right * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            Player.MovePosition(Player.position + playerCamera.transform.right * speed * Time.deltaTime);
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

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        if (collision.gameObject.CompareTag("Kill Zone"))
        {
            Player.transform.position = new Vector3(0, 15.25f, 0);
            Player.transform.rotation = new Quaternion(0, 0, 0, 0);
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
