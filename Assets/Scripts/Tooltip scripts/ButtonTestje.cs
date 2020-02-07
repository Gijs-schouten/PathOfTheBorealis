using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class ButtonTestje : MonoBehaviour
{
    [SerializeField] private EVRButtonId ButtonToHighlight;
    [SerializeField] private ISteamVR_Action_In_Source ActionButton;

    public void HighlightButton(Hand _hand)
    {
        //ControllerButtonHints.ShowButtonHint(_hand, EVRButtonId.k_EButton_SteamVR_Trigger);
    }
}