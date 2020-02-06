using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class DoorLock : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private GameObject KeyObject;
    [SerializeField] private GameObject LockPivot;
    [Space(10)]
    [SerializeField] private int TurnCount;
    [SerializeField] private int RotationPoints;

    [Header("Door")]
    [SerializeField] private GameObject DoorPivot;
    [SerializeField] private bool IsLocked;

    private BoxCollider DoorCollider;
    private Collider KeyCollider;

    private Rigidbody KeyRb;

    private bool KeyInserted;

    private Throwable ThrowableScript;
    private VelocityEstimator VelocityScript;
    private CircularDrive CircularDriveScript;

    private Vector3 OldKeyRotation;

    void Start()
    {
        if(KeyObject != null)
        {
            KeyRb = KeyObject.GetComponent<Rigidbody>();

            ThrowableScript = KeyObject.GetComponent<Throwable>();
            VelocityScript = KeyObject.GetComponent<VelocityEstimator>();
            CircularDriveScript = KeyObject.GetComponent<CircularDrive>();

            KeyCollider = KeyObject.GetComponent<Collider>();

            OldKeyRotation = KeyObject.transform.eulerAngles;
        }

        DoorCollider = DoorPivot.GetComponent<BoxCollider>();

        if (IsLocked)
            DoorCollider.enabled = false;
    }

    void Update()
    {
        //Only check rotation when the key is inserted & the door is locked.
        if (KeyInserted && IsLocked)
            RotationCheck();

        if (KeyInserted)
            KeyObject.transform.position = LockPivot.transform.position;

        if (!IsLocked && KeyObject != null)
            KeyObject.transform.eulerAngles = new Vector3(DoorPivot.transform.eulerAngles.x, DoorPivot.transform.eulerAngles.y - 90, DoorPivot.transform.eulerAngles.z);

        if (KeyInserted && IsLocked && RotationPoints >= TurnCount)
            UnlockDoor();

        if (RotationPoints <= 0)
            RotationPoints = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == KeyObject.name && !KeyInserted)
        {
            KeyRb.isKinematic = true;
            KeyObject.transform.eulerAngles = new Vector3(0, 90, 0);

            Destroy(ThrowableScript);
            Destroy(VelocityScript);

            //Let go of the object when it is inserted in the door.
            Hand[] _hands = FindObjectOfType<Player>().GetComponentsInChildren<Hand>();

            foreach (Hand _hand in _hands)
            {
                if (_hand.ObjectIsAttached(KeyObject))
                {
                    _hand.DetachObject(KeyObject);
                    break;
                }
            }

            KeyInserted = true;
        }
    }

    public void UnlockDoor()
    {
        Destroy(CircularDriveScript);

        KeyRb.isKinematic = false;

        KeyCollider.enabled = false;

        IsLocked = false;
        DoorCollider.enabled = true;
    }

    /// <summary>
    /// When KeyObject is rotated, add or deduct points until the goal is reached.
    /// </summary>
    private void RotationCheck()
    {
        if (KeyObject.transform.eulerAngles.x > OldKeyRotation.x)
        {
            RotationPoints += 1;
            OldKeyRotation = KeyObject.transform.eulerAngles;
        }
        if (KeyObject.transform.eulerAngles.x < OldKeyRotation.x)
        {
            RotationPoints -= 1;
            OldKeyRotation = KeyObject.transform.eulerAngles;
        }
    }
}
