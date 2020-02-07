using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;   

public class HandVelocity : MonoBehaviour
{
    #region References
    private PlayerStatemachine StateMachine;
    [HideInInspector] public Hand hand;
    #endregion

    #region Private variables
    private bool Moving;
    private bool Hopping;
    private int LastVelocityInt;
    private int CurrentVelocityInt;
    private int VelocityLogSize = 5;
    private int FramesRemoved = 2;
    private float ClimbTimer;

    private Vector3 StartVelocity;
    private Vector3 GrabPos;
    private Vector3 LastFrameHand;
    private Vector3 StartingTransform;
    private Vector3 refVelocity;
    private Vector3 refLedgeVel;

    private List<Vector3> LastFramesVel = new List<Vector3>();
    private HandVelocity OtherHandVel;
    private WallDetection DetectWall;

    private SteamVR_Action_Boolean Trigger = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("InteractUI");
    [HideInInspector] public SteamVR_Action_Boolean Grab = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("GrabGrip");
    #endregion

    #region Public variables
    [HideInInspector] public bool Climbing;
    [HideInInspector] public bool Swimming;
    [HideInInspector] public bool Switched;

    [HideInInspector] public Transform GrabbedObject;
    [HideInInspector] public Vector3 SwimVelocity;
    [HideInInspector] public Vector3 Motion;
    #endregion

    private void Awake()
    {
        StateMachine = transform.root.GetComponent<PlayerStatemachine>();
        hand = GetComponent<Hand>();
        OtherHandVel = hand.otherHand.GetComponent<HandVelocity>();
        LastFrameHand = transform.localPosition;
        StartingTransform = transform.localPosition;
        DetectWall = transform.parent.GetComponentInChildren<WallDetection>();
    } 

    private void Update()
    {  

        #region Climbing

        if (GrabbedObject != null)
        {
            if (!Climbing)
            {
                StateMachine.State = PlayerState.Climbing;
                GrabPos = transform.localPosition;
                Climbing = true;
            }

            Motion = GrabPos - transform.localPosition;

            if (OtherHandVel.Climbing)
            {
                Motion = Vector3.Max(Motion, OtherHandVel.Motion);
                Motion /= 2;
            }

            StateMachine.CharController.Move(StateMachine.CharController.transform.TransformVector(Motion));
            Motion = Vector3.zero;
            
            RenderModel model = hand.mainRenderModel;
            model.SetHandPosition(Vector3.SmoothDamp(model.GetHandPosition(), GrabbedObject.position, ref refVelocity, .015f));
            model.SetHandRotation(Quaternion.SlerpUnclamped(model.GetHandRotation(), GrabbedObject.rotation, 30 * Time.deltaTime));   
            
        }
        else if(Climbing == true)
        {
            DetectWall.Checking = true;
            Climbing = false;
        }

        GrabPos = transform.localPosition;

        #endregion

        #region Walking

        if(StateMachine.State == PlayerState.Walking)
        {
            if (Trigger.GetState(hand.trackedObject.inputSource))
            {
                if (Moving == false)
                {
                    StartVelocity = hand.transform.position;
                    Moving = true;
                }

                Vector3 handVelocity = transform.TransformDirection(hand.trackedObject.GetVelocity());
                CurrentVelocityInt = Mathf.RoundToInt(Vector3.Dot(StartVelocity, handVelocity));
                CurrentVelocityInt = Mathf.Clamp(CurrentVelocityInt, -10, 10);

                if (CurrentVelocityInt != 0)
                {
                    StateMachine.MovementInput += new Vector3(0, 0, Mathf.Abs(CurrentVelocityInt)) * Time.deltaTime;
                    CurrentVelocityInt = 0;
                }
            }
            else
            {
                CurrentVelocityInt = 0;
                Moving = false;
            }
        }
        
        #endregion

        #region Swimming

        if (StateMachine.State == PlayerState.Swimming)
        {
            if (Grab.GetState(hand.trackedObject.inputSource))
            {
                if(Swimming)
                {
                    if (LastFramesVel.Count > 0)
                        LastFramesVel.Insert(0, LastFrameHand);
                    if (LastFramesVel.Count > VelocityLogSize)
                        LastFramesVel.RemoveAt(VelocityLogSize);
                }
                else
                {
                    LastFrameHand = transform.localPosition;
                    LastFramesVel.Add(LastFrameHand);
                    Swimming = true;
                }

                SwimVelocity = (LastFrameHand - transform.localPosition);
            }
            else if (Swimming)
            {
                Vector3 handVector = new Vector3();
                if (LastFramesVel.Count >= VelocityLogSize - 1)
                {
                    for (int i = 0; i < LastFramesVel.Count - FramesRemoved; i++)
                    {
                        handVector += LastFramesVel[i];
                    }
                    SwimVelocity = handVector /= LastFramesVel.Count - FramesRemoved;
                    Swimming = false;
                }
                else
                    Swimming = false;
            }

            LastFrameHand = transform.localPosition;
        }

        #endregion
    }
}
