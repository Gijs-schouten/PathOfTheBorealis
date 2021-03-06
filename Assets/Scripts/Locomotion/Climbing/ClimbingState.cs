﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbingState : IState
{
    private PlayerStatemachine Owner;

    public ClimbingState(PlayerStatemachine owner)
    {
        Owner = owner;
    }

    public void Enter()
    {
        //Owner.BodyAnimator.SetBool("Climbing", true);
        //Owner.IKCalibrator.iksToCalibrate[0].solver.locomotion.weight = 0;
        Owner.CharController.enableOverlapRecovery = false;
    }

    public void Execute()
    {
        if (!Owner.LeftHandVelocity.Climbing && !Owner.RightHandVelocity.Climbing && Owner.Climber.Point == Vector3.zero)
            Owner.State = PlayerState.Walking;
    }

    public void Exit()
    {
        //Owner.BodyAnimator.SetBool("Climbing", false);
        //Owner.IKCalibrator.iksToCalibrate[0].solver.locomotion.weight = 1;
        Owner.CharController.enableOverlapRecovery = true;
    }

    public void FixedExecute()
    {

    }
}
