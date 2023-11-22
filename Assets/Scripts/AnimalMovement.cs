using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalMovement : MonoBehaviour
{
    //public float moveDistance = 1f; // Distance to move on the Z-axis
    public float rotationAngleLR = 90f; // Rotation angle for left/right swipe
    public float rotationAngleUD = 180f; // Rotation angle for up/down swipe

    private Vector3 targetPosition;
    private Quaternion targetRotation;


    private Vector3 curMousePos;
    private Animator animator;

    public bool facingUp = true; 
    public bool facingRight = false;
    public bool facingLeft = false;
    public bool facingDown = false;

    public float zPos, xPos;

    public ParticleSystem waterSplash;

    void Start()
    {
        targetPosition = transform.position;
        targetRotation = transform.rotation;
        animator = GetComponent<Animator>();
        animator.SetTrigger("IsIdel");
    }

    void Update()
    {
        
        // Check for mouse input
        if (Input.GetMouseButtonDown(0))
        {
            targetPosition = transform.position; // Initialize the target position to the current position
            curMousePos = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            Vector3 swipeDelta = Input.mousePosition - curMousePos;

            //animator.SetTrigger("hasJump");
            
            // Calculate the target position
            if (swipeDelta.x > 0 && swipeDelta.x > swipeDelta.y && transform.position.x < 12) // Swipe right
            {
                targetPosition += transform.right;
                if (facingRight && !facingUp && !facingDown && !facingLeft) // facing right
                {
                    // Play anim
                    animator.SetTrigger("rightJump");
                }
                if (facingUp && !facingRight && !facingDown && !facingLeft) // facing up
                {
                    // Play anim
                    animator.SetTrigger("upToRightRot");
                    facingUp = false; facingRight = true;
                }
                if (facingLeft && !facingRight && !facingDown && !facingUp) // facing left
                {
                    // Play anim
                    animator.SetTrigger("leftToRightRot");
                    facingLeft = false; facingRight = true;
                }
                if (facingDown && !facingRight && !facingLeft && !facingUp) // facing down
                {
                    // Play anim
                    animator.SetTrigger("downToRightRot");
                    facingDown = false; facingRight= true;
                }
                
            }
            else if (swipeDelta.x < 0 && swipeDelta.x < swipeDelta.y  && transform.position.x > -12) // Swipe left
            {
                targetPosition += transform.right * -1;
                if (facingLeft && !facingUp && !facingDown && !facingRight) // facing left
                {
                    // Play anim
                    animator.SetTrigger("leftJump");
                }
                if (facingUp && !facingRight && !facingDown && !facingLeft) // facing up
                {
                    // Play anim
                    animator.SetTrigger("upToLeftRot");
                    facingUp = false; facingLeft = true;
                }
                if (facingRight && !facingLeft && !facingDown && !facingUp) // facing Right
                {
                    // Play anim
                    animator.SetTrigger("rightToLeftRot");
                    facingRight = false; facingLeft = true;
                }
                if (facingDown && !facingRight && !facingLeft && !facingUp) // facing down
                {
                    // Play anim
                    animator.SetTrigger("downToLeftRot");
                    facingDown = false; facingLeft = true;
                }
                
            }
            else if (swipeDelta.y > 0 && swipeDelta.y > swipeDelta.x) // Swipe up
            {
                targetPosition += transform.forward;
                if (facingUp && !facingRight && !facingDown && !facingLeft) // facing up
                {
                    // Play anim
                    animator.SetTrigger("hasJump");
                }
                if (facingRight && !facingUp && !facingDown && !facingLeft) // facing right
                {
                    // Play anim
                    animator.SetTrigger("rightToUpRot");
                    facingRight = false; facingUp = true;
                }
                if (facingLeft && !facingRight && !facingDown && !facingUp) // facing left
                {
                    // Play anim
                    animator.SetTrigger("leftToUpRot");
                    facingLeft = false; facingUp = true;
                }
                if (facingDown && !facingRight && !facingLeft && !facingUp) // facing down
                {
                    // Play anim
                    animator.SetTrigger("downToUpRot");
                    facingDown = false; facingUp = true;
                }
                
            }
            else if (swipeDelta.y < 0 && swipeDelta.y < swipeDelta.x && transform.position.z > -4f) // Swipe down
            {
                targetPosition += transform.forward * -1;
                if (facingDown && !facingUp && !facingRight && !facingLeft) // facing down
                {
                    // Play anim
                    animator.SetTrigger("backJump");
                }
                if (facingRight && !facingUp && !facingDown && !facingLeft) // facing right
                {
                    // Play anim
                    animator.SetTrigger("rightToDownRot");
                    facingRight = false; facingDown = true;
                }
                if (facingLeft && !facingRight && !facingDown && !facingUp) // facing left
                {
                    // Play anim
                    animator.SetTrigger("leftToDownRot");
                    facingLeft = false; facingDown = true;
                }
                if (facingUp && !facingRight && !facingLeft && !facingDown) // facing up
                {
                    // Play anim
                    animator.SetTrigger("upToDownRot");
                    facingUp = false; facingDown = true;
                }
                
            }
            Invoke("startMove", 0.12f);
            

            // Rotate towards the target rotation
            //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10f);

        }
    }

    public void startMove()
    {
        // Move towards the target position
        
        transform.position = Vector3.Lerp(transform.position, targetPosition, 10f);
    }

    private void GameOver()
    {
        GameManager.singleton.LoadMainMenu();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("water"))
        {
            // Player drowns
            waterSplash.Play();
            this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            this.enabled = false;
            Invoke("GameOver", 1f);
        }

        if (collision.gameObject.CompareTag("vehicle"))
        {
            // player gets hit by a car
            animator.SetTrigger("IsDead");
            this.enabled = false;
            Invoke("GameOver", 1f);
        }

        if (collision.gameObject.CompareTag("obstacle"))
        {
            if ((facingRight && !facingUp && !facingDown && !facingLeft) || (facingLeft && !facingRight && !facingDown && !facingUp)) // facing right or left
            {
                xPos += 0;
            }
            if ((facingUp && !facingRight && !facingDown && !facingLeft) || (facingDown && !facingRight && !facingLeft && !facingUp)) // facing up or down
            {
                zPos += 0;
            }
            transform.position = new Vector3(xPos, transform.position.y, zPos);
        }
        if (collision.gameObject.CompareTag("log"))
        {
            collision.gameObject.GetComponent<Animator>().SetTrigger("IsCarry");
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        
    }

    // make animal to stay on log
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            zPos = Mathf.Round(transform.position.z);
            xPos = Mathf.Round(transform.position.x);
        }
        transform.position = new Vector3(Mathf.Round(transform.position.x), transform.position.y, Mathf.Round(transform.position.z));
        if (collision.gameObject.CompareTag("log"))
        {
            this.transform.position = new Vector3(collision.transform.position.x, transform.position.y, transform.position.z);
            if(this.transform.position.x > 12 || this.transform.position.x < -12)
            {
                Debug.Log("Game Over");
                GameOver();
            }
        }
    }
}