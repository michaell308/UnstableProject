using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grinding : MonoBehaviour
{
    private float playerTiltSpeed = 1;
    private float autoTiltSpeed = 20;
    private Vector3 originalRotation;
    // Start is called before the first frame update
    void Start()
    {
        originalRotation = transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            //var lookPos = target.position - transform.position;
            //lookPos.y = 0;
            //var rotation = Quaternion.LookRotation(lookPos);
            Quaternion rotation = transform.rotation;
            rotation *= Quaternion.Euler(0, 0, -90); // this adds a 90 degrees Z rotation
            //var adjustRotation = transform.rotation.y + rotationAdjust; //<- this is wrong!
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * playerTiltSpeed);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            //var lookPos = target.position - transform.position;
            //lookPos.y = 0;
            //var rotation = Quaternion.LookRotation(lookPos);
            Quaternion rotation = transform.rotation;
            rotation *= Quaternion.Euler(0, 0, 90); // this adds a 90 degrees Z rotation
            //var adjustRotation = transform.rotation.y + rotationAdjust; //<- this is wrong!
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 1);
        }
        if (transform.localRotation.eulerAngles.z >= 320 && transform.localRotation.eulerAngles.z <= 350)
        {
            transform.Rotate(0.0f, 0.0f, -autoTiltSpeed * Time.deltaTime);
        }
        else if ((transform.localRotation.eulerAngles.z >= 350 && transform.localRotation.eulerAngles.z <= 360) ||
            (transform.localRotation.eulerAngles.z >= 0 && transform.localRotation.eulerAngles.z <= 20))
        {
            transform.Rotate(0.0f, 0.0f, autoTiltSpeed * Time.deltaTime);
        }

        float moveInput = Input.GetAxis("Horizontal");
        //transform.Rotate(0.0f, 0.0f,  (0.3f + -moveInput) * speed * Time.deltaTime);
        //transform.rotation = transform.rotation + Quaternion.Euler(0,0,1);
        //Debug.Log(transform.localRotation.eulerAngles.z);
        if (transform.localRotation.eulerAngles.z >= 20 && transform.localRotation.eulerAngles.z <= 320)
        {
            Debug.Log("DEATHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHH");
        }
        /*if (transform.localRotation.eulerAngles.z <= 320) {
            Debug.Log("death");
            Player.Death();
            //transform.eulerAngles = new Vector3(originalRotation.x, originalRotation.y, 13);
        }
        else if (transform.localRotation.eulerAngles.z >= 380)
        {
            Debug.Log("death2");
            Player.Death();
            //transform.eulerAngles = new Vector3(originalRotation.x, originalRotation.y, -45);
        }*/
    }
}
