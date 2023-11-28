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
    private int maxCoinsPerLane = 70;


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
            DeleteAllCoins();
            DeleteLane();

            SpawnLane(0);
            SpawnCoins();
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
        GameObject player = GameObject.Find("Player");

        if (player != null)
        {
            for (int i = 0; i < maxCoinsPerLane; i++)
            {
                float randomX = Random.Range((int)-safeZone, (int)safeZone + 1); // Random X position in the lane
                float randomY = 1.0f;
                float randomZ = Random.Range(player.transform.position.z - length, player.transform.position.z + length); // this should follow the lane as it moves

                Vector3 spawnPosition = new Vector3(randomX, randomY, randomZ);

                Instantiate(coinPrefab, spawnPosition, Quaternion.identity);
            }
        }
    }

    private void DeleteLane()
    {
        Destroy(activeLanes[0]);
        activeLanes.RemoveAt(0);
    }

    private void DeleteAllCoins()
    {
        CoinHandler[] coinHandlers = GameObject.FindObjectsOfType<CoinHandler>();

        foreach (CoinHandler coinHandler in coinHandlers)
        {
            Destroy(coinHandler.gameObject);
        }
    }
}
