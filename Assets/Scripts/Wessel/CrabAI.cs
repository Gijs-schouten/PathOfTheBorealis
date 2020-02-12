using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class CrabAI : MonoBehaviour
{
    public float randomY;
    public float time;
    public float speed;
    public float endTimer;

    void Start()
    {
        randomY = Random.Range(45, 180);
        transform.Rotate(0, randomY, 0);
        endTimer = Random.Range(2.5f, 5);
    }

    private void Update()
    {     
        transform.position = transform.position + transform.forward * speed * Time.deltaTime;
        time += Time.deltaTime;
        if  (time > endTimer)
        {
            speed = 0;
            StartCoroutine(Fartingoehyea());
        }
      
        IEnumerator Fartingoehyea()
        {
            time = 0;
            yield return new WaitForSecondsRealtime(endTimer);
            randomY = Random.Range(45, 180);
            transform.Rotate(0, randomY, 0);
            speed = 5;
            StopAllCoroutines();
        }

    }
}
