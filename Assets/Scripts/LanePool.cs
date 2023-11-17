using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanePool : MonoBehaviour
{   public GameObject[] lanePrefabs;
    private Transform playerTransform;
    public float spawnZ=0.0f;
    private float length=20.0f;
    public float safeZone=15.0f;
    private int maximumLanes=3;
    private List<GameObject> activeLanes;
    // Start is called before the first frame update
    void Start()
    {   
        activeLanes=new List<GameObject>();
        playerTransform=GameObject.FindGameObjectWithTag("Player").transform;
        for (int i = 0; i < maximumLanes; i++)
        {
            SpawnLane(0);
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(playerTransform.position.z - safeZone>(spawnZ-maximumLanes*length)){
            SpawnLane(0);
            DeleteLane();
        }
    }
    private void SpawnLane(int prefab=-1){
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
