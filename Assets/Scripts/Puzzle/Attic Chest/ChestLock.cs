using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class ChestLock : MonoBehaviour
{
    [SerializeField] private bool IsLocked;
    [SerializeField] private GameObject KeyObject;
    [Space(10)]
    [SerializeField] private AtticChest AtticChestScript;

    private BoxCollider LockCollider;
    private Rigidbody KeyRb;

    private bool KeyInserted;

    private Throwable KeyThrowScript;
    private VelocityEstimator KeyVelocityScript;


    void Start()
    {
        KeyRb = KeyObject.GetComponent<Rigidbody>();
        LockCollider = GetComponent<BoxCollider>();

        KeyThrowScript = KeyObject.GetComponent<Throwable>();
        KeyVelocityScript = KeyObject.GetComponent<VelocityEstimator>();

        AtticChestScript = GetComponentInParent<AtticChest>();
    }

    void Update()
    {
        if (KeyInserted)
        {
            KeyObject.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.1f);
            KeyObject.transform.eulerAngles = new Vector3(KeyObject.transform.eulerAngles.x, 90, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!KeyInserted && IsLocked)
        {
            if(other.gameObject == KeyObject)
            {
                KeyRb.isKinematic = true;

                Destroy(KeyThrowScript);
                Destroy(KeyVelocityScript);

                Hand[] _hands = FindObjectOfType<Player>().GetComponentsInChildren<Hand>();

                foreach (Hand _hand in _hands)
                {
                    if (_hand.ObjectIsAttached(KeyObject))
                    {
                        _hand.DetachObject(KeyObject);
                        break;
                    }
                }

                other.isTrigger = true;
                KeyInserted = true;

                MakeChestInteractable();
            }
        }
    }

    public void MakeChestInteractable()
    {
        AtticChestScript.UnlockChest();
        Debug.Log("De kist kan geopend worden.");
    }
}
