using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightHolderScript : MonoBehaviour
{
    [SerializeField] private GameObject LightObject;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == LightObject)
        {
            other.GetComponent<LightBulbScript>().BecomeStatic();

            other.gameObject.transform.position = new Vector3(transform.position.x, transform.position.y - 0.1f, transform.position.z);
            other.gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }
}
