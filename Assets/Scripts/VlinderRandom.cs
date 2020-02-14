using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VlinderRandom : MonoBehaviour
{
    private Animator anim;
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        anim.speed = Random.Range(1, 1.3f);

    }
}
