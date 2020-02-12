using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPlayerShark : MonoBehaviour
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
