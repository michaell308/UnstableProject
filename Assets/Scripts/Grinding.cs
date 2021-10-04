using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grinding : MonoBehaviour
{
    public float playerTiltSpeed = 1;
    public float autoTiltSpeed = 20;
    private Vector3 originalRotation;

    public static bool shouldTilt = true;

    GameObject player = null;

    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
        }
        originalRotation = transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldTilt)
        {
            if (Input.GetKey(KeyCode.D))
            {
                Quaternion rotation = transform.rotation;
                rotation *= Quaternion.Euler(0, 0, -90); // this adds a 90 degrees Z rotation
                //var adjustRotation = transform.rotation.y + rotationAdjust; //<- this is wrong!
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * playerTiltSpeed);
            }
            else if (Input.GetKey(KeyCode.A))
            {
                Quaternion rotation = transform.rotation;
                rotation *= Quaternion.Euler(0, 0, 90); // this adds a 90 degrees Z rotation
                //var adjustRotation = transform.rotation.y + rotationAdjust; //<- this is wrong!
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * playerTiltSpeed);
            }

            float currentAngle = transform.localRotation.eulerAngles.z;
            Debug.Log(currentAngle);
            if ((currentAngle > 292 || Mathf.Approximately(currentAngle,292)) && (currentAngle < 342 || Mathf.Approximately(currentAngle,342)))
            {
                Debug.Log("right");
                transform.Rotate(0.0f, 0.0f, -autoTiltSpeed * Time.deltaTime); //this tilts us to the right
            }
            else if (((currentAngle > 342 || Mathf.Approximately(currentAngle,342)) && (currentAngle < 360 || Mathf.Approximately(currentAngle,360))) ||
                ((currentAngle > 0 || Mathf.Approximately(currentAngle, 0)) && (currentAngle <= 32 || Mathf.Approximately(currentAngle,32))) 
                || currentAngle < 0)
            {
                Debug.Log("left");
                transform.Rotate(0.0f, 0.0f, autoTiltSpeed * Time.deltaTime);
            }

            float moveInput = Input.GetAxis("Horizontal");
            //transform.Rotate(0.0f, 0.0f,  (0.3f + -moveInput) * speed * Time.deltaTime);
            //transform.rotation = transform.rotation + Quaternion.Euler(0,0,1);
            //Debug.Log(transform.localRotation.eulerAngles.z);
            if (currentAngle >= 32 && currentAngle <= 292)
            {
                Debug.Log("DEATHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHH");
                player.GetComponent<Player>().Death();
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
}
