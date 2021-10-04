using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private static Transform playerTransform;
    public static Rigidbody2D rb;
    //public static int railNum = 1;
    public Transform leftPos;
    public Transform rightPos;
    

    //Movement vectors for each possible direction
    Vector2 playerJumpVerticalUpVelocity;
    Vector2 playerJumpVerticalDownVelocity;

    //Booleans for key input
    bool movingPlayerLeft = false;
    bool movingPlayerRight = false;
    bool movingPlayerUp = false;
    bool movingPlayerDown = false;
    Vector3 dir = Vector2.zero;

    List<Vector2> railPositions;
    public static int railPosIdx = 1;
    List<Vector2> railPositionsUp;

    private static bool playerIsDead = false;

    //variables to tweak jump numbers
    //public float jumpTime = 0.2f;
    //public int jumpVelocity = 30;
    public int jumpHeight = 5;
    public int upJumpVelocity = 700;
    public int downJumpVelocity = 500;
    public int minUpVelocity = 100;
    public int minDownVelocity = 100;

    //References for object that contains flare animation
    public GameObject flare;
    public Animator flareAnim;

    //IEnumerator? Need to play animation and THEN  delete it.
    public Transform sparkPrefab;

    // Start is called before the first frame update
    void Start()
    {
        //playerJumpVerticalUpVelocity = new Vector2(0, jumpVelocity);
        //playerJumpVerticalDownVelocity = new Vector2(0, -jumpVelocity);
        playerTransform = this.transform;
        rb = this.GetComponent<Rigidbody2D>();
        railPositions = new List<Vector2>();
        railPositions.Add(leftPos.position);
        railPositions.Add(transform.position);
        railPositions.Add(rightPos.position);
        railPositionsUp = new List<Vector2>();
        railPositionsUp.Add(new Vector2(leftPos.position.x, leftPos.position.y + jumpHeight));
        railPositionsUp.Add(new Vector2(transform.position.x, transform.position.y + jumpHeight));
        railPositionsUp.Add(new Vector2(rightPos.position.x, rightPos.position.y + jumpHeight));

        
        //Play flare animation immediately
        flare = GameObject.FindWithTag("flare");
        flareAnim = flare.GetComponent<Animator>();
        enableFlare();
    }

    private void FixedUpdate()
    {
        if (!playerIsDead)
        {
            //Check if vertical button is pressed.
            if (movingPlayerUp)
            {
                if (dir == Vector3.zero)
                {
                    dir = railPositionsUp[railPosIdx] - new Vector2(transform.position.x, transform.position.y);
                }
                Vector3 dir2 = railPositionsUp[railPosIdx] - new Vector2(transform.position.x, transform.position.y);

                rb.velocity = Time.deltaTime * (new Vector3(0,minUpVelocity) + (dir2 * upJumpVelocity));
                //Debug.Log(dir2);
                if (dir2.y <= 0.02f)
                {
                    Debug.Log("stop moving player up damn you");
                    dir = Vector3.zero;

                    rb.velocity = Vector2.zero;
                    transform.position = railPositionsUp[railPosIdx];
                    movingPlayerDown = true;
                    movingPlayerUp = false;
                }
            }

            if (movingPlayerDown)
            {
                if (dir == Vector3.zero)
                {
                    dir = railPositions[railPosIdx] - new Vector2(transform.position.x, transform.position.y);
                }

                Vector3 dir2 = new Vector2(0, jumpHeight) + (railPositions[railPosIdx] - new Vector2(transform.position.x, transform.position.y));// - ;
                
                dir2 = -dir2;
                Debug.Log(dir2);
                rb.velocity = Time.deltaTime * (new Vector3(0, -minDownVelocity) + (dir2 * downJumpVelocity));
                //rb.velocity = Time.deltaTime * (new Vector3(0, -minDownVelocity) + (dir2 * downJumpVelocity));
                //dir2 = -dir2;
                if (dir2.y <= -jumpHeight)
                {
                    Debug.Log("stop going down");
                    dir = Vector3.zero;

                    rb.velocity = Vector2.zero;
                    transform.position = railPositions[railPosIdx];
                    movingPlayerDown = false;

                    //Resume flare animation
                    enableFlare();
                    Grinding.shouldTilt = true;
                }

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

                    enableFlare();
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

                    enableFlare();
                }
            }

            if (movingPlayerUp)
            {
                if (transform.position.y >= railPositionsUp[railPosIdx].y)
                {
                    /*dir = Vector3.zero;
                   
                    rb.velocity = Vector2.zero;
                    transform.position = railPositionsUp[railPosIdx];
                    movingPlayerDown = true;
                    movingPlayerUp = false;*/
                }
            }
            if (movingPlayerDown)
            {
                if (transform.position.y <= railPositions[railPosIdx].y)
                {
                   /* dir = Vector3.zero;

                    rb.velocity = Vector2.zero;
                    transform.position = railPositions[railPosIdx];
                    movingPlayerDown = false;*/
                }
            }
            /*if (transform.position == railPositions[railPosIdx])
            {
                movingPlayerLeft = false;
            }*/
            bool movingPlayerHorizontal = movingPlayerLeft || movingPlayerRight;
            bool movingPlayerVertical = movingPlayerUp || movingPlayerDown;

            if (Input.GetKeyDown(KeyCode.Q) && !movingPlayerHorizontal && !movingPlayerVertical)
            {
                if (railPosIdx > 0)
                {
                    //Stop flare animation
                    disableFlare();
                    createSpark();

                    movingPlayerHorizontal = true;
                    movingPlayerLeft = true;
                    railPosIdx--;
                }

            }
            if (Input.GetKeyDown(KeyCode.E) && !movingPlayerHorizontal && !movingPlayerVertical)
            {
                if (railPosIdx < 2)
                {
                    //Stop flare animation
                    disableFlare();
                    createSpark();

                    movingPlayerHorizontal = true;
                    movingPlayerRight = true;
                    railPosIdx++;
                }
            }
            if (Input.GetKeyDown(KeyCode.W) && !movingPlayerVertical && !movingPlayerHorizontal)
            {
                //Stop flare animation
                disableFlare();
                createSpark();

                movingPlayerUp = true;
                Grinding.shouldTilt = false;
            }
        }
    }

    //Keeps applied horizontal velocity of jump for X seconds before stopping velocity and enabling player input again
    /*IEnumerator HorizontalJumpTimer()
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
        //yield return new WaitForSeconds(jumpTime);
        rb.velocity = playerJumpVerticalDownVelocity;
        StartCoroutine("VerticalJumpDownTimer");
    }

    //Keeps applied downwards velocity of jump for X seconds before stopping and enabling player input again
    IEnumerator VerticalJumpDownTimer()
    {
        //yield return new WaitForSeconds(jumpTime + 0.02f);
        rb.velocity = Vector2.zero;
    }*/

    public static void Death()
    {
        playerIsDead = true;
        Debug.Log("player death");
        rb.velocity = rb.velocity.normalized * 5;
        //rb.velocity = Vector2.zero;
        rb.gravityScale = 2;
        playerTransform.position = new Vector3(playerTransform.position.x, playerTransform.transform.position.y, 140);
        Grinding.shouldTilt = false;
    }

    private void disableFlare()
    {
        flareAnim.enabled = false;
        flare.GetComponent<SpriteRenderer>().material.color = new Color(1f, 1f, 1f, 0f);
    }

    private void enableFlare()
    {
        flareAnim.enabled = true;
        flare.GetComponent<SpriteRenderer>().material.color = new Color(1f, 1f, 1f, 1f);
        flareAnim.Play("Flare");
    }
    private void createSpark()
    {
        Transform spark = Instantiate(sparkPrefab, playerTransform.position - new Vector3(4.29f, 4.2f, 0), Quaternion.identity);
        Destroy(spark.gameObject, 0.5f);
    }
}
