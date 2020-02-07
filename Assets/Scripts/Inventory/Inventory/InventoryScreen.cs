using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryScreen : MonoBehaviour
{
    private GameObject _Player;

    private void Awake()
    {
        _Player = transform.root.gameObject;
    }

    private void Update()
    {
        Quaternion lookRot = Quaternion.LookRotation(transform.position - _Player.transform.position, Vector3.up);
        Vector3 lookRotEuler = new Vector3(15, lookRot.y, 0);
        lookRot.eulerAngles = new Vector3(20, lookRot.eulerAngles.y, 0);
        transform.rotation = lookRot;
    }

}
