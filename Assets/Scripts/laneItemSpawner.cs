using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laneItemSpawner : MonoBehaviour
{
    public GameObject[] items;
    public int NumOfItemsToSpawn;
    public List<Vector3> lanePos = new List<Vector3>();
    
    

    // Start is called before the first frame update
    void Start()
    {
        
            GenerateSegment();
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GenerateSegment(){
        for(int i = 0; i < NumOfItemsToSpawn; i++)
        {
            int randPos = Random.Range(0, lanePos.Count);
            int randItems = Random.Range(0, items.Length);
            GameObject newItem = Instantiate(items[randItems], lanePos[randPos], transform.rotation, this.gameObject.transform);
            newItem.transform.localPosition = lanePos[randPos];
            lanePos.Remove(lanePos[randPos]);
        }
        
    }
}
