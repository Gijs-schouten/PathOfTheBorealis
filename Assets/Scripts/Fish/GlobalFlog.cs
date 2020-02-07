using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalFlog : MonoBehaviour
{
    public GameObject fishPrefab;

    public Transform[] positions;

    static int numberFish = 20;
    public static GameObject[] allFish = new GameObject[numberFish];

    public static GameObject goalStart;
    public static Vector3 start;
    public static Vector3 goalPos; 

    // Start is called before the first frame update
    void Start()
    {
        goalStart = GameObject.Find("goalStart");
        start = new Vector3(goalStart.transform.position.x, goalStart.transform.position.y, goalStart.transform.position.z);
        goalPos = new Vector3(goalStart.transform.position.x, goalStart.transform.position.y, goalStart.transform.position.z);

        for (int i = 0; i < numberFish; i++)
        {
            Vector3 pos = positions[0].transform.position;

           allFish[i] = (GameObject)Instantiate(fishPrefab, pos, Quaternion.identity);

        }

    }

    // Update is called once per frame
    void Update()
    {
     
        if(Random.Range(0,10000) < 50)
        {
            goalPos = positions[0].transform.position;

            Vector3 currentPos = goalPos;
            int j = Random.Range(0, positions.Length);
            goalPos = positions[j].transform.position;
            if(currentPos == goalPos)
            {
                if(j > positions.Length)
                {
                    j = 0;
                }
                goalPos = positions[j + 1].transform.position;
            }
        }
    }
}
