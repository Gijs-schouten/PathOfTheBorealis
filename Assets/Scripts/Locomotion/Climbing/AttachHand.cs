using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class AttachHand : MonoBehaviour
{
    #region References
    private Interactable interactable;
    private HandVelocity AttachedHand;
    public Transform LeftHandAttachPosition;
    public Transform RighthandAttachPosition;
    #endregion

    private void Awake()
    {
        interactable = GetComponent<Interactable>();
    }

    public void ClimbAttach()
    {
        AttachedHand = interactable.attachedToHand.GetComponent<HandVelocity>();
        if(interactable.attachedToHand.handType == Valve.VR.SteamVR_Input_Sources.LeftHand)
            AttachedHand.GrabbedObject = LeftHandAttachPosition;
        if (interactable.attachedToHand.handType == Valve.VR.SteamVR_Input_Sources.RightHand)
            AttachedHand.GrabbedObject = RighthandAttachPosition;

        PlayerSoundManager.Instance.PlaySFX(PlayerSFX.Climb, interactable.attachedToHand.handType);
    }

    public void ClimbDetatch()
    {
        if(AttachedHand != null)
        {
            AttachedHand.GrabbedObject = null;
            AttachedHand = null;
        }
    }
}
