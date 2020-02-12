using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using Valve.VR;

public class TwoHandInteractable : Throwable
{

    private HandVelocity LeftAttachedHand, RightAttachedHand;
    private TwoHandAttach EventHandler;
    private MeshCollider MeshColl;

    private Vector3 minPoint = new Vector3(Mathf.Infinity, Mathf.Infinity, Mathf.Infinity), maxPoint = new Vector3(-Mathf.Infinity, -Mathf.Infinity, -Mathf.Infinity);
    private Quaternion oldRotation;

    private GameObject LeftHandPos, RightHandPos;
    private float LeftHandDist, RightHandDist;
    private SteamVR_Skeleton_Poser poser;

    private Vector3 InitialHandPos1, InitialHandPos2;
    private Quaternion InitialRotation;
    private GameObject Pivot;
    private GameObject Parent;

    private void Start()
    {
        MeshColl = GetComponent<MeshCollider>();
        LeftHandPos = new GameObject(); LeftHandPos.transform.parent = transform;
        RightHandPos = new GameObject(); RightHandPos.transform.parent = transform;
        poser = GetComponent<SteamVR_Skeleton_Poser>();
        EventHandler = GetComponent<TwoHandAttach>();
        InitialRotation = transform.rotation;
        Parent = transform.parent.gameObject;
    }

    protected override void HandHoverUpdate(Hand hand)
    {

        GrabTypes startingGrabType = hand.GetGrabStarting();
        if (hand.handType == Valve.VR.SteamVR_Input_Sources.LeftHand)
        {
            if(LeftAttachedHand == null)
                LeftAttachedHand = hand.GetComponent<HandVelocity>();

            if (startingGrabType != GrabTypes.None)
            {
                PlayerSoundManager.Instance.PlaySFX(PlayerSFX.Climb, hand.handType);
                hand.AttachObject(gameObject, startingGrabType, attachmentFlags, attachmentOffset);

                Vector3 dist = hand.transform.position - MeshColl.bounds.center;
                LeftHandDist = dist.magnitude;

                if (transform.TransformPoint(dist).y < MeshColl.bounds.center.y)
                    CalculateRotation(Vector3.down);
                else
                    CalculateRotation(Vector3.up);

                //LeftHandPos.transform.rotation = transform.rotation * hand.transform.rotation;
                //LeftAttachedHand.GrabbedObject = LeftHandPos.transform;

            }

        }
        if (hand.handType == Valve.VR.SteamVR_Input_Sources.RightHand)
        {
            if (RightAttachedHand == null)
                RightAttachedHand = hand.GetComponent<HandVelocity>();

            if (startingGrabType != GrabTypes.None)
            {
                PlayerSoundManager.Instance.PlaySFX(PlayerSFX.Climb, hand.handType);
                hand.AttachObject(gameObject, startingGrabType, attachmentFlags, attachmentOffset);

                Vector3 dist = hand.transform.position - MeshColl.bounds.center;
                RightHandDist = dist.magnitude;

                if(transform.TransformPoint(dist).y < MeshColl.bounds.center.y)
                    CalculateRotation(Vector3.down);
                else
                    CalculateRotation(Vector3.up);

                //RightHandPos.transform.rotation = RightHandPos.transform.rotation;
                //RightAttachedHand.GrabbedObject = RightHandPos.transform;

            } 
        }
    }

