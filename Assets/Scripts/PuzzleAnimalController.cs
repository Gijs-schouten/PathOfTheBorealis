using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum PuzzleAnimalStates
{
    Guard,
    Eat,
    Move
}

public class PuzzleAnimalController : MonoBehaviour
{
    public bool hasObjective;

    [SerializeField] private Animator anim;
    [SerializeField] private Transform player, objectTransform, waypoint;
    [SerializeField] private float minAttackRange, maxAttackRange;

    [SerializeField] private GameObject animalCollider;

    private bool setState , deleteAfterObjective;
    private PuzzleAnimalStates state;
    private NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (hasObjective == true && setState == false) { state = PuzzleAnimalStates.Move; setState = true; }
        if (state == PuzzleAnimalStates.Guard)
        {
            if (DetectPlayerInRange(minAttackRange, maxAttackRange, player))
            {
                Debug.Log("Attack");
                anim.SetInteger("state", 1);
            }
            else
            {
                anim.SetInteger("state", 0);
            }
        }

        else if (state == PuzzleAnimalStates.Move)
        {
            anim.SetInteger("state", 3);
            agent.destination = waypoint.position;
            anim.speed = agent.velocity.magnitude / agent.speed;
            if (animalCollider.activeInHierarchy == true)
            {
                animalCollider.SetActive(false);
            }

            if (!agent.pathPending)
            {
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                    {
                        anim.speed = 1;

                        anim.SetInteger("state", 2);
                    }
                }
            }
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
}
