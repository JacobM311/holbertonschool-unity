using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class EnemySpawner : MonoBehaviour
{
    public GameObject EnemyPrefab;
    private ARPlaneManager planeManager;
    public Vector3 offset = new Vector3(0, 0, 0);

    void Start()
    {
        planeManager = FindObjectOfType<ARPlaneManager>();
        if (planeManager != null)
        {
            planeManager.planesChanged += OnPlaneChanged;
        }
    }

    void OnPlaneChanged(ARPlanesChangedEventArgs args)
    {
        foreach (var plane in args.added)
        {
            Vector3 spawnPosition = plane.transform.position + offset;
            Instantiate(EnemyPrefab, spawnPosition, Quaternion.identity);
            Debug.Log("Enemy placed");

        }
    }
}