using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZipMove : MonoBehaviour
{
    public Transform endPos;
    public float time = 2f;
    private float elapsedtime;
    private Vector3 startpos;
    public GameObject player;
    public bool canzip;
    public float speed;
    void Start()
    {
           // player = GameObject.Find("HeadCollider");
    }
      void OnTriggerEnter(Collider other)
    {if (other.gameObject.tag == "Player")
        {canzip = true;}}
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        { canzip = false; }
    }

    void Update()
    {
    if (canzip == true && Input.GetKey(KeyCode.Space))
        {
            startpos = player.transform.position;
            player.transform.position = Vector3.Lerp(startpos, endPos.transform.position, (elapsedtime * time));
            elapsedtime += Time.deltaTime;
        }
        speed = elapsedtime / time;
        if (canzip == true && (Input.GetMouseButton(0)))
        {time = .025f;}
        if (canzip == true && (Input.GetMouseButtonUp(0)))
        {time = .05f;}
    }
}

