using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public float speed = .001f;
    float rotationSpeed = .25f;
    Vector3 averageHeading;
    Vector3 averagePosition;
    public float neighbourDistance = 20;
   public GameObject globalFlock;

    public bool turning;
    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(0.5f, 1);
       
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Vector3.Distance(transform.position, globalFlock.transform.position) >= GlobalFlock.tankSize)
        {
            turning = true;
        }
        else
        {
            turning = false;
        }
        if (turning)
        {
            Vector3 dir = globalFlock.transform.position - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation,
                                                                        Quaternion.LookRotation(dir),
                                                                        rotationSpeed * Time.deltaTime);
            speed = Random.Range(0.5f, 1);
        }
        else
        {
            if (Random.Range(0, 5) < 1)
            {
                ApplyRules();                
            }
        }
        transform.Translate(0, 0, Time.deltaTime * speed);

    }

    void ApplyRules()
    {
        GameObject[] gos;
        gos = GlobalFlock.allFish;

        Vector3 vcentre = globalFlock.transform.position;
        Vector3 vavoid = globalFlock.transform.position;
        float gSpeed = 0.1f;

        Vector3 goalPos = GlobalFlock.goalPos;

        float dist;

        int groupSize = 0;
     
        foreach(GameObject go in gos)
        {
            if (go != this.gameObject)
            {
                dist = Vector3.Distance(go.transform.position, this.transform.position);
                if(dist <= neighbourDistance)
                {
                    vcentre += go.transform.position;
                    groupSize++;
                    if (dist < 1.0f){
                        vavoid = vavoid + (this.transform.position - go.transform.position);
                    }
                    Flock anotherflock = go.GetComponent<Flock>();
                    gSpeed = gSpeed + anotherflock.speed;
                }
            }
        }
        if(groupSize > 0)
        {
            Debug.Log(groupSize);
            vcentre = vcentre / groupSize + (goalPos - this.transform.position);
            speed = gSpeed / groupSize;

            // Vector3 dir = (vcentre + vavoid) - transform.position;
            Vector3 dir = goalPos;
            if(dir != goalPos)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation,
                                                Quaternion.LookRotation(dir),
                                                rotationSpeed * Time.deltaTime);
            }
        }
    }

    
}
