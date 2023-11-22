using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using TMPro;


public class EnemySpawner : MonoBehaviour
{
    public GameObject EnemyPrefab;
    private ARRaycastManager aRRaycastManager;
    public int numberOfEnemies = 5;
    public TMP_Text textToDisable;
    public TMP_Text textToEnable;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();
    private List<GameObject> spawnedEnemies = new List<GameObject>();
    public GameObject images;
    public InitialBallSpawn Script;
    private ARPlane storedPlane;

    void Start()
    {
        aRRaycastManager = GetComponent<ARRaycastManager>();
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                if (aRRaycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
                {
                    ARPlane hitPlane = (ARPlane)hits[0].trackable;
                    SpawnEnemiesOnPlane(hitPlane);
                }
            }
        }
    }

    private void SpawnEnemiesOnPlane(ARPlane plane)
    {
        GameObject firstEnemy = Instantiate(EnemyPrefab, plane.center, Quaternion.identity);
        spawnedEnemies.Add(firstEnemy);
        textToDisable.gameObject.SetActive(false);
        textToEnable.gameObject.SetActive(true);
        images.SetActive(true);
        storedPlane = plane;

        for (int i = 1; i < numberOfEnemies; i++)
        {
            Vector3 spawnPosition;
            bool positionFound;

            do
            {
                positionFound = true;
                spawnPosition = GetRandomPositionAround(firstEnemy.transform.position, plane.extents);

                foreach (GameObject enemy in spawnedEnemies)
                {
                    if (IsColliding(spawnPosition, enemy.transform.position))
                    {
                        positionFound = false;
                        break;
                    }
                }
            }
            while (!positionFound);

            GameObject newEnemy = Instantiate(EnemyPrefab, spawnPosition, Quaternion.identity);
            spawnedEnemies.Add(newEnemy);
        }
        Script.enabled = true;
        this.enabled = false;
    }

    private bool IsColliding(Vector3 position1, Vector3 position2)
    {
        float minDistance = .05f;
        float distance = Vector3.Distance(position1, position2);
        return distance < minDistance;
    }

    private Vector3 GetRandomPositionAround(Vector3 center, Vector3 extents)
    {
        float randomX = Random.Range(-extents.x, extents.x);
        float randomZ = Random.Range(-extents.z, extents.z);
        return new Vector3(center.x + randomX, center.y, center.z + randomZ);
    }

    public void ResetEnemies()
    {
        foreach (var enemy in spawnedEnemies)
        {
            Destroy(enemy);
        }
        spawnedEnemies.Clear();

        //respawn enemies on the stored plane
        if (storedPlane != null)
        {
            SpawnEnemiesOnPlane(storedPlane);
        }
    }

    public ARPlane GetStoredPlane()
    {
        return storedPlane;
    }
}