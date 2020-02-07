using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtticStickScript : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Lock_stairs")
        {
            Animator _lockAnimator = other.gameObject.GetComponentInParent<Animator>();

            if (_lockAnimator != null)
                _lockAnimator.SetBool("OpenHatch", true);
        }
    }
}
