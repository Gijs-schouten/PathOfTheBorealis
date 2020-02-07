using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class SyncToWrist : MonoBehaviour
{

    private Hand Hand;
    private Vector3 Offset;
    private Quaternion OffsetRot;

    private Transform handGoal;

    private void Awake()
    {
        Hand = transform.parent.GetComponent<Hand>();
        Offset = transform.localPosition;
        OffsetRot = transform.localRotation;
    }

    private void Update()
    {
        if(Hand.mainRenderModel != null)
        {
            transform.position = Hand.mainRenderModel.GetBonePosition(1);
            transform.rotation = Hand.mainRenderModel.GetBoneRotation(1);
        }
        if(transform.childCount > 0)
        {
            if(handGoal == null)
                handGoal = transform.GetChild(0);
            else if(Hand.trackedObject.inputSource == Valve.VR.SteamVR_Input_Sources.LeftHand)
                handGoal.transform.localRotation = Quaternion.Euler(-90, 0, 180);
            else if(Hand.trackedObject.inputSource == Valve.VR.SteamVR_Input_Sources.RightHand)
                handGoal.transform.localRotation = Quaternion.Euler(90, 0, 0);
        }
        
    }
}
