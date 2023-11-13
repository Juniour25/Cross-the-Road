using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    public Transform Player; // Reference to the player's transform
    public float followSpeed = 5f; // Speed at which the cam follows player
    public Vector3 offset; // Desired offset for the player


    private void Start()
    {
        offset = transform.position - Player.position;
    }
    // Update is called once per frame
    void LateUpdate()
    {
        if(Player == null)
        {
            // Make sure there is a player reference
            Debug.LogWarning("Player refernce has not been set to the camera.");
            return;
        }

        // calculate the target position for the camera
        Vector3 targetPosition = Player.position + offset;

        Vector3 newOffset = transform.position - Player.position;

        // Smoothly move the camera towards the target position
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

    }
}
