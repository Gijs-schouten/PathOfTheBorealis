using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flocking : MonoBehaviour
{
    public FlockManager myManager;
    float speed;
   public bool turning = false;
    private float flocker = 20f;
    public GameObject farting;
  public  Vector3 goalPos;
    public bool test;
   //public float rotationSpeed = 5;

    // Use this for initialization
    void Start()
    {

        speed = Random.Range(myManager.minSpeed,
                                myManager.maxSpeed);
        // flocker = GetComponent<FlockManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 goalPos = FlockManager.goalpos;
        if (Vector3.Distance(transform.position, farting.transform.position) >= flocker)
        {
            turning = true;
        }
        else

            turning = false;
        if (turning)
        {
            Vector3 dir = farting.transform.position - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation,
                                                                        Quaternion.LookRotation(dir),
                                                                        myManager.rotationSpeed * Time.deltaTime);
        }
        else
        {
            if (Random.Range(0, 5) < 3)
            {
                ApplyRules();
     
            }
        }
        transform.Translate(0, 0, Time.deltaTime * speed);      

    }
    void ApplyRules()
    {
        GameObject[] gos;
        gos = myManager.allFish;

        Vector3 vcentre = farting.transform.position;
        Vector3 vavoid = farting.transform.position;
        float gSpeed = 0.01f;
        float nDistance;
        int groupSize = 0;

       

        foreach (GameObject go in gos)
        {
            test = false;
            if (go != this.gameObject)
            {
                nDistance = Vector3.Distance(go.transform.position, transform.position);
                if (nDistance <= myManager.neighbourDistance)
                {
                    vcentre += go.transform.position;
                    groupSize++;

                    if (nDistance < 1.0f)
                    {
                        vavoid = vavoid + (transform.position - go.transform.position);
                    }

                    Flocking anotherFlock = go.GetComponent<Flocking>();
                    gSpeed = gSpeed + anotherFlock.speed;
                }
            }
        }

        if (groupSize > 0)
        {
              vcentre = vcentre / groupSize + (goalPos - transform.position);
            //speed = gSpeed / groupSize;
            // vcentre = goalPos;
           
            Vector3 direction = (vcentre + vavoid) - transform.position;
            if (direction != goalPos)
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(goalPos), myManager.rotationSpeed * Time.deltaTime);
            test = true;
        }
    }
}