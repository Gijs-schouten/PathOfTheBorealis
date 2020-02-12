using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogController : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private float alertRange;
    [SerializeField] private GameObject target;

    private bool didSpring = false;

    void Update()
    {
        WhenInRange();
    }

    private void WhenInRange()
    {
        if (GetDistance() < alertRange)
        {
            //Debug if neccesary
            //Debug.Log("Is In Range");

            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Jump") && didSpring == false)
            {
                anim.Play("Jump");
                Destroy(gameObject,0.8f);
                didSpring = true;
            }
        }
    }

    private float GetDistance()
    {
        float dist = Vector3.Distance(target.transform.position, transform.position);
        return dist;
    }
}
