using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachedTo : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public bool shouldPingPong;
    public Vector3 pingPongOffset1;
    public Vector3 pingPongOffset2;
    // Start is called before the first frame update
    void Start()
    {
        if (shouldPingPong)
        {
            StartCoroutine("moveScarfDown");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldPingPong)
        {
            //float y = Mathf.PingPong(Time.time * 2, 1) * 0.2f - 0.1f;
            //transform.position = target.position + offset + new Vector3(0, y, 0);
        }
        else
        {
            //transform.position = target.position + offset;
        }
        transform.position = target.position + offset;

        transform.rotation = target.rotation;
    }

    IEnumerator moveScarfDown()
    {
        yield return new WaitForSeconds(.1f);
        transform.position = target.position + pingPongOffset1;
        yield return new WaitForSeconds(.25f);
        transform.position = target.position + pingPongOffset1;
        StartCoroutine("moveScarfUp");
    }

    IEnumerator moveScarfUp()
    {
        yield return new WaitForSeconds(.25f);
        transform.position = target.position - pingPongOffset1;
        yield return new WaitForSeconds(.25f);
        transform.position = target.position - pingPongOffset1;
        StartCoroutine("moveScarfDown");

    }
}
