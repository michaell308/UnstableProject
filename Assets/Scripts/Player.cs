using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private static Transform playerTransform;
    public static Rigidbody2D rb;
    public static int railNum = 2;
    public Transform leftPos;
    public Transform rightPos;

    //Movement vectors for each possible direction
    Vector2 playerJumpVerticalUpVelocity = new Vector2(0, 10);
    Vector2 playerJumpVerticalDownVelocity = new Vector2(0, -10);

    //Booleans for key input
    bool playerJumpVerticalPress = false;
    bool movingPlayerVertical = false;
    bool movingPlayerLeft = false;
    bool movingPlayerRight = false;
    Vector3 dir = Vector2.zero;

    List<Vector3> railPositions;
    public int railPosIdx = 1;

    private static bool playerIsDead = false;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = this.transform;
        rb = this.GetComponent<Rigidbody2D>();
        railPositions = new List<Vector3>();
        railPositions.Add(leftPos.position);
        railPositions.Add(transform.position);
        railPositions.Add(rightPos.position);
    }

    private void FixedUpdate()
    {
        if (!playerIsDead)
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

            if (movingPlayerLeft)
            {
                // var heading = transform.position - leftPos.position;
                //rb.velocity = -heading * 999;
                if (dir == Vector3.zero)
                {
                    dir = leftPos.position - transform.position;
                }
                rb.velocity = dir.normalized * 1000 * Time.deltaTime;
                //Apply left horizontal velocity
                //rb.velocity = playerJumpLeftVelocity;
                //StartCoroutine("HorizontalJumpTimer");
            }


            //Check if right is pressed
            if (movingPlayerRight)
            {
                if (dir == Vector3.zero)
                {
                    dir = rightPos.position - transform.position;
                }
                rb.velocity = dir.normalized * 1000 * Time.deltaTime;
                //Apply right horizontal velocity
                //rb.velocity = playerJumpRightVelocity;
                //StartCoroutine("HorizontalJumpTimer");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerIsDead)
        {
            /*if (movingPlayerHorizontal && transform.position != railPositions[railPosIdx])
            {
                transform.position = Vector3.MoveTowards(transform.position, railPositions[railPosIdx], Time.deltaTime * 1000); //can also be Lerp instead
            }
            if (transform.position == railPositions[railPosIdx])
            {
                movingPlayerHorizontal = false;
            }*/
            if (movingPlayerLeft)
            {
                if (transform.position.x < railPositions[railPosIdx].x || transform.position.y > railPositions[railPosIdx].y)
                {
                    dir = Vector3.zero;
                    movingPlayerLeft = false;
                    rb.velocity = Vector2.zero;
                    transform.position = railPositions[railPosIdx];
                }
            }
            if (movingPlayerRight)
            {
                if (transform.position.x > railPositions[railPosIdx].x || transform.position.y < railPositions[railPosIdx].y)
                {
                    dir = Vector3.zero;
                    movingPlayerRight = false;
                    rb.velocity = Vector2.zero;
                    transform.position = railPositions[railPosIdx];
                }
            }
            /*if (transform.position == railPositions[railPosIdx])
            {
                movingPlayerLeft = false;
            }*/
            bool movingPlayerHorizontal = movingPlayerLeft || movingPlayerRight;
            if (Input.GetKeyDown(KeyCode.Q) && !movingPlayerHorizontal && !movingPlayerVertical)
            {
                if (railPosIdx > 0)
                {
                    movingPlayerHorizontal = true;
                    movingPlayerLeft = true;
                    railPosIdx--;
                }

            }
            if (Input.GetKeyDown(KeyCode.E) && !movingPlayerHorizontal && !movingPlayerVertical)
            {
                if (railPosIdx < 2)
                {
                    movingPlayerHorizontal = true;
                    movingPlayerRight = true;
                    railPosIdx++;
                }
            }
            if (Input.GetKeyDown(KeyCode.W) && movingPlayerVertical && !movingPlayerHorizontal)
            {
                movingPlayerVertical = true;
                playerJumpVerticalPress = true;
            }
        }
    }

    //Keeps applied horizontal velocity of jump for X seconds before stopping velocity and enabling player input again
    IEnumerator HorizontalJumpTimer()
    {
        yield return new WaitForSeconds(.25f);
        rb.velocity = Vector2.zero;
        movingPlayerLeft = false;
        movingPlayerRight = false;
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

    public static void Death()
    {
        playerIsDead = true;
        Debug.Log("player death");
        rb.velocity = rb.velocity.normalized * 5;
        //rb.velocity = Vector2.zero;
        rb.gravityScale = 2;
        playerTransform.position = new Vector3(playerTransform.position.x, playerTransform.transform.position.y, 140);
    }
}
