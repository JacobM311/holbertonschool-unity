using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 6f;
    private Rigidbody rb;
    private int score = 0;
    private int health = 5;
    public Text scoreText;
    public Text healthText;
    public Image WinLose;
    public Text WinLoseText;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        WinLose.enabled = false;
        WinLoseText.enabled = false;
        
    }

    void Update()
    {
        if (health == 0)
        {
            WinLose.color = Color.red;
            WinLoseText.color = Color.white;
            WinLoseText.text = "Game Over!";
            WinLoseText.enabled = true;
            WinLose.enabled = true;
            ReloadScene();
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene("menu");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Pickup")
        {
            score += 1;
            scoreText.text = "Score: " + score.ToString();
            Destroy(other.gameObject);
        }
        if (other.tag == "Trap")
        {
            health -= 1;
            healthText.text = "Health: " + health.ToString();
        }
        if (other.tag == "Goal")
        {
            WinLose.enabled = true;
            WinLoseText.enabled = true;
            WinLose.color = Color.green;
            WinLoseText.color = Color.black;
            WinLoseText.text = "You Win!";
            ReloadScene();
        }
    }

    IEnumerator LoadScene(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    public void ReloadScene()
    {
        StartCoroutine(LoadScene(3));
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(transform.forward * speed);
        }

        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(-transform.forward * speed);
        }

        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(-transform.right * speed);
        }

        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(transform.right * speed);
        }
    }
}
