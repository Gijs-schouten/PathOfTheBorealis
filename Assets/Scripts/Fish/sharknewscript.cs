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
       
    }

}
