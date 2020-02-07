using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{

    Animator anim;
    bool mayFly;
    public float speed;
    public float sittingSec;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(Startflying());
    }

    // Update is called once per frame
    void Update()
    {
        if (mayFly)
        {
            transform.position += transform.forward * Time.deltaTime * speed;
        }


    }

    IEnumerator Startflying()
    {
        //Debug.Log(anim.GetCurrentAnimatorStateInfo(0).length);
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length * sittingSec);
        anim.SetBool("flying", true);
        mayFly = true;
    }
}
