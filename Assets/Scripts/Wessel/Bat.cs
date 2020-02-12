using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour
{
    public float speed;
    public float up;
    public float time;
    private Animator anim;

    void Start()
    {
        speed = Random.Range(15f, 25);
        up = Random.Range(-.1f, -.25f);
        time = Random.Range(.01f, .3f);
         anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Time.deltaTime >= time){
            anim.SetTrigger("Start");
        }
        transform.position = transform.position + transform.forward * speed * Time.deltaTime;
        transform.Rotate(up, 0, 0 * Time.deltaTime);
    }
}
