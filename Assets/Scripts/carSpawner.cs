using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carSpawner : MonoBehaviour
{
    public float spawnRate, startTime;
    public GameObject[] Vehicles;

    // Start is called before the first frame update
    void Start()
    {
        spawnVehicles();
        InvokeRepeating("spawnVehicles", startTime, spawnRate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // method for spawning vehicles
    public void spawnVehicles()
    {
        int randV = Random.Range(0, Vehicles.Length);
        Vector3 vehiclePos = Vehicles[randV].transform.localPosition;
        GameObject newV = Instantiate(Vehicles[randV], transform.position, transform.rotation, this.gameObject.transform);
        newV.transform.localPosition = vehiclePos;
    }
}
