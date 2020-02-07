using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class Backpack : MonoBehaviour
{
    #region References
    private BoxCollider Collider;
    private Rigidbody Rb;
    private Interactable Interactable;
    #endregion

    #region Variables
    private GameObject BackpackObject;
    private GameObject InventoryScreen;
    private Transform ParentTransform;
    private Vector3 RefVelocity;

    private bool ReadyToPutAway;
    private float DistToGround;
    #endregion
   
    private void Awake()
    {
        ParentTransform = transform.parent;
        BackpackObject = transform.GetChild(0).gameObject;
        InventoryScreen = transform.GetChild(1).gameObject;
        BackpackObject.SetActive(false);

        Collider = GetComponent<BoxCollider>();
        Rb = GetComponent<Rigidbody>();
        Interactable = GetComponent<Interactable>();

        DistToGround = Collider.bounds.extents.y;
    }

    private void Update()
    {
        if(Interactable.attachedToHand != null)
        {
            Vector3 target = Interactable.attachedToHand.transform.position;
            target -= new Vector3(0, 0.2f, 0);
            transform.position = Vector3.SmoothDamp(transform.position, target, ref RefVelocity, 0f);
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        if (IsGrounded() && transform.parent == null)
        {
            InventoryScreen.SetActive(true);
        }
        else if (InventoryScreen.activeSelf)
            InventoryScreen.SetActive(false);

        Vector3 dist = ParentTransform.transform.position - transform.position;
        if (dist.magnitude > 10)
            PutAwayPack(true);
    } 

    public void PutAwayPack(bool DoItAnyway = false)
    {
        float direction = AngleDir(Camera.main.transform.right, Camera.main.transform.position -transform.position, Camera.main.transform.up);
        if (direction < 0 || DoItAnyway)
        {
            Rb.isKinematic = true;
            Rb.useGravity = false;
            BackpackObject.SetActive(false);
            Collider.enabled = true;
            transform.SetParent(ParentTransform);
            ResetTransform();
        }
        else 
        {
            Rb.isKinematic = false;
            Rb.useGravity = true;
            Collider.enabled = true;
        }
    }

    public void GrabPack()
    {
        BackpackObject.SetActive(true);
        transform.SetParent(null);
        Collider.enabled = false;
    }

    float AngleDir(Vector3 fwd, Vector3 targetDir, Vector3 up)
    {
        Vector3 perp = Vector3.Cross(fwd, targetDir);
        float dir = Vector3.Dot(perp, up);

        if (dir > 0f)
        {
            return 1f;
        }
        else if (dir < 0f)
        {
            return -1f;
        }
        else
        {
            return 0f;
        }
    }

    private void ResetTransform()
    {
        transform.localPosition = new Vector3(0, 0, -.5f);
        transform.localEulerAngles = Vector3.zero;
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up * .2f, DistToGround + 0.1f);
    }
}
