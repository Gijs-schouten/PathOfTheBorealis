using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FishAI : MonoBehaviour
{
    Quaternion desiredAngleRight;
    Quaternion desiredAngleLeft;
    List<Quaternion> rotations = new List<Quaternion>();
    float timeleft = 2.5f;
    public bool test;

    private bool randomAngle;
    private Animator anim;
    private NavMeshAgent agent;
    private Transform movePoint;

    void Start()
    {
        anim = GetComponent<Animator>();
        desiredAngleRight = transform.rotation;
        desiredAngleLeft = transform.rotation;
        rotations.Add(desiredAngleRight);
        rotations.Add(desiredAngleLeft);
        agent = GetComponent<NavMeshAgent>();
        movePoint = transform.GetChild(0);
    }

    void OnDrawGizmos()
     {
         Vector3 fwd = transform.forward;
        Gizmos.color = Color.red;
         Gizmos.DrawRay(transform.position, -transform.right);
     }

    void Update()
    {
        //anim.SetFloat("swimSpeed", agent.speed);
        RaycastHit hit;
        timeleft -= Time.deltaTime;
        
        if (timeleft < 0)
        {
            randomAngle = (Random.value > 0.5f);
            timeleft = 2.5f;
        }
        if (Physics.Raycast(transform.position, -transform.right, out hit, 3f))
        {
           /*Debug.Log(hit.transform.name);
             if (hit.transform == GameObject.Find("Hook"))
              {
                  agent.destination = hit.transform.position;
                  return;
              }
              */
           float fishDistance = Vector3.Distance(transform.position, hit.transform.position);
             desiredAngleRight *= Quaternion.Euler(0, 10 / fishDistance, 0);
             desiredAngleLeft *= Quaternion.Euler(0, -10 / fishDistance, 0);
             
            if (randomAngle)
            {
               // test = true;
                anim.SetTrigger("slowSwim");
                transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredAngleRight, 200 * Time.deltaTime);
                agent.speed = 0;
                return;
            }
            else
            {
              // test = true;
                anim.SetTrigger("slowSwim");
                transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredAngleLeft, 200 * Time.deltaTime);
                agent.speed = 0;
                return;
            }
           
        }
        test = false;
        //anim.SetBool("isTurning", false);
        agent.destination = movePoint.transform.position;
        agent.speed = 2;

    
    }

}
    