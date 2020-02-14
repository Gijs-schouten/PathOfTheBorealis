using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class AiTrigger : MonoBehaviour
{
    private float dis;
    public float maxDis = 15;
    public Transform player;
    public PlayableDirector playableDirector;
    public Animation anim;

    void Start()
    {
        anim = GetComponent<Animation>();
    }

    void Update()
    {
        dis = Vector3.Distance (player.transform.position, transform.position);
        if (dis <= maxDis)
        {
            Debug.Log("shart");
            playableDirector.Play();
            anim.Play("swim");
        }
    }
}
