using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class TwoHandAttach : MonoBehaviour
{
    #region References
    private Interactable interactable;
    private HandVelocity LeftAttachedHand, RightAttachedHand;

    private MeshCollider Collider;

    public GameObject LeftHandAttachPosition, RighthandAttachPosition, LeftOriginalAttachPos, RightOriginalAttachPos;
    #endregion

    private void Awake()
    {
        interactable = GetComponent<Interactable>();
        Collider = GetComponent<MeshCollider>();
        LeftOriginalAttachPos = new GameObject();
        RightOriginalAttachPos = new GameObject();
    }

    public void ClimbAttach()
    {
        if (interactable.attachedToHand.handType == Valve.VR.SteamVR_Input_Sources.LeftHand)
        {
            LeftAttachedHand = interactable.attachedToHand.GetComponent<HandVelocity>();
            if(LeftAttachedHand.GrabbedObject == null)
            {
                LeftAttachedHand.GrabbedObject = GetGrabPosition(LeftHandAttachPosition, LeftAttachedHand.gameObject).transform;
                LeftOriginalAttachPos.transform.position = LeftAttachedHand.GrabbedObject.transform.position;
            }
        }
            
        if (interactable.attachedToHand.handType == Valve.VR.SteamVR_Input_Sources.RightHand)
        {
            RightAttachedHand = interactable.attachedToHand.GetComponent<HandVelocity>();
            if (RightAttachedHand.GrabbedObject == null)
            {
                RightAttachedHand.GrabbedObject = GetGrabPosition(RighthandAttachPosition, RightAttachedHand.gameObject).transform;
                RightOriginalAttachPos.transform.position = RightAttachedHand.GrabbedObject.transform.position;

                

            }  
        }
            

        PlayerSoundManager.Instance.PlaySFX(PlayerSFX.Climb, interactable.attachedToHand.handType);
    }

    public void AttachTwoHand()
    {
        if (interactable.attachedToHand.handType == Valve.VR.SteamVR_Input_Sources.LeftHand)
        {
            LeftAttachedHand = interactable.attachedToHand.GetComponent<HandVelocity>();
            LeftHandAttachPosition = GetGrabPosition(LeftHandAttachPosition, LeftAttachedHand.gameObject);
            LeftHandAttachPosition.transform.rotation = Quaternion.Euler(0, -180, -90);
            LeftOriginalAttachPos.transform.position = LeftHandAttachPosition.transform.position;
            Debug.Log("Attached Left");
        }

        if (interactable.attachedToHand.handType == Valve.VR.SteamVR_Input_Sources.RightHand)
        {
            RightAttachedHand = interactable.attachedToHand.GetComponent<HandVelocity>();
            RighthandAttachPosition = GetGrabPosition(RighthandAttachPosition, RightAttachedHand.gameObject);
            RighthandAttachPosition.transform.rotation = Quaternion.Euler(0, 90, 90);
            RightOriginalAttachPos.transform.position = RighthandAttachPosition.transform.position;

            Debug.Log("Attached Right");


        }   
    }

    public void ClimbDetatch()
    {

        if(LeftAttachedHand == null && RightAttachedHand != null)
        {
            RightAttachedHand.GrabbedObject = null;
        }
        if (LeftAttachedHand != null && RightAttachedHand == null)
        {
            LeftAttachedHand.GrabbedObject = null;
        }
        if (LeftAttachedHand != null && RightAttachedHand != null)
        {
            if (!LeftAttachedHand.Grab.GetActive(LeftAttachedHand.hand.handType))
            {
                LeftAttachedHand.GrabbedObject = null;
            }
            if (!RightAttachedHand.Grab.GetActive(RightAttachedHand.hand.handType))
            {
                RightAttachedHand.GrabbedObject = null;
            }
        }

        /*

        if (AttachedHand != null)
        {
            AttachedHand.GrabbedObject = null;
            AttachedHand = null;
        }
        */
    }

    public void DetachTwoHand()
    {
        if (LeftAttachedHand == null && RightAttachedHand != null)
        {
            RenderModel model = RightAttachedHand.hand.mainRenderModel;
            model.SetHandPosition(Vector3.zero);
            model.SetHandRotation(Quaternion.Euler(0,0,0));
            RightAttachedHand = null;
            Debug.Log("DeAttached Right");
        }
        else if (LeftAttachedHand != null && RightAttachedHand == null)
        {
            RenderModel model = LeftAttachedHand.hand.mainRenderModel;
            model.SetHandPosition(Vector3.zero);
            model.SetHandRotation(Quaternion.Euler(0, 0, 0));
            LeftAttachedHand = null;
            Debug.Log("DeAttached Left");
        }
        else if (LeftAttachedHand != null && RightAttachedHand != null)
        {
            if(LeftAttachedHand.hand.IsGrabEnding(gameObject))
            {
                RenderModel model = LeftAttachedHand.hand.mainRenderModel;
                model.SetHandPosition(Vector3.zero);
                model.SetHandRotation(Quaternion.Euler(0, 0, 0));
                LeftAttachedHand = null;
            }
            if(RightAttachedHand.hand.IsGrabEnding(gameObject))
            {
                RenderModel model2 = RightAttachedHand.hand.mainRenderModel;
                model2.SetHandPosition(Vector3.zero);
                model2.SetHandRotation(Quaternion.Euler(0, 0, 0));
                RightAttachedHand = null;
            }


            Debug.Log("DeAttached bofa");
        }


    }

    private GameObject GetGrabPosition(GameObject obj, GameObject Hand)
    {
        Destroy(obj);
        obj = new GameObject();

        obj.transform.parent = transform;

        Vector3 ObjPosition = Collider.ClosestPoint(Hand.transform.position);
        obj.transform.position = ObjPosition;
        
        return obj;
    }
}
