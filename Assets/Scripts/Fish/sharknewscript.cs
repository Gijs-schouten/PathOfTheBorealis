using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class sharknewscript : MonoBehaviour
{
    public Transform[] targets;
    public bool isMoving;
    public bool rotating;
    public float speed;
    public int current;
    public Transform newTarget;
    float rotationTime;
    Vector3 targetDir;
    Quaternion rotationTarget;
    private Animator anim;

    void Start()
    {
        speed = 5;
        isMoving = false;
        newTarget = targets[Random.Range(0, targets.Length)];
        anim = GetComponent<Animator>();

    }

    void OnDrawGizmos()
    {
        Vector3 fwd = transform.forward;
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward);
    }


    void Update()
    {
        newTarget = targets[current];
        Vector3 targetDir = newTarget.transform.position - transform.position;
        rotationTarget = Quaternion.LookRotation(targetDir);
       transform.rotation = Quaternion.Lerp(transform.rotation, rotationTarget, 1f * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger("Bite");
            speed = 20;
            StartCoroutine(rotateanim());
        }
        
        if (transform.position == newTarget.position)
        {
            current++;
            if (current == targets.Length)
            {
                current = 0;
                speed = 5;

            }
          
        }
        
        
        transform.position = Vector3.MoveTowards(transform.position, newTarget.transform.position, speed * Time.deltaTime);
    }
    IEnumerator rotateanim()
    {
        yield return new WaitForSecondsRealtime(.8f);

        float animTime = anim.GetCurrentAnimatorStateInfo(0).length;
        Debug.Log(animTime);

        for (int i = 0; i < 15; i++)
        {
            yield return new WaitForEndOfFrame();
            transform.Rotate(0, 0, 3f);
        }
       

    public Transform[] targets;
    public Transform[] secondTargets;
    public bool trigger = false;
    public GameObject invisibleBarrier;

    [SerializeField]
    private float rotationSpeed = 5f;
    private float speed = 1.5f;
    private int current;
    private int rounds;
    private int newRounds;
    private int secondCurrent;
    private Transform newTarget;
    private Transform secondTarget;
    private bool checkPlayer;

    public CheckPlayerShark otherScript;


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
