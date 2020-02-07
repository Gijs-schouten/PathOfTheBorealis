using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;



public class SwimmingState : IState
{
    private PlayerStatemachine Owner;

    private Vector3 RefVector;
    private float SpeedDamp;

    private SteamVR_Action_Boolean RotateLeft = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("RotateLeft");
    private SteamVR_Action_Boolean RotateRight = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("RotateRight");
    private SteamVR_Action_Boolean DiveInput = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("InteractThumb");

    private HandVelocity HandVelocityL;
    private HandVelocity HandVelocityR;



    public SwimmingState(PlayerStatemachine owner)
    {
        Owner = owner;
    }

    public void Enter()
    {
        Owner.CameraPostProcessing.EnterWater();
        Owner.SwimmingInput.y = Owner.Velocity.y;
        HandVelocityL = Owner.LeftHandVelocity;
        HandVelocityR = Owner.RightHandVelocity;
    }

    public void Execute()
    {
        switch (Owner._WaterState)
        {
            case WaterState.Hovering:

                AboveWaterSwimming();

                if (DiveInput.GetStateDown(SteamVR_Input_Sources.Any))
                {
                    Owner.SwimmingInput = Vector3.zero;
                    Owner.StartCoroutine(Owner.Dive(new Vector3(0, 1, 0)));
                }

                break;
            case WaterState.Underwater:

                EnableSwimming();

                if (DiveInput.GetStateDown(SteamVR_Input_Sources.Any))
                {
                    Owner.SwimmingInput = Vector3.zero;
                    Vector3 dist = new Vector3(0, Owner.WaterPlane.transform.position.y - (Owner.transform.position.y + (Owner.CharController.center.y * 1.2f)), 0);
                    Owner.StartCoroutine(Owner.Dive(-dist));
                }

                break;
            case WaterState.AboveWater:

                break;
        }

        Owner.CheckWater();

        Owner.Climber.Checking = true;
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

    private void EnableSwimming()
    {
        if (HandVelocityL.Swimming && HandVelocityR.Swimming)
        {
            Vector3 swimmingInput = HandVelocityL.SwimVelocity + HandVelocityR.SwimVelocity;
            swimmingInput = Owner.CharController.transform.TransformVector(swimmingInput);
            Owner.SwimmingInput += swimmingInput;
            SpeedDamp = .75f;

            PoolManager.Instance.PlayParticle(ParticleEnum.StreamBubbles, Owner.LeftHand.transform.position);
            PoolManager.Instance.PlayParticle(ParticleEnum.StreamBubbles, Owner.RightHand.transform.position);
        }
        else if (HandVelocityL.Swimming && !HandVelocityR.Swimming)
        {
            Owner.SwimmingInput += Owner.CharController.transform.TransformVector(HandVelocityL.SwimVelocity);
            PoolManager.Instance.PlayParticle(ParticleEnum.StreamBubbles, Owner.LeftHand.transform.position);
            SpeedDamp = 1.5f;
        }
        else if (!HandVelocityL.Swimming && HandVelocityR.Swimming)
        {
            Owner.SwimmingInput += Owner.CharController.transform.TransformVector(HandVelocityR.SwimVelocity);
            PoolManager.Instance.PlayParticle(ParticleEnum.StreamBubbles, Owner.RightHand.transform.position);
            SpeedDamp = 1.5f;
        }
        else
        {
            SpeedDamp = 5f;
        }

        if (RotateRight.GetStateDown(SteamVR_Input_Sources.RightHand))
        {
            Owner.transform.eulerAngles += new Vector3(0, 30, 0);
        }
        if (RotateLeft.GetStateDown(SteamVR_Input_Sources.RightHand))
        {
            Owner.transform.eulerAngles -= new Vector3(0, 30, 0);
        }
    }

    private void AboveWaterSwimming()
    {
        if(HandVelocityL.Swimming && HandVelocityR.Swimming && Owner.Swimming)
        {
            Owner.StartSwimCooldown();

            Vector3 dist = HandVelocityL.transform.position - HandVelocityR.transform.position;
            dist = Camera.main.transform.InverseTransformVector(dist);

            SpeedDamp = 1f;

            Vector3 forwardVector = Camera.main.transform.forward;
            forwardVector.y = 0;

            dist.y = 0;
            Owner.SwimmingInput += forwardVector * Mathf.Abs(dist.x);
        }
        else if(HandVelocityL.Swimming && HandVelocityR.Swimming && !Owner.Swimming)
        {
            SpeedDamp = 0.25f;
        }
        else if (!HandVelocityL.Swimming && !HandVelocityR.Swimming)
        {
            Owner.Swimming = true;
            SpeedDamp = 0.25f;
        }
    }
    

}
