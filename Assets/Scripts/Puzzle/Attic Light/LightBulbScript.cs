using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class LightBulbScript : MonoBehaviour
{
    [SerializeField] private Light LightObject;
    [SerializeField] private int TurnCount;

    private bool AttachedToHolder;

    private Interactable InteractionScript;
    private Throwable ThrowableScript;
    private VelocityEstimator VelocityScript;
    private CircularDrive CircularDriveScript;

    private Rigidbody Rb;

    private bool CanRotate;

    private Vector3 OldRotation;

    private int RotationPoints;

    [HideInInspector] public bool HasPower;

    void Start()
    {
        InteractionScript = GetComponent<Interactable>();
        ThrowableScript = GetComponent<Throwable>();
        VelocityScript = GetComponent<VelocityEstimator>();
        CircularDriveScript = GetComponent<CircularDrive>();

        Rb = GetComponent<Rigidbody>();

        LightObject = GetComponentInChildren<Light>();
        LightObject.enabled = false;

        OldRotation = transform.eulerAngles;
    }

    void Update()
    {
        if (CanRotate)
        {
            Debug.Log(RotationPoints);

            if (RotationPoints < 0)
                RotationPoints = 0;
            if (RotationPoints > TurnCount)
                RotationPoints = TurnCount;

            RotationCheck();

            if(RotationPoints >= TurnCount)
            {
                if (CircularDriveScript != null)
                    Destroy(CircularDriveScript);

                if (LightObject.enabled == false && HasPower)
                    LightObject.enabled = true;
            }
        }
    }

    public void BecomeStatic()
    {
        Hand[] _hands = FindObjectOfType<Player>().GetComponentsInChildren<Hand>();

        foreach (Hand _hand in _hands)
        {
            if (_hand.ObjectIsAttached(gameObject))
            {
                _hand.DetachObject(gameObject);
                break;
            }
        }

        Destroy(ThrowableScript);
        Destroy(VelocityScript);

        Rb.isKinematic = true;

        CanRotate = true;
    }

    private void RotationCheck()
    {
        if(transform.eulerAngles.y > OldRotation.y)
        {
            RotationPoints -= 1;
            OldRotation = transform.eulerAngles;
        }
        if (transform.eulerAngles.y < OldRotation.y)
        {
            RotationPoints += 1;
            OldRotation = transform.eulerAngles;
        }
    }
}
