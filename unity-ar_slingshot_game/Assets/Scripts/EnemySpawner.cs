using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


public class EnemySpawner : MonoBehaviour
{
    public GameObject EnemyPrefab;
    private ARRaycastManager aRRaycastManager;
    public int numberOfEnemies = 5;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

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
        List<GameObject> spawnedEnemies = new List<GameObject>();

        GameObject firstEnemy = Instantiate(EnemyPrefab, plane.center, Quaternion.identity);
        spawnedEnemies.Add(firstEnemy);

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
}
