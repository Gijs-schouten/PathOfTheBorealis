using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum BasicAnimalStates
{
    Walking,
    Running,
    Idle
}
public class BasicAnimal : MonoBehaviour
{
    [SerializeField] bool hide = false;
    [SerializeField]
    private float minTargetRange, maxTargetRange, wanderSpeed, runSpeed, rangeWaypoint;

    [SerializeField]
    private Transform target, hideSpot;
    private NavMeshAgent agent;

    [SerializeField] private Animator anim;

    [SerializeField] private Transform[] points;
    private int destPoint = 0;

    private bool reset, rareAnimal, seen;

    public BasicAnimalStates BasicAnimalState
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
        if (DetectPlayerInRange(minTargetRange,maxTargetRange,target) == true)
        {
            reset = false;
            if (hide) { seen = true; }
            Flee();
        }
        else if (agent.enabled == true && seen == false){
            if (reset == false)
            {
                GotoNextPoint();
                reset = true;
            }
            Walk();
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
        if (!agent.pathPending && agent.remainingDistance < rangeWaypoint)
            StartCoroutine(Idle());
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

    private void Flee()
    {
        if (agent.enabled == false)
        {
            StopAllCoroutines();
            agent.enabled = true;
        }
        if (anim.GetInteger("state") != 1)
        {
            anim.SetInteger("state", 1);
        }

        agent.speed = runSpeed;
        anim.speed = 1;

        if (hide)
        {
            agent.SetDestination(hideSpot.transform.position);
            Destroy(gameObject,5f);
        }
        else
        {
            Debug.Log("run");
            Vector3 runTo = transform.position + ((transform.position - target.position) * 10);
            float distance = Vector3.Distance(transform.position, target.position);
            if (distance < 30f) agent.SetDestination(runTo);
        }

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

    private IEnumerator Idle()
    {
        anim.speed = 1;
        anim.SetInteger("state", 2);
        agent.enabled = false;
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length * 3);
        agent.enabled = true;
        GotoNextPoint();
        anim.SetInteger("state", 0);
    }
}
