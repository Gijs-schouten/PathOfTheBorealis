using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NewFishAI : MonoBehaviour
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

    void Start()
    {
        speed = 5;  
        isMoving = false;
        newTarget = targets[Random.Range(0, targets.Length)];

    }

    void OnDrawGizmos()
    {
        Vector3 fwd = transform.forward;
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward);
    }


    void Update()
    {
       
        Vector3 targetDir = newTarget.transform.position - transform.position;
        rotationTarget = Quaternion.LookRotation(targetDir);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotationTarget, 5f * Time.deltaTime);    

        if (transform.position == newTarget.position)
        {
            newTarget = targets[Random.Range(0, targets.Length)];
        }      
        transform.position = Vector3.MoveTowards(transform.position, newTarget.transform.position, speed * Time.deltaTime);
    }
}

/*IEnumerator startRotation()
{
    float t = 0;
    while (t < 5)
    {
        t += Time.deltaTime;
        Debug.Log("farting");
        speed = 0f;
        newTarget = targets[Random.Range(0, targets.Length)];
        Vector3 targetDir = newTarget.transform.position - transform.position;
        rotationTarget = Quaternion.LookRotation(targetDir);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotationTarget, 1f * Time.deltaTime);

    }
    // speed = 5;
    yield return new WaitForEndOfFrame();
    */