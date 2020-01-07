﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rayCastTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 1f))
        {
            Debug.Log(hit.transform.name);
        }
    }
}
