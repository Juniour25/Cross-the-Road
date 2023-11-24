using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanePool : MonoBehaviour
{
    public GameObject[] lanePrefabs;
    private Transform playerTransform;
    public float spawnZ = 0.0f;
    private float length = 20.0f;
    public float safeZone = 15.0f;
    private int maximumLanes = 3;
    private List<GameObject> activeLanes;

    private int maxCoinsPerLane = 20;
    private float zRange = 20.0f;


    public GameObject coinPrefab;
    // Start is called before the first frame update
    void Start()
    {
        SpawnCoins();
        activeLanes = new List<GameObject>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        for (int i = 0; i < maximumLanes; i++)
        {
            SpawnLane(0);

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform.position.z - safeZone > (spawnZ - maximumLanes * length))
        {
            SpawnLane(0);
            SpawnCoins();
            DeleteLane();
        }
    }
    private void SpawnLane(int prefab = -1)
    {
        GameObject go;
        go = Instantiate(lanePrefabs[0]) as GameObject;
        go.transform.SetParent(transform);
        go.transform.position = Vector3.forward * spawnZ;
        spawnZ += length;
        activeLanes.Add(go);
    }

    private void SpawnCoins()
    {
        for (int i = 0; i < maxCoinsPerLane; i++)
        {
            float randomX = Random.Range(0.0f, 20.0f); // Random X position in the lane
            float randomY = 1.0f; // Adjust this based on your desired Y position
            float randomZ = Random.Range(-zRange, zRange); // Random Z position within a specified range

            Vector3 spawnPosition = new Vector3(randomX, randomY, randomZ);

            Instantiate(coinPrefab, spawnPosition, Quaternion.identity);
        }
    }

    private void DeleteLane()
    {
        Destroy(activeLanes[0]);
        activeLanes.RemoveAt(0);
    }
}
