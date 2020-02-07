using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flock : MonoBehaviour
{
    public float speed = 0.5f;
    float rotationSpeed = 4f;
    Vector3 avarageHeading;
    Vector3 avaragePosition;
    float neighbourDistance = 4f;
    public Transform goalStart;
    Vector3 goalPos;
    
    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(0.5f, 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.GetChild(0).transform.position, transform.TransformDirection(Vector3.down), out hit, 3f))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + (0.04f * speed), transform.position.z);
        }


        if (transform.position.y >= 40f)
        {

           
            //ApplyRules(true);
            goalPos = GlobalFlog.start;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(goalPos), rotationSpeed * Time.deltaTime);
        }
        else { 
              if (Random.Range(0, 5) < 1)
              {
                   ApplyRules(false);
              }
        }

        transform.Translate(0, 0, Time.deltaTime * speed);
       
    }

    void ApplyRules(bool back)
    {
        GameObject[] gos;
        gos = GlobalFlog.allFish;

        Vector3 vCentre = Vector3.zero;
        Vector3 vAvoid = Vector3.zero;
        float gSpeed = 0.1f;
        if (!back)
        {
            goalPos = GlobalFlog.goalPos;
        }
        else
        {
            goalPos = GlobalFlog.start;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(goalPos), rotationSpeed * Time.deltaTime);
        }

        float dist;

        int groupSize = 0;
        foreach (GameObject go in gos)
        {
            if (go != this.gameObject)
            {

                dist = Vector3.Distance(go.transform.position, this.transform.position);
                if (dist <= neighbourDistance)
                {
                    vCentre += go.transform.position;
                    groupSize++;

                    if (dist < 2f)
                    {
                        vAvoid = vAvoid + (this.transform.position - go.transform.position);
                    }

                    flock anotherFlock = go.GetComponent<flock>();
                    gSpeed = gSpeed + anotherFlock.speed;
                }
            }
        }

        if(groupSize > 0)
        {
            vCentre = vCentre / groupSize + (goalPos - this.transform.position);
            speed = gSpeed / groupSize;

            Vector3 direction = (vCentre + vAvoid) - transform.position;
            if(direction!= Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
            }

        }
    }
}
