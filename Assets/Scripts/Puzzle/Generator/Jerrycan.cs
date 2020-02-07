using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Jerrycan : MonoBehaviour
{
    [Header("Generator")]
    [SerializeField] private Generator GeneratorObject;

    [Header("Jerrycan Values")]
    [SerializeField] private float SecondsBeforeEmpty;

    private Rigidbody Rb;

    private bool IsEmpty;

    private int FillCount;

    void Start()
    {
        Rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.name == "Fuel_intake" && !IsEmpty)
        {
            if(transform.eulerAngles.z >= 100)
            {
                FillCount += 1;

                if(FillCount > 250)
                    GiveFuel();
            }
        }
    }

    private void GiveFuel()
    {
        IsEmpty = true;

        GeneratorObject.RecieveFuel();
    }
}
