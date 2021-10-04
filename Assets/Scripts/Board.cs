using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    GameObject player = null;
    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
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
            if (player.GetComponent<Player>().railPosIdx == collision.GetComponentInParent<HazardGroup>().railNum)
            {
                Debug.Log("you died...");
                player.GetComponent<Player>().Death();
            }
        }
    }
}
