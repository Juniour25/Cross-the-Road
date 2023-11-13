using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class logMovement : MonoBehaviour
{
    public float moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // move car forwards and destroy it when it reaches the end
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        if (transform.localPosition.z >= 0.7f)
        {
            Destroy(gameObject);
        }
    }
}
