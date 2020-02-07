using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class MovementController : IState
{
    #region References
    private PlayerStatemachine Owner;
    #endregion

    #region Variables
    private SteamVR_Action_Boolean RotateLeft = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("RotateLeft");
    private SteamVR_Action_Boolean RotateRight = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("RotateRight");
    private Vector3 LeftStickInput;
    private Vector3 RefInputVel;
    #endregion

    public MovementController(PlayerStatemachine owner)
    {
        Owner = owner;
    }
    
    public void Enter()
    {
        Owner.Velocity = Vector3.zero;
        Owner.OldGravity = 0;
    }

    public void Execute()
    {
        Owner.MovementInput = Vector3.SmoothDamp(Owner.MovementInput, Vector3.zero, ref RefInputVel, 1);
        Owner.OldGravity = Owner.Velocity.y;
        Owner.Velocity = Camera.main.transform.TransformDirection(Owner.MovementInput);
        Owner.AddGravity(PlayerState.Walking);

        if (RotateRight.GetStateDown(SteamVR_Input_Sources.RightHand))
        {
            Owner.transform.eulerAngles += new Vector3(0, 30, 0);
        }
        if (RotateLeft.GetStateDown(SteamVR_Input_Sources.RightHand))
        {
            Owner.transform.eulerAngles -= new Vector3(0, 30, 0);
        }
    }

    public void Exit()
    {
        if(Owner.State == PlayerState.Swimming)
            Owner.SwimmingInput.y = Owner.Velocity.y / Owner.SwimmingSpeed;
    }

    public void FixedExecute()
    {
        Owner.CharController.Move((Owner.Velocity * Owner.MovementSpeed) * Time.fixedDeltaTime);
    }
}
