using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum MouseStates
{
    None,
    Moving
}
public class ElephantPuzzleController : MonoBehaviour
{
    [SerializeField] private bool releasedMouse;
    [SerializeField] private Animator mouseAnim, elephantAnim;
    [SerializeField] private float minRangeElephant, maxRangeElephant;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private GameObject mouse, elephant;

    private NavMeshAgent mouseAgent, elephantAgent;
    private PuzzleAnimalStates elephantState;
    private MouseStates mouseState;

    private Vector3 elephantLocation; // Dit is een reference en hij moet geen reference zijn
    private bool isBusyDestroying;

    private void Start()
    {
        mouseAgent = mouse.GetComponent<NavMeshAgent>();
        elephantAgent = elephant.GetComponent<NavMeshAgent>();

        elephantState = PuzzleAnimalStates.Guard;
        mouseState = MouseStates.None;

    }

    private void Update()
    {
        MouseUpdate();
        ElephantUpdate();
    }

    private void MouseUpdate()
    {
        if (mouseState == MouseStates.None)
        {
            if (Input.GetKeyDown(KeyCode.O))
            {
                Debug.Log("Switch State");
                elephantState = PuzzleAnimalStates.Move;
                mouseState = MouseStates.Moving;

                Vector3 tempVarLocation = elephant.transform.position;
                elephantLocation = tempVarLocation;
                //New Trasform get data new transform
            }
        }
        else if (mouseState == MouseStates.Moving && mouse != null)
        {
            mouseAnim.SetInteger("state", 1); //Walk
            mouseAgent.destination = transform.position;
            mouseAnim.speed = (mouseAgent.velocity.magnitude / mouseAgent.speed) * 4 ;


            if (!mouseAgent.pathPending)
            {
                if (mouseAgent.remainingDistance <= mouseAgent.stoppingDistance)
                {
                    if (!mouseAgent.hasPath || mouseAgent.velocity.sqrMagnitude == 0f)
                    {
                        Destroy(mouse, 2f);
                    }
                }
            }
        }
    }

    private void ElephantUpdate()
    {
        if (elephantState == PuzzleAnimalStates.Guard)
        {
            if (DetectPlayerInRange(minRangeElephant, maxRangeElephant, playerTransform))
            {
                elephantAnim.SetInteger("state", 1); // Attack
            }
            else
            {
                elephantAnim.SetInteger("state", 0);
            }
        }
        else if (elephantState == PuzzleAnimalStates.Move)
        {
            if (isBusyDestroying == false)
            {
                Destroy(elephant, 1f);
                isBusyDestroying = true;
            }
        }
    }

    private bool DetectPlayerInRange(float minRange, float maxRange, Transform targetTransform)
    {
        bool inRange;
        float dist = Vector3.Distance(targetTransform.position, elephant.transform.position);
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
