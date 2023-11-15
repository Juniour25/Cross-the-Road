using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class LanePool : MonoBehaviour
{   
    public GameObject[] lanePrefabs;
    private Transform playerTransform;
    private float spawnZ=0.0f;
    public float length;
    private int lanesOnScreen=3;
    private List<GameObject> activeLanes;
    private float safeZone=15.0f;
    // Start is called before the first frame update
    private void Start()
    {   activeLanes=new List<GameObject>();
        playerTransform=GameObject.FindGameObjectWithTag("Player").transform;
        for (int i = 0; i < lanesOnScreen; i++)
        {
            SpawnLane(0);
        }
    }
    private void Update(){
        if(playerTransform.position.z-safeZone>(spawnZ-lanesOnScreen*length)){
            SpawnLane(0);
            DeleteLane();
        }
    }
    
    // Update is called once per frame
    private void SpawnLane(int prefabIndex=-1){
        GameObject go;
        go=Instantiate(lanePrefabs[0]) as GameObject;
        go.transform.SetParent(transform);
        go.transform.position=Vector3.forward*spawnZ;
        spawnZ+=length;
        activeLanes.Add(go);
    }
    private void DeleteLane(){
        Destroy(activeLanes[0]);
        activeLanes.RemoveAt(0);
    }
    
}
