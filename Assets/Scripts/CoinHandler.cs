using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinHandler : MonoBehaviour
{
    private GameManager gameManager;
    public float rotationSpeed;

    void Start()
    {
        // Find the GameManager in the scene
        gameManager = FindObjectOfType<GameManager>();

        if (gameManager == null)
        {
            Debug.LogError("GameManager not found in the scene. Make sure it is present.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Increase score if player collides with the coin
        if (other.gameObject.CompareTag("Player"))
        {
            if (gameManager != null)
            {
                gameManager.IncreaseScore(10);
                Destroy(gameObject);
            }
        }
    }

    private void Update()
    {
        RotateCoin();
    }

    private void RotateCoin()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}
