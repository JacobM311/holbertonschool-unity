using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class InitialBallSpawn : MonoBehaviour
{
    public GameObject ballPrefab;
    private GameObject currentBall;
    private Vector3 offset = new Vector3(0, -0.2f, .8f);
    public List<Image> BallUIElements = new List<Image>();
    private float delayBeforeSpawning = 1.0f;
    private float delayAfterSpawning = 1.0f;
    private bool ballIsMoving = false;
    private bool hitEnemy = false;
    private int hits = 0;
    private int ballSpawns = 0;
    public int score = 0;
    public TMP_Text Score;
    private bool isGameOver = false;
    public Button resetButton;
    public EnemySpawner spawn;
    private bool gameOverScheduled = false;


    private Rigidbody rb;

    void Start()
    {
        ResetGame();
    }

    private void ResetGame()
    {
        ballSpawns = 0;
        hits = 0;
        score = 0;
        Score.text = score.ToString();
        isGameOver = false;
        resetButton.gameObject.SetActive(false);

        foreach (var uiElement in BallUIElements)
        {
            uiElement.gameObject.SetActive(true);
        }

        Invoke("SpawnBall", delayBeforeSpawning);
        gameOverScheduled = false;
    }

    public void SpawnBall()
    {
        if (isGameOver) return; // Stop spawning if the game is over

        if (ballSpawns <= BallUIElements.Count)
        {
            BallUIElements[ballSpawns].gameObject.SetActive(false);
        }

        if (ballSpawns >= 5)
        {
            CheckGameOver();
        }
        if (ballSpawns >= 5) return;

        Vector3 spawnPosition = Camera.main.transform.position + Camera.main.transform.rotation * offset;
        currentBall = Instantiate(ballPrefab, spawnPosition, Quaternion.identity);
        rb = currentBall.GetComponent<Rigidbody>();
        currentBall.transform.SetParent(Camera.main.transform);
        ballIsMoving = false;
        hitEnemy = false;

        ballSpawns++;
    }

    private void Update()
    {
        if (!ballIsMoving && !hitEnemy && currentBall != null && rb != null && rb.velocity != Vector3.zero)
        {
            ballIsMoving = true;
            ScheduleRespawn();
        }

        if (hitEnemy)
        {
            ScheduleRespawn();
        }

        if (ballSpawns == 5 && ballIsMoving && !gameOverScheduled)
        {
            gameOverScheduled = true; // Set the flag to true to avoid multiple scheduling
            Invoke("CheckGameOver", delayAfterSpawning);
        }
    }

    private void ScheduleRespawn()
    {
        hitEnemy = false;
        CancelInvoke("RespawnBall"); // Cancel any existing scheduled respawn
        Invoke("RespawnBall", delayAfterSpawning); // Schedule a new respawn
    }

    private void RespawnBall()
    {
        if (currentBall != null)
        {
            Destroy(currentBall);
        }
        if (ballSpawns < 5)
        {
            Invoke("SpawnBall", delayAfterSpawning);
        }
    }

    public void OnBallCollision()
    {
        hits++;
        hitEnemy = true;
        score += 10;
        Score.text = score.ToString();
    }

    private void CheckGameOver()
    {
        if (hits >= 5)
        {
            Debug.Log("You win!");
        }
        else
        {
            Debug.Log("Game over");
        }
        resetButton.gameObject.SetActive(true);
        spawn.enabled = true;
        spawn.ResetEnemies();
    }

    public void OnResetButtonPressed()
    {
        ResetGame();
    }

}