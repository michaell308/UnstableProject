using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScarfAttachedTo : MonoBehaviour
{
    public Transform playerCharacterSprite;
    public Transform playerTransform;
    private Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = playerTransform.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player.movingPlayerUp || player.movingPlayerDown)
        {
            //transform.position = playerCharacterSprite.position;
        }
        transform.rotation = playerCharacterSprite.rotation;
    }
}
