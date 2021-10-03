using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Rigidbody2D playerWithBoardRB;
    public static int railNum = 2;
    // Start is called before the first frame update
    void Start()
    {
        playerWithBoardRB = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void Death()
    {
        playerWithBoardRB.gravityScale = 1;
    }
}
