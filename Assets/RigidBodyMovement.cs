using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidBodyMovement : MonoBehaviour
{
    public Transform leftPos;
    public Transform rightPos;

    //Rigid body
    Rigidbody2D rb;

    //Movement vectors for each possible direction
    Vector2 playerJumpVerticalUpVelocity = new Vector2(0, 10);
    Vector2 playerJumpVerticalDownVelocity = new Vector2(0, -10);

    //Booleans for key input
    bool playerJumpVerticalPress = false;
    bool movingPlayerVertical = false;
    bool movingPlayerHorizontal = false;

    List<Vector3> railPositions;
    int railPosIdx = 1;

    // Start is called before the first frame update
    void Start()
    {
        railPositions = new List<Vector3>();
        railPositions.Add(leftPos.position);
        railPositions.Add(transform.position);
        railPositions.Add(rightPos.position);
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        //Check if vertical button is pressed.
        if (playerJumpVerticalPress)
        {
            //Set key press to false so that the function is not called multiple times
            playerJumpVerticalPress = false;

            //Apply upwards vertical velocity
            rb.velocity = playerJumpVerticalUpVelocity;
            StartCoroutine("VerticalJumpUpTimer");
        }
    }
    // Update is called once per frame
    //Checks for key presses and if player input is enabled
    void Update()
    {
        if (movingPlayerHorizontal && transform.position != railPositions[railPosIdx])
        {
            transform.position = Vector3.MoveTowards(transform.position, railPositions[railPosIdx], Time.deltaTime * 10); //can also be Lerp instead
        }
        if (transform.position == railPositions[railPosIdx])
        {
            movingPlayerHorizontal = false;
        }

        if (Input.GetKeyDown(KeyCode.Q) && !movingPlayerHorizontal && !movingPlayerVertical)
        {
            if (railPosIdx > 0)
            {
                movingPlayerHorizontal = true;
                railPosIdx--;
            }
            
        }
        if (Input.GetKeyDown(KeyCode.E) && !movingPlayerHorizontal && !movingPlayerVertical)
        {
            if (railPosIdx < 2)
            {
                movingPlayerHorizontal = true;
                railPosIdx++;
            }
        }
        if (Input.GetKeyDown(KeyCode.W) && movingPlayerVertical && !movingPlayerHorizontal)
        {
            movingPlayerVertical = true;
            playerJumpVerticalPress = true;
        }
    }

    //Keeps applied horizontal velocity of jump for X seconds before stopping velocity and enabling player input again
    IEnumerator HorizontalJumpTimer()
    {
        Debug.Log("HorizontalJumpTimer");
        yield return new WaitForSeconds(.25f);
        rb.velocity = Vector2.zero;
        //movingPlayerVertical = false;
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
        movingPlayerVertical = false;
    }
}
