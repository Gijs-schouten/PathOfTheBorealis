using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using Valve.VR;

public class PaddleManager : MonoBehaviour
{
    #region References
    [SerializeField] public Paddle LeftPaddle;
    [SerializeField] public Paddle RightPaddle;

    public PlayerStatemachine Player;
    [HideInInspector] public Hand LeftHand;
    [HideInInspector] public Hand RightHand;

    private Rigidbody Rb;
    private BoxCollider Collider;
    private Interactable Interactable;
    //private CenterOfMass COM;
    //[HideInInspector] public WaterInterface Water;

    public GameObject BoatRear;
    public GameObject BoatFront;
    public GameObject PlayerSeat;
    public GameObject PlayerExit;
    #endregion

    #region Variables
    public float PaddleSide;
    public float PaddleDirection;
    public float PaddleForce;

    #endregion

    #region Private Variables
    private SteamVR_Action_Boolean Grab = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("GrabGrip");
    #endregion

    private void Awake()
    {
        Collider = GetComponent<BoxCollider>();
        Rb = GetComponent<Rigidbody>();
        Interactable = GetComponent<Interactable>();
        //Water = GetComponent<WaterInterface>();
        //COM = GetComponent<CenterOfMass>();
        LeftHand = Player.LeftHand;
        RightHand = Player.RightHand;
    }

    private void Update()
    {
        if(LeftPaddle != null && RightPaddle != null)
        {
            Vector3 dist = BoatRear.transform.position - BoatFront.transform.position;
            PaddleForce = (LeftPaddle.Force + RightPaddle.Force);

            if (LeftPaddle.InWater && !RightPaddle.InWater)
            {
                Vector3 dirToPaddle = transform.position - LeftPaddle.transform.position;
                PaddleSide = AngleDir(dist, dirToPaddle, Vector3.up);
                if (PaddleDirection == 0)
                {
                    Vector3 distance = LeftPaddle.Distance;
                    distance.Normalize();
                    if (distance.z < 0)
                        PaddleDirection = 1;
                    if (distance.z > 0)
                        PaddleDirection = -1;
                    Debug.Log(distance);
                }

                //Debug.Log(AngleDir(-transform.right, transform.TransformVector(LeftPaddle.Distance), Vector3.up));
            }
            if (!LeftPaddle.InWater && RightPaddle.InWater)
            {
                Vector3 dirToPaddle = transform.position - RightPaddle.transform.position;
                PaddleSide = AngleDir(dist, dirToPaddle, Vector3.up);
                if(PaddleDirection == 0)
                {
                    Vector3 distance = RightPaddle.Distance;
                    distance.Normalize();
                    if (distance.z < 0)
                        PaddleDirection = 1;
                    if (distance.z > 0)
                        PaddleDirection = -1;
                    Debug.Log(distance);
                }


                //Debug.Log(AngleDir(transform.right, transform.TransformVector(RightPaddle.Distance), Vector3.up));
            }
        }

        if(Interactable.hoveringHand != null)
        {
            if (Grab.GetStateDown(Interactable.hoveringHand.trackedObject.inputSource) && Interactable.isHovering)
            {
                EnterBoat();
            }
        }
    }

    private void FixedUpdate()
    {
        if (!LeftPaddle.InWater && !RightPaddle.InWater)
        {
            PaddleDirection = 0;
        }
    }

    public float AngleDir(Vector3 fwd, Vector3 targetDir, Vector3 up)
    {
        Vector3 perp = Vector3.Cross(fwd, targetDir);
        float dir = Vector3.Dot(perp, up);

        if (dir > 0f)
        {
            return 1f;
        }
        else if (dir < 0f)
        {
            return -1f;
        }
        else
        {
            return 0f;
        }
    }

    public void EnterBoat()
    {
        Player.PaddleManager = this;
        Player.State = PlayerState.Kayaking;
        Collider.enabled = false;
        Rb.constraints = RigidbodyConstraints.None;
    }

    public void ExitBoat()
    {
        Collider.enabled = true;
        Rb.constraints = RigidbodyConstraints.FreezePositionX; Rb.constraints = RigidbodyConstraints.FreezePositionZ;
        Rb.constraints = RigidbodyConstraints.FreezeRotationX; Rb.constraints = RigidbodyConstraints.FreezeRotationY;
    }
}
