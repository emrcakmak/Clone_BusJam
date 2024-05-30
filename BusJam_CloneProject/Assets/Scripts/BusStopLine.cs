using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusStopLine : MonoBehaviour
{
    public bool isOccupied = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (HasCharacter())
        {
            isOccupied = true;
        }
        else
        {
            isOccupied = false;
        }
    }


    public bool HasCharacter()
    {
        return transform.childCount > 0;
    }
}
