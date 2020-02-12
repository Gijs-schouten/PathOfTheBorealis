using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ez_rondjes : MonoBehaviour
{
    public float randomPos;
    public float randomNeg;
    public float randomY;
    private float threshHold = 2.5f;
    private Animator anim;

    void Start()
    {
        randomPos = Random.Range(.25f, .5f);
        randomNeg = Random.Range(-.25f, -.5f);
        anim = gameObject.GetComponent<Animator>();
        anim.speed = Random.Range(1, 1.5f);
        if (Random.Range(1, 10) < threshHold)
        {
            randomY = randomNeg;
        }
        else
        {
            randomY = randomPos;
        }
    }

    void Update()
    {
        transform.Rotate(0, randomY, 0 * Time.deltaTime);
        transform.position = transform.position + transform.forward * 1 * Time.deltaTime;


    }
}
