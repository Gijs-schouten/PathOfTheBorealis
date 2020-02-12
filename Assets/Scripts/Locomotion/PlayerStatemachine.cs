using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;
using UnityEngine.SceneManagement;

public enum PlayerState
{
    Walking,
    Climbing,
    Kayaking,
    Swimming,
    Ziplining
}

public interface IState
{
    void Enter();
    void Execute();
    void Exit();
    void FixedExecute();
}

public class PlayerStatemachine : MonoBehaviour
{
     
    #region references
    public PlayerState State;
    public Hand LeftHand;
    public Hand RightHand;

    [HideInInspector] public CharacterController CharController;
    [HideInInspector] public Player Player;
    [HideInInspector] public HandVelocity RightHandVelocity;
    [HideInInspector] public HandVelocity LeftHandVelocity;
    [HideInInspector] public PaddleManager PaddleManager;
    [HideInInspector] public CamPostProcessing CameraPostProcessing;
    [HideInInspector] public Animator BodyAnimator;
    [HideInInspector] public GameObject LeftHandSwimPose, RightHandSwimPose;
    [HideInInspector] public WallDetection Climber;
    #endregion 

    #region settings
    public float MovementSpeed = 2;
    public float SwimmingSpeed = 1;
    #endregion

    #region variables
    [HideInInspector] public float OldGravity;
    [HideInInspector] public Vector3 Velocity;
    [HideInInspector] public Vector3 MovementInput;
    [HideInInspector] public Vector3 SwimmingInput;
    [HideInInspector] public float WorldHeight;
    [HideInInspector] public float JumpHeight = 6;
    [HideInInspector] public List<GameObject> WaterPlanes;

    public SteamVR_Action_Boolean SceneButton = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("SwitchScene");

    #endregion

    #region private variables
    private PlayerState LastFrameState;
    private StateMachine StateMachine = new StateMachine();
    #endregion

    private void Awake()
    {
        CharController = GetComponent<CharacterController>();
        Player = GetComponent<Player>();
        RightHandVelocity = RightHand.GetComponent<HandVelocity>();
        LeftHandVelocity = LeftHand.GetComponent<HandVelocity>();
        StateMachine.ChangeState(new MovementController(this));
        CameraPostProcessing = CamPostProcessing.Instance;
        BodyAnimator = GetComponentInChildren<Animator>();
        Climber = GetComponentInChildren<WallDetection>();
        LeftHandSwimPose = LeftHand.transform.GetChild(0).gameObject; LeftHandSwimPose.SetActive(false);
        RightHandSwimPose = RightHand.transform.GetChild(0).gameObject; RightHandSwimPose.SetActive(false);
    }

    private void Update()
    {
        if (State != LastFrameState)
        {
            switch (State)
            {
                case PlayerState.Walking:
                    StateMachine.ChangeState(new MovementController(this));
                    break;
                case PlayerState.Climbing:
                    StateMachine.ChangeState(new ClimbingState(this));
                    break;
                case PlayerState.Kayaking:
                    StateMachine.ChangeState(new KayakState(this));
                    break;
                case PlayerState.Swimming:
                    StateMachine.ChangeState(new SwimmingState(this));
                    break;
                case PlayerState.Ziplining:

                    break;
            }
        }
        LastFrameState = State;

        if (StateMachine.currentState != null)
            StateMachine.currentState.Execute();

        if (SceneButton.GetActive(SteamVR_Input_Sources.Any))
        {
            int currentScene = SceneManager.GetActiveScene().buildIndex;
            if((currentScene += 1) <= SceneManager.sceneCount)
            {
                SceneManager.LoadScene(currentScene += 1);
            }
            else
            {
                SceneManager.LoadScene(0);
            }
        }
    }

    public void AddGravity(PlayerState stateToAddGravity, float multiplier = 0)
    {
        switch (stateToAddGravity)
        {
            case PlayerState.Walking:
                if (multiplier == 0)
                    multiplier = MovementSpeed;

                Velocity.y = CharController.isGrounded ? 0 : OldGravity -= (8.91f / multiplier) * Time.deltaTime;
                break;
            case PlayerState.Swimming:
                //Maybe later
                break;
            case PlayerState.Climbing:
                Velocity.y -= 8.91f * Time.deltaTime;
                break;
        }
        
    }

    
    private void FixedUpdate()
    {
        if (StateMachine.currentState != null)
            StateMachine.currentState.FixedExecute();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            if (!WaterPlanes.Contains(other.gameObject))
                WaterPlanes.Add(other.gameObject);

            CheckWater();
        }
    }

    public void CheckWater()
    {
        if (WaterPlanes.Count > 0)
        {
            for (int i = 0; i < WaterPlanes.Count; i++)
            {
                if (CameraPostProcessing.transform.position.y + .2f < WaterPlanes[i].transform.position.y)
                {
                    State = PlayerState.Swimming;
                }
                else if (CameraPostProcessing.transform.position.y + 1f > WaterPlanes[i].transform.position.y)
                {
                    State = PlayerState.Walking;
                }
                else
                {
                    Velocity.y = 0;
                }
            }
        }
    }

}

public class StateMachine
{
    public IState currentState;

    public void ChangeState(IState newState)
    {
        if (currentState != null)
            currentState.Exit();

        currentState = newState;
        newState.Enter();
    }
}