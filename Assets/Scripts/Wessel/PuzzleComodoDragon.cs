using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum ComodoStates
{
    Walking,
    Running,
    Idle,
    Eating,
    Attacking
}
public class PuzzleComodoDragon : MonoBehaviour
{
    //Target
    [SerializeField] private Transform target, foodTarget;
    //Customizable Vars
    [SerializeField] private float minTargetRange, maxTargetRange, wanderSpeed, runSpeed, rangeWaypoint;

    //Things
    [SerializeField] private Animator anim;
    [SerializeField] private NavMeshAgent agent;

    //Waypoint vars
    [SerializeField] private Transform[] points;

    //private vars used by the script
    private int destPoint = 0;
    private bool reset, resetIdle;

    public bool hasFood = false;

    public ComodoStates ComodoState
    {
        get;
        set;
    }

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        GotoNextPoint();
    }

    private void Update()
    {
        if (hasFood == false)
        {
            if (DetectPlayerInRange(minTargetRange, maxTargetRange, target) == true)
            {
                reset = false;
                Attack();
            }
            else if (agent.enabled == true)
            {
                if (reset == false)
                {
                    GotoNextPoint();
                    reset = true;
                }
                Walk();
            }
            else if (agent.enabled == false && resetIdle == true)
            {
                AttackReset();
            }
        }
        else
        {
            Run();
        }
    }

    private void Walk()
    {
        if (anim.GetInteger("state") != 0)
        {
            anim.SetInteger("state", 0);
        }

        agent.speed = wanderSpeed;
        anim.speed = agent.velocity.magnitude / agent.speed;

        if (anim.speed > 1)
        {
            anim.speed = 1;
        }

        Debug.Log(agent.pathPending);
        if (!agent.pathPending && agent.remainingDistance <= rangeWaypoint + 1)
        {
            StartCoroutine(Idle());
        }
    }

    private void Attack()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        direction.y = 0;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 4);

        resetIdle = true;
        Debug.Log("Comodo Attack");
        if (anim.GetInteger("state") != 4)
        {
            agent.enabled = false;
            anim.speed = 1;
            anim.SetInteger("state", 4);
        }
    }
    
    private void AttackReset()
    {
        agent.enabled = true;
    }

    private IEnumerator Idle()
    {
        agent.enabled = false;
        resetIdle = false;
        anim.speed = 1;
        anim.SetInteger("state", 2);
        Debug.Log("IsIdling");
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        agent.enabled = true;
        GotoNextPoint();
        anim.SetInteger("state", 0);
        resetIdle = true;
    }

    private void Eat()
    {
        anim.speed = 1;
        agent.speed = 0;
        anim.SetInteger("state", 3);
    }

    void Run()
    {
        agent.destination = foodTarget.position;
        if (anim.GetInteger("state") != 1)
        {
            anim.SetInteger("state", 1);
        }

        agent.speed = runSpeed;
        agent.angularSpeed = 120f;
        anim.speed = agent.velocity.magnitude / agent.speed;

        if (anim.speed > 1)
        {
            anim.speed = 1;
        }

        if (!agent.pathPending && agent.remainingDistance <= rangeWaypoint)
            Eat();
    }

    private void GotoNextPoint()
    {
        // Returns if no points have been set up
        if (points.Length == 0)
            return;

        // Set the agent to go to the currently selected destination.
        agent.destination = points[destPoint].position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint = (destPoint + 1) % points.Length;
    }

    private bool DetectPlayerInRange(float minRange, float maxRange, Transform targetTransform)
    {
        bool inRange;
        float dist = Vector3.Distance(targetTransform.position, transform.position);
        if (dist > minRange && dist < maxRange)
        {
            inRange = true;
        }
        else
        {
            inRange = false;
        }

        return inRange;
    }

}
