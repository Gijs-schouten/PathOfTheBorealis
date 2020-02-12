using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockManager : MonoBehaviour
{
    public GameObject fishPrefab;
    public int numFish = 20;
    public GameObject[] allFish;

    public Vector3 swimLimits = new Vector3(5, 5, 5);
    [Range(1.0f, 10.0f)]
    public float neighbourDistance;
    [Range(0.0f, 5.0f)]
    public float rotationSpeed;
   public static Vector3 goalpos;
    public GameObject goalPrefab;

    [Header("Fish Settings")]
    [Range(0.0f, 5.0f)]
    public float minSpeed;
    [Range(0.0f, 5.0f)]
    public float maxSpeed;
    public  int tankSize = 5;

    // Use this for initialization
    void Start()
    {
         
        allFish = new GameObject[numFish];
        for (int i = 0; i < numFish; i++)
        {
            Vector3 pos = this.transform.position + new Vector3(Random.Range(-swimLimits.x, swimLimits.x),
                                                                  Random.Range(-swimLimits.y, swimLimits.y),
                                                                  Random.Range(-swimLimits.z, swimLimits.z));
            allFish[i] = (GameObject)Instantiate(fishPrefab, pos, Quaternion.identity);
            allFish[i].GetComponent<Flocking>().myManager = this;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(Random.Range(0,100000) < 50)
        {
            Vector3 goalPos = transform.position + new Vector3(Random.Range(-swimLimits.x, swimLimits.x),
                                                                  Random.Range(-swimLimits.y, swimLimits.y),
                                                                  Random.Range(-swimLimits.z, swimLimits.z));
            goalPrefab.transform.position = goalPos;
        }
    }
}