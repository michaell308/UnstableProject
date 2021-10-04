using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        Debug.Log("ONTRIGGERENTER");
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
        if (collision.gameObject.layer == LayerMask.NameToLayer("RestartLevel"))
        {
            //Debug.Log("RESTART THE LEVEL");
            //Scene thisScene = SceneManager.GetActiveScene();
            //SceneManager.LoadScene(thisScene.name);
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("CompletedLevel"))
        {
            SceneManager.LoadScene("Level2");
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("CompletedGame"))
        {
            SceneManager.LoadScene("CreditsScene");
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
