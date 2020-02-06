using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwimmingState : IState
{
    private PlayerStatemachine Owner;

    private Vector3 RefVector;
    private float SpeedDamp;
     
    public SwimmingState(PlayerStatemachine owner)
    {
        Owner = owner;
    }

    public void Enter()
    {
        Owner.CameraPostProcessing.EnterWater();
        Owner.SwimmingInput.y = Owner.Velocity.y;
    }

    public void Execute()
    {
        Owner.CheckWater();
        HandVelocity handVelocityL = Owner.LeftHandVelocity;
        HandVelocity handVelocityR = Owner.RightHandVelocity;

        if (handVelocityL.Swimming && handVelocityR.Swimming)
        {
            Vector3 swimmingInput = handVelocityL.SwimVelocity + handVelocityR.SwimVelocity;
            swimmingInput = Owner.CharController.transform.TransformVector(swimmingInput);
            Owner.SwimmingInput += swimmingInput;
            SpeedDamp = .75f;
        }
        else if (handVelocityL.Swimming && !handVelocityR.Swimming)
        {
            Owner.SwimmingInput += Owner.CharController.transform.TransformVector(handVelocityL.SwimVelocity);
            SpeedDamp = 1.5f;
        }
            
        else if (!handVelocityL.Swimming && handVelocityR.Swimming)
        {
            Owner.SwimmingInput += Owner.CharController.transform.TransformVector(handVelocityR.SwimVelocity);
            SpeedDamp = 1.5f;
        }
        else
        {
            SpeedDamp = 3f;
        }

        Owner.SwimmingInput = Vector3.SmoothDamp(Owner.SwimmingInput, Vector3.zero, ref RefVector, SpeedDamp);
        
    }

    public void Exit()
    {
        Owner.CameraPostProcessing.ExitWater();
        Owner.Climber.Checking = true;
    }

    public void FixedExecute()
    {
        Owner.CharController.Move(Owner.SwimmingInput * Owner.SwimmingSpeed * Time.fixedDeltaTime);
    }
}
