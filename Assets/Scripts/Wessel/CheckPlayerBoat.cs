using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPlayerBoat : MonoBehaviour
{ 
    public bool triggerPlayer;

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            triggerPlayer = true;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            triggerPlayer = false;
        }
    }
}
