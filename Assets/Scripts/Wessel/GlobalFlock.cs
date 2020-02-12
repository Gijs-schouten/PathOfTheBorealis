using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalFlock : MonoBehaviour
{
    public GameObject fishPrefab;
    public static int tankSize = 5;
    public GameObject goalPrefab;
   public  static int numFish = 50;
    public static GameObject[] allFish = new GameObject[numFish];

    public static Vector3 goalPos;
    // Start is called before the first frame update
    void Start()
    {
        goalPos = transform.position;
        for(int i = 0; i < numFish; i++)
        {
            Vector3 pos = new Vector3(Random.Range(-tankSize, tankSize),
                                                       Random.Range(-tankSize, tankSize),
                                                        Random.Range(-tankSize, tankSize));
            allFish[i] = (GameObject)Instantiate(fishPrefab, transform.position + pos, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Random.Range(0, 10000) < 50)
        {
            goalPos = new Vector3(Random.Range(-tankSize + transform.position.x, tankSize + transform.position.x),
                                                   Random.Range(-tankSize + transform.position.y, tankSize + transform.position.y),
                                                    Random.Range(-tankSize + transform.position.z, tankSize + transform.position.z));
          //  Debug.Log(goalPos);
        }
    }
}
