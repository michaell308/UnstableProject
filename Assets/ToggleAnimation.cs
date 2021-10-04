using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleAnimation : MonoBehaviour
{
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            anim.enabled = true;
            GetComponent<SpriteRenderer>().material.color = new Color(1f, 1f, 1f, 1f);
            anim.Play("Flare");
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            anim.enabled = false;
            GetComponent<SpriteRenderer>().material.color = new Color(1f, 1f, 1f, 0f);
        }
    }
}