    protected override void HandAttachedUpdate(Hand hand)
    {
        if (hand.handType == Valve.VR.SteamVR_Input_Sources.LeftHand)
        {
            RenderModel model = hand.mainRenderModel;
            model.SetHandPosition(EventHandler.LeftHandAttachPosition.transform.position);
            model.SetHandRotation(EventHandler.LeftHandAttachPosition.transform.rotation);

            if (hand.IsGrabEnding(gameObject))
            {
                LeftAttachedHand.GrabbedObject = null;
                hand.DetachObject(gameObject, restoreOriginalParent);
            }



        }
        if (hand.handType == Valve.VR.SteamVR_Input_Sources.RightHand)
        {
            RenderModel model = hand.mainRenderModel;
            model.SetHandPosition(EventHandler.RighthandAttachPosition.transform.position);
            model.SetHandRotation(EventHandler.RighthandAttachPosition.transform.rotation);

            if (hand.IsGrabEnding(gameObject))
            {
                RightAttachedHand.GrabbedObject = null;
                hand.DetachObject(gameObject, restoreOriginalParent);
            }
        }

        if (hand.currentAttachedObject == gameObject && hand.otherHand.currentAttachedObject == gameObject)
        {

            InitialHandPos1 = EventHandler.LeftOriginalAttachPos.transform.position;
            InitialHandPos2 = EventHandler.RightOriginalAttachPos.transform.position;

            Vector3 currentHandPosition1 = LeftAttachedHand.transform.position; // current first hand position
            Vector3 currentHandPosition2 = RightAttachedHand.transform.position; // current second hand position

            Vector3 handDir1 = (InitialHandPos1 - InitialHandPos2).normalized; // direction vector of initial first and second hand position
            Vector3 handDir2 = (currentHandPosition1 - currentHandPosition2).normalized; // direction vector of current first and second hand position 

            Quaternion handRot = Quaternion.FromToRotation(handDir1, handDir2); // calculate rotation based on those two direction vectors

            float currentGrabDistance = Vector3.Distance(currentHandPosition1, currentHandPosition2);
            float initialGrabDistance = Vector3.Distance(InitialHandPos1, InitialHandPos2);
            float p = (currentGrabDistance / initialGrabDistance); // percentage based on the distance of the initial positions and the new positions

            if(Pivot == null)
            {
                Pivot = new GameObject();
                Pivot.transform.position = (InitialHandPos1 + InitialHandPos2) / 2;
                transform.parent = Pivot.transform;
                InitialRotation = Pivot.transform.rotation;
            }

            //Vector3 newScale = new Vector3(p * initialObjectScale.x, p * initialObjectScale.y, p * initialObjectScale.z); // calculate new object scale with p
            Pivot.transform.rotation = InitialRotation * handRot; // add rotation
            Pivot.transform.position = (currentHandPosition1 + currentHandPosition2) / 2;
            
            
                                                                                 //attachedObject.transform.localScale = newScale; // set new scale
                                                                                 // set the position of the object to the center of both hands based on the original object direction relative to the new scale and rotation
        }
        else
        {
            if(Pivot != null)
            {
                transform.parent = Parent.transform.parent;
                Destroy(Pivot.gameObject);
            }
        }

        if (onHeldUpdate != null)
            onHeldUpdate.Invoke(hand);
    }

    private void CalculateRotation(Vector3 direction)
    {
        oldRotation = transform.rotation;
        transform.rotation = Quaternion.identity;


        if(LeftAttachedHand != null)
        {
            LeftHandPos.transform.position = (MeshColl.bounds.center + (direction * LeftHandDist)) + poser.skeletonMainPose.leftHand.bonePositions[1];
            float leftAngle = Quaternion.Angle(transform.rotation, LeftAttachedHand.transform.rotation);
            LeftHandPos.transform.rotation = LeftAttachedHand.transform.rotation;//poser.skeletonMainPose.leftHand.boneRotations[1];
        }
        if(RightAttachedHand != null)
        {
            RightHandPos.transform.position = MeshColl.bounds.center + (direction * RightHandDist);
            float rightAngle = Quaternion.Angle(transform.rotation, RightAttachedHand.transform.rotation);
            RightHandPos.transform.rotation = RightAttachedHand.transform.rotation;// Quaternion.FromToRotation(-RightAttachedHand.transform.up, direction);
        }
        
        transform.rotation = oldRotation;
    }
}
