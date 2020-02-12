using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum RhinoStates
{
    Chase,
    Patrol,
    Rest,
    Attack
}

public class RhinoController : MonoBehaviour
{
    public bool specialItem;

    [Header("Animator Settings")]
    [SerializeField] private Animator anim;

    [Header("Target Settings")]
    [SerializeField] private GameObject[] target;
    [SerializeField] private float attackRange;
    [SerializeField] private float shoutRange;

    [Header("Waypoint Settings")]
    [SerializeField] private Transform[] waypoints;

    [Header("Item settings")]
    [SerializeField] private Transform item;
    [SerializeField] private GameObject kristal;

    [Header("Speed settings")]
    [SerializeField] private float runSpeed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float rotationSpeed;

    private NavMeshAgent navmeshAgent;
    private int destPoint = 0, cachedAnimationState = 1;
    private float cachedSpeed;
    private bool reset;

    //idle reset
    public RhinoStates rhinoState
    {
        get;
        set;
    }

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        ItemUpdate();
        StateUpdate();
    }

    private void Init()
    {
        rhinoState = RhinoStates.Patrol;
        navmeshAgent = GetComponent<NavMeshAgent>();
        navmeshAgent.speed = walkSpeed;
        cachedSpeed = navmeshAgent.speed;

        GotoNextPoint();

        SetAnimationState(cachedAnimationState); //Equals 1 for walking
    }

    private void AttackPlayer()
    {
        RotateTowards(target[0].transform);
    }

    private void RunTowardsPlayer()
    {
        SetTarget(target[0]);
    }

    private void Patrol()
    {
        if (navmeshAgent.enabled == true && !navmeshAgent.pathPending && navmeshAgent.remainingDistance < 0.5f)
        {
            anim.speed = 1;
            StartCoroutine(Idle());
        }
        else if (navmeshAgent.enabled == true)
        {
            anim.speed = navmeshAgent.velocity.magnitude / navmeshAgent.speed;
        }
    }

    private void Rest()
    {
        SetTarget(kristal);
        if (navmeshAgent.enabled == true && !navmeshAgent.pathPending && navmeshAgent.remainingDistance < 0.5f)
        {
            anim.speed = 1;
            SetAnimationState(4);
        }
        else if (navmeshAgent.enabled == true)
        {
            anim.speed = navmeshAgent.velocity.magnitude / navmeshAgent.speed;
        }
    }

    private IEnumerator Idle()
    {
        navmeshAgent.speed = 0;
        navmeshAgent.enabled = false;
        if (Random.Range(0, 1) == 0) SetAnimationState(0);
        else SetAnimationState(2); //Equals 0/2 for idling or eating
        yield return new WaitForSeconds(4f);
        navmeshAgent.enabled = true;
        GotoNextPoint();
        SetAnimationState(cachedAnimationState);
        navmeshAgent.speed = cachedSpeed;
    }

    private void RotateTowards(Transform thisTarget)
    {
        Vector3 direction = (thisTarget.position - transform.position).normalized;
        direction.y = 0;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }

    private void GotoNextPoint()
    {
        if (waypoints.Length == 0)
            return;

        navmeshAgent.destination = waypoints[destPoint].position;
        destPoint = (destPoint + 1) % waypoints.Length;
    }

    private void SetTarget(GameObject target)
    {
        navmeshAgent.destination = target.transform.position;
    }

    private void SetAnimationState(int state)
    {
        anim.SetInteger("animationState", state);
    }

    private void ItemUpdate()
    {
        if (specialItem == false)
        {
            if ((item.transform.position - target[0].transform.position).sqrMagnitude < 20.0f)
            {
                if (rhinoState != RhinoStates.Chase)
                {
                    anim.speed = 1;
                    ResetNavmeshValues(runSpeed, 30f, true);
                    SetAnimationState(3);
                    rhinoState = RhinoStates.Chase;
                }
                if (((transform.position - target[0].transform.position).sqrMagnitude < shoutRange))
                {
                    SetAnimationState(5);
                }
            }
            else if ((transform.position - target[0].transform.position).sqrMagnitude < attackRange)
            {
                if (rhinoState != RhinoStates.Attack)
                {
                    anim.speed = 1;
                    ResetNavmeshValues(0f, 0f, false);
                    SetAnimationState(4);
                    rhinoState = RhinoStates.Attack;
                }
            }
            else
            {
                if (rhinoState != RhinoStates.Patrol)
                {
                    anim.speed = 1;
                    ResetNavmeshValues(walkSpeed, 0f, true);
                    SetAnimationState(1);
                    rhinoState = RhinoStates.Patrol;
                }
            }
        }
        else
        {
            if (rhinoState != RhinoStates.Rest)
            {
                anim.speed = 1;
                ResetNavmeshValues(walkSpeed, 0f, true);
                SetAnimationState(1);
                rhinoState = RhinoStates.Rest;
            }
        }
    }
    private void StateUpdate()
    {
        if (rhinoState == RhinoStates.Patrol)
        {
            Patrol();
        }
        else if (rhinoState == RhinoStates.Chase)
        {
            RunTowardsPlayer();
        }
        else if (rhinoState == RhinoStates.Attack)
        {
            AttackPlayer();
        }
        else if (rhinoState == RhinoStates.Rest)
        {
            Rest();
        }
    }

    private void ResetNavmeshValues(float speedF, float stoppingDistanceF, bool autobraking)
    {
        navmeshAgent.stoppingDistance = stoppingDistanceF;
        navmeshAgent.speed = speedF;
        navmeshAgent.autoBraking = autobraking;
    }
}
