using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidBodyMovement : MonoBehaviour
{
    //Rigid body
    Rigidbody2D rb;

    //Movement vectors for each possible direction
    Vector2 playerJumpVerticalUpVelocity = new Vector2(0, 10);
    Vector2 playerJumpVerticalDownVelocity = new Vector2(0, -10);
    Vector2 playerJumpLeftVelocity = new Vector2(-8, 2);
    Vector2 playerJumpRightVelocity = new Vector2(8, -2);
    Vector2 playerStopVelocity = new Vector2(0, 0);

    //Booleans for key input
    bool playerJumpVerticalPress = false;
    bool playerJumpHorizontalPress = false;
    bool playerInputEnabled = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        //Check if vertical button is pressed and make sure only going up is allowed
        if (playerJumpVerticalPress && Input.GetAxisRaw("Vertical") > 0)
        {
            //Set key press to false so that the function is not called multiple times
            playerJumpVerticalPress = false;

            //Apply upwards vertical velocity
            rb.velocity = playerJumpVerticalUpVelocity;
            StartCoroutine("VerticalJumpUpTimer");
            StartCoroutine("VerticalJumpDownTimer");
        }

        //Check if left is pressed
        if (playerJumpHorizontalPress && Input.GetAxisRaw("Horizontal") < 0)
        {
            //Set key press to false so that the function is not called multiple times
            playerJumpHorizontalPress = false;

            //Apply left horizontal velocity
            rb.velocity = playerJumpLeftVelocity;
            StartCoroutine("HorizontalJumpTimer");
        }

        //Check if right is pressed
        if(playerJumpHorizontalPress && Input.GetAxisRaw("Horizontal") > 0)
        {
            //Set key press to false so that the function is not called multiple times
            playerJumpHorizontalPress = false;

            //Apply right horizontal velocity
            rb.velocity = playerJumpRightVelocity;
            StartCoroutine("HorizontalJumpTimer");
        }
    }
    // Update is called once per frame
    //Checks for key presses and if player input is enabled
    void Update()
    {
        if (Input.GetButtonDown("Vertical") && playerInputEnabled)
        {
            playerInputEnabled = false;
            playerJumpVerticalPress = true;
        }
        if (Input.GetButtonDown("Horizontal") && playerInputEnabled)
        {
            playerInputEnabled = false;
            playerJumpHorizontalPress = true;
        }
    }

    //Keeps applied horizontal velocity of jump for X seconds before stopping velocity and enabling player input again
    IEnumerator HorizontalJumpTimer()
    {
        yield return new WaitForSeconds(.25f);
        rb.velocity = playerStopVelocity;
        
        playerInputEnabled = true;
    }

    //Keeps applied upwards velocity of jump for X seconds before stopping. Then applies downwards velocity
    IEnumerator VerticalJumpUpTimer()
    {
        yield return new WaitForSeconds(.1f);
        rb.velocity = playerStopVelocity;
        rb.velocity = playerJumpVerticalDownVelocity;
    }

    //Keeps applied downwards velocity of jump for X seconds before stopping and enabling player input again
    IEnumerator VerticalJumpDownTimer()
    {
        yield return new WaitForSeconds(.22f);
        rb.velocity = playerStopVelocity;
        playerInputEnabled = true;
    }
}
