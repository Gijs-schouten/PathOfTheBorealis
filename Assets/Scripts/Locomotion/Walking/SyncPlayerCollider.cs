using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncPlayerCollider : MonoBehaviour
{
    private CharacterController CharController;

    private void Awake()
    {
        if(CharController == null)
            CharController = transform.root.GetComponent<CharacterController>();

    }

    private void FixedUpdate()
    {
        CharController.center = new Vector3(transform.localPosition.x, transform.localPosition.y / 2, transform.localPosition.z);
        CharController.height = transform.localPosition.y;
    }

}
