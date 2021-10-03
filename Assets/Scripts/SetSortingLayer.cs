using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSortingLayer : MonoBehaviour
{

    //public string sortingLayerName;        // The name of the sorting layer .
    public int sortingOrder;            //The sorting order
    // Start is called before the first frame update
    void Start()
    {
        // Set the sorting layer and order.
        //GetComponent<Renderer>().sortingLayerName = sortingLayerName;
        GetComponent<Renderer>().sortingOrder = sortingOrder;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
