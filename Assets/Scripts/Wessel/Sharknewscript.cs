using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Sharknewscript : MonoBehaviour
{
    
    public Transform[] targets;
    public Transform[] secondTargets;
    public bool trigger = false;
    public GameObject invisibleBarrier;

    [SerializeField]
    private float rotationSpeed = 5f;
    private float speed = 5;
    private int current;
    private int rounds;
    private int newRounds;
    private int secondCurrent;
    private Transform newTarget;
    private Transform secondTarget;
    private bool checkPlayer;

    public CheckPlayerBoat otherScript;


    Vector3 targetDir;
    Quaternion rotationTarget;

    void Start()
    {
        otherScript.GetComponent<CheckPlayerBoat>();
        
    }
    void Update()
    {
        if (!trigger)
        {
            newTarget = targets[current];
            Vector3 targetDir = newTarget.transform.position - transform.position;
            rotationTarget = Quaternion.LookRotation(targetDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotationTarget, rotationSpeed * Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, newTarget.transform.position, speed * Time.deltaTime);
        }
        else
        {
            secondTarget = secondTargets[secondCurrent];
            Vector3 targetDir = secondTarget.transform.position - transform.position;
            rotationTarget = Quaternion.LookRotation(targetDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotationTarget, rotationSpeed * Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, secondTarget.transform.position, speed * Time.deltaTime);
        }

        //Go away from Boat
        if (transform.position == newTarget.position)
        {
            current++;
           
            if (current == targets.Length)
            {
                rounds++;
                if (rounds >= 3)
                {
                    trigger = true;
                    rounds = 0;
                    invisibleBarrier.SetActive(false);
                }
                current = 0;
            }
        }

        //Go to Boat
        if (transform.position == secondTarget.position)
        {
            secondCurrent++;
            Debug.Log(otherScript.triggerPlayer);
            if (secondCurrent == secondTargets.Length)
            {
                newRounds++;
              
             
                if (otherScript.triggerPlayer == false && newRounds >= 3)
                {
                    trigger = false;
                    newRounds = 0;
                    invisibleBarrier.SetActive(true);
                }
                secondCurrent = 0;
            }
       
        }
    }
    
}
