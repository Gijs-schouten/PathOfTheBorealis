using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class KayakState : IState
{ 
    private PlayerStatemachine Owner;

    private SteamVR_Action_Boolean JoystickPress = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("JoystickPress");

    public KayakState(PlayerStatemachine owner)
    {
        Owner = owner;
    }

    public void Enter()
    {
        Owner.transform.parent = Owner.PaddleManager.transform;

        Owner.CharController.enabled = false;
    }

    public void Execute()
    {

        Vector3 offset = Vector3.zero; offset.y = .35f;
        Owner.transform.position = Owner.PaddleManager.PlayerSeat.transform.position - offset;
        Owner.transform.rotation = Owner.PaddleManager.PlayerSeat.transform.rotation;

        if (JoystickPress.GetStateDown(Owner.RightHand.trackedObject.inputSource))
            Owner.State = PlayerState.Walking;
    }

    public void Exit()
    {
        Owner.PaddleManager.ExitBoat();

        Owner.transform.parent = null;
        Owner.CharController.enabled = true;

        Vector3 controllerPos = Owner.CharController.center; controllerPos.y = 0;

        Owner.transform.position = Owner.PaddleManager.PlayerExit.transform.position - controllerPos;
        Owner.transform.rotation = Owner.PaddleManager.PlayerExit.transform.rotation;
    }

    public void FixedExecute()
    {

    }
}
