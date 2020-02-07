using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class PinScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "PinHolder")
        {
            ViseScript _viseScript = other.gameObject.GetComponentInParent<ViseScript>();

            if(_viseScript != null)
            {
                //Haal object uit hand als hij verbonden is met de target
                Hand[] _hands = FindObjectOfType<Player>().GetComponentsInChildren<Hand>();

                foreach (Hand _hand in _hands)
                {
                    if (_hand.ObjectIsAttached(gameObject))
                    {
                        _hand.DetachObject(gameObject);
                        break;
                    }
                }

                _viseScript.AttachPin();

                gameObject.SetActive(false);
            }
        }
    }
}
