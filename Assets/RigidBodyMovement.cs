using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidBodyMovement : MonoBehaviour
{
    public Transform leftPos;
    public Transform playerPos;
    public Transform rightPos;

    //Rigid body
    Rigidbody2D rb;

    //Movement vectors for each possible direction
    Vector2 playerJumpVerticalUpVelocity = new Vector2(0, 10);
    Vector2 playerJumpVerticalDownVelocity = new Vector2(0, -10);
    Vector2 playerJumpLeftVelocity = new Vector2(-8, 2);
    Vector2 playerJumpRightVelocity = new Vector2(8, -2);

    //Booleans for key input
    bool playerJumpVerticalPress = false;
    //bool playerJumpHorizontalPress = false;
    bool notMovingPlayerHorizontal = true;
    bool movingPlayerHorizontal = false;

    bool hitLeft = false;
    bool hitRight = false;

    //Horizontal Direction
    //float playerHorizontalDirection;

    List<Vector3> railPositions;
    int railPosIdx = 1;

    // Start is called before the first frame update
    void Start()
    {
        railPositions = new List<Vector3>();
        railPositions.Add(leftPos.position);
        railPositions.Add(playerPos.position);
        railPositions.Add(rightPos.position);
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        //Check if vertical button is pressed. NOTE: Pressing 'S' will also jump.
        if (playerJumpVerticalPress)
        {
            //Set key press to false so that the function is not called multiple times
            playerJumpVerticalPress = false;

            //Apply upwards vertical velocity
            rb.velocity = playerJumpVerticalUpVelocity;
            StartCoroutine("VerticalJumpUpTimer");
        }

        //Check if left is pressed
        if (hitLeft && false)
        {
            //Set key press to false so that the function is not called multiple times
            //playerJumpHorizontalPress = false;
            
           // Debug.Log("hit left");
           // playerInputEnabled = false;
            //hitLeft = false;
            //Apply left horizontal velocity
            //rb.velocity = playerJumpLeftVelocity;
            //StartCoroutine("HorizontalJumpTimer");
        }
        else if (hitRight && false) //if(playerJumpHorizontalPress && playerHorizontalDirection > 0) //Check if right is pressed
        {
            //playerInputEnabled = false;
            //hitRight = false;
            //Set key press to false so that the function is not called multiple times
            //playerJumpHorizontalPress = false;
            //hitRight = false;
           // rb.velocity = playerJumpRightVelocity;
            //StartCoroutine("HorizontalJumpTimer");
            //Apply right horizontal velocity
            //rb.velocity = playerJumpRightVelocity;
            //StartCoroutine("HorizontalJumpTimer");
        }
    }
    // Update is called once per frame
    //Checks for key presses and if player input is enabled
    void Update()
    {
        if (movingPlayerHorizontal && transform.position != railPositions[railPosIdx])
        {
            Debug.Log("ok?");
            transform.position = Vector3.MoveTowards(transform.position, railPositions[railPosIdx], Time.deltaTime * 10); //can also be Lerp instead
        }
        if (transform.position == railPositions[railPosIdx])
        {
            Debug.Log("ok?2");
            movingPlayerHorizontal = false;
        }

        if (Input.GetKeyDown(KeyCode.Q) && !movingPlayerHorizontal && notMovingPlayerHorizontal)
        {
            
            if (railPosIdx > 0)
            {
                movingPlayerHorizontal = true;
                railPosIdx--;
                /*while (transform.position != railPositions[railPosIdx])
                {
                    transform.position = Vector3.Lerp(transform.position, railPositions[railPosIdx], Time.deltaTime * 10);
                }*/
            }
            //while (transform.position !=)
            
            
        }
        if (Input.GetKeyDown(KeyCode.E) && !movingPlayerHorizontal && notMovingPlayerHorizontal)
        {
            if (railPosIdx < 2)
            {
                movingPlayerHorizontal = true;
                railPosIdx++;
                /*while (transform.position != railPositions[railPosIdx])
                {
                    transform.position = Vector3.Lerp(transform.position, railPositions[railPosIdx], Time.deltaTime * 10);
                }*/
            }
        }
        if (Input.GetKeyDown(KeyCode.W) && notMovingPlayerHorizontal && !movingPlayerHorizontal)
        {
            notMovingPlayerHorizontal = false;
            playerJumpVerticalPress = true;
            //StartCoroutine("VerticalJumpUpTimer");
        }
        if ((hitLeft || hitRight) && notMovingPlayerHorizontal)
        {
            //transform.MovePosition(leftPos.position * Time.deltaTime * 10);
            //Debug.Log("hit horizontal jump");
            //playerInputEnabled = false;
            //rb.velocity = playerJumpLeftVelocity;
            //StartCoroutine("HorizontalJumpTimer");
            //playerJumpHorizontalPress = true;
            //playerHorizontalDirection = Input.GetAxisRaw("Horizontal");
        }
    }

    //Keeps applied horizontal velocity of jump for X seconds before stopping velocity and enabling player input again
    IEnumerator HorizontalJumpTimer()
    {
        Debug.Log("HorizontalJumpTimer");
        yield return new WaitForSeconds(.25f);
        rb.velocity = Vector2.zero;
        notMovingPlayerHorizontal = true;
    }

    //Keeps applied upwards velocity of jump for X seconds before stopping. Then applies downwards velocity
    IEnumerator VerticalJumpUpTimer()
    {
        yield return new WaitForSeconds(.1f);
        rb.velocity = playerJumpVerticalDownVelocity;
        StartCoroutine("VerticalJumpDownTimer");
    }

    //Keeps applied downwards velocity of jump for X seconds before stopping and enabling player input again
    IEnumerator VerticalJumpDownTimer()
    {
        yield return new WaitForSeconds(.12f);
        rb.velocity = Vector2.zero;
        notMovingPlayerHorizontal = true;
    }
}
