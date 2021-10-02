using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rail : MonoBehaviour
{
    private float speed = 500;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Vector3 moveVector = new Vector3(45, 45, 0);
        rb.AddForce(-transform.right * speed * Time.deltaTime, ForceMode2D.Impulse);
        //rb.AddForce(-moveVector * speed * Time.deltaTime, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
