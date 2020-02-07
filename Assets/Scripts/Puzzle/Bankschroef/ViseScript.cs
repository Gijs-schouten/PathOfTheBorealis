using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class ViseScript : MonoBehaviour
{
    [Header("Vise Parts")]
    [SerializeField] private GameObject MoveablePart;
    [SerializeField] private GameObject PinHolder;
    [SerializeField] private PinScript PickupPin;
    [SerializeField] private GameObject RotationPin;
    [Space]
    [SerializeField] private GameObject StuckObject;

    [Header("Moveable part Values")]
    [SerializeField] private float MinPos;
    [SerializeField] private float MaxPos;

    [HideInInspector] public bool PinConnected;

    private Vector3 OldPinRotation;


    private void Awake()
    {
        StuckObject.GetComponent<Rigidbody>().isKinematic = true;
        StuckObject.GetComponent<Collider>().enabled = false;

        //RotationPin.gameObject.SetActive(false);
    }
    void Update()
    {
        //Make sure the object drops after reaching a certain angle
        //to prevent the object getting stuck
        if (PinConnected)
        {
            if (RotationPin.transform.eulerAngles.z > 88)
            {
                ReleaseStuckObject();
            }
        }
    }

    private void FixedUpdate()
    {
        CheckRotation();
    }

    private void CheckRotation()
    {
        if (PinConnected)
        {
            if (RotationPin.transform.eulerAngles.z > OldPinRotation.z)
            {
                MoveablePart.transform.Translate(Vector3.forward * 0.08f * Time.fixedDeltaTime);
                OldPinRotation = RotationPin.transform.eulerAngles;
            }
            if (RotationPin.transform.eulerAngles.z < OldPinRotation.z)
            {
                MoveablePart.transform.Translate(Vector3.back * 0.08f * Time.fixedDeltaTime);
                OldPinRotation = RotationPin.transform.eulerAngles;
            }
        }
    }

    public void AttachPin()
    {
        //RotationPin.transform.position = PinHolder.transform.position;
        RotationPin.transform.position = new Vector3(2.130911f, 0.2412276f, 0.433f);

        RotationPin.transform.eulerAngles = new Vector3(1, 90, 0);
        OldPinRotation = RotationPin.transform.eulerAngles;

        RotationPin.gameObject.SetActive(true);

        PinConnected = true;
    }

    public void ReleaseStuckObject()
    {
        StuckObject.GetComponent<Rigidbody>().isKinematic = false;
        StuckObject.GetComponent<Collider>().enabled = true;
    }
}
