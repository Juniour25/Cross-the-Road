using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinHandler : MonoBehaviour
{
    private GameManager gameManager;
    private SoundManager soundManager;
    public float rotationSpeed;

    void Start()
    {   
        soundManager=GameObject.Find("Sound Manager").GetComponent<SoundManager>();
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
            {   soundManager.PlayCoinCollect();
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
