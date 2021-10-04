using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndcapPlacement : MonoBehaviour
{
    public bool startEndcap;
    public bool stopEndcap;

    public Transform startEndcapPrefab;
    public Transform stopEndcapPrefab;
    // Start is called before the first frame update
    void Start()
    {
        if (startEndcap)
        {
            if (GetComponent<SpriteRenderer>().sprite.name == "rail large")
            {
                Vector3 endcapPosition = new Vector3(transform.position.x - 8.602f, transform.position.y - 8.579f, 0);
                Transform endcap = Instantiate(startEndcapPrefab, endcapPosition, Quaternion.Euler(0, 0, 45));
                endcap.SetParent(GameObject.FindWithTag("World").transform);
            }
            if (GetComponent<SpriteRenderer>().sprite.name == "rail medium")
            {
                Vector3 endcapPosition = new Vector3(transform.position.x - 8.602f, transform.position.y - 8.579f, 0);
                Transform endcap = Instantiate(startEndcapPrefab, endcapPosition, Quaternion.Euler(0, 0, 45));
                endcap.SetParent(GameObject.FindWithTag("World").transform);
            }
        }

        if (stopEndcap)
        {
            if (GetComponent<SpriteRenderer>().sprite.name == "rail large")
            {
                Vector3 endcapPosition = new Vector3(transform.position.x + 8.602f, transform.position.y + 8.579f, 0);
                Transform endcap = Instantiate(stopEndcapPrefab, endcapPosition, Quaternion.Euler(0, 0, 45));
                endcap.SetParent(GameObject.FindWithTag("World").transform);
            }
            if (GetComponent<SpriteRenderer>().sprite.name == "rail medium")
            {
                Vector3 endcapPosition = new Vector3(transform.position.x - 8.602f, transform.position.y - 8.579f, 0);
                Transform endcap = Instantiate(stopEndcapPrefab, endcapPosition, Quaternion.Euler(0, 0, 45));
                endcap.SetParent(GameObject.FindWithTag("World").transform);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
