using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleAnimalControllerNoNavmesh : MonoBehaviour
{
    public bool hasObjective;

    [SerializeField] private Animator anim;
    [SerializeField] private Transform player, objectTransform, waypoint;
    [SerializeField] private float minAttackRange, maxAttackRange;

    [SerializeField] private GameObject animalCollider;

    private bool setState, deleteAfterObjective;
    private PuzzleAnimalStates state;

    private float timer;

    void Update()
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
            anim.SetInteger("state", 2);
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
            timer = 0;
            inRange = false;
        }

        return inRange;
    }

    void OnParticleCollision(GameObject other)
    {
        timer = timer + Time.deltaTime * 1;

        if (timer > 1)
        {
            hasObjective = true;
        }
    }
}
