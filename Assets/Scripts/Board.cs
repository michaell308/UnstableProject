using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public bool onHazard = false;
    Player player = null;
    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindWithTag("Player").GetComponent<Player>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Hazard"))
        {
            onHazard = true;

            bool playerIsJumping = player.movingPlayerUp || player.movingPlayerDown;
            if (player.railPosIdx == collision.GetComponentInParent<HazardGroup>().railNum && !playerIsJumping)
            {
                Debug.Log("you died...");
                player.Death(10);
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Hazard"))
        {
            onHazard = true;

            bool playerIsJumping = player.movingPlayerUp || player.movingPlayerDown;
            if (player.railPosIdx == collision.GetComponentInParent<HazardGroup>().railNum && !playerIsJumping)
            {
                //Debug.Log("you died...");
                player.Death(10);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Hazard"))
        {
            onHazard = false;
        }
    }
}
