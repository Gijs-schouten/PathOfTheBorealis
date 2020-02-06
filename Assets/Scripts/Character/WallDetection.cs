using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDetection : MonoBehaviour
{
    private PlayerStatemachine StateMachine;
    public Vector3 Point;
    public bool Checking;
    private Transform CameraPos;
    private Vector3 RefVelocity;
    private MeshCollider MeshColl;
    private CheckCollision CheckScript;
    [SerializeField] private LayerMask HitMask;


    private void Start()
    {
        StateMachine = transform.root.GetComponent<PlayerStatemachine>();
        CameraPos = Camera.main.transform;
        MeshColl = GetComponentInChildren<MeshCollider>();
        CheckScript = GetComponentInChildren<CheckCollision>();
    }

    private void Update()
    {
        transform.position = CameraPos.position + new Vector3(CameraPos.forward.x * .5f, .3f, CameraPos.forward.z * .5f);
        transform.rotation = Quaternion.identity;

        if (Point != Vector3.zero)
        {
            Checking = false;
            MeshColl.gameObject.SetActive(true);

            Vector3 center = StateMachine.CharController.center; center.y = 0;
            Vector3 pos = Point + new Vector3(CameraPos.forward.x * (MeshColl.transform.localScale.x / 2), MeshColl.transform.localScale.y * 1.02f, CameraPos.forward.z * (MeshColl.transform.localScale.z / 2));
            pos += (center * .5f);
            MeshColl.transform.position = pos;

            if (!CheckScript.Collided)
            {
                Vector3 offset = pos - center; offset.y -= StateMachine.CharController.height / 2;
                if ((StateMachine.CharController.transform.position - offset).magnitude > 0.3f)
                {
                    StateMachine.CharController.enabled = false;
                    StateMachine.transform.position = Vector3.SmoothDamp(StateMachine.transform.position, offset, ref RefVelocity, 1f);
                }
                else
                    Clear();
            }
            else
                Clear();
        }
        else
            MeshColl.gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        if(Checking)
        {
            RaycastHit HitWall;
            Ray wallCheckRay = new Ray(transform.position, Vector3.down);
            float dist = transform.position.y - StateMachine.transform.position.y;
            if(Physics.Raycast(wallCheckRay, out HitWall, dist, HitMask))
            {
                if (HitWall.normal == Vector3.up)
                {
                    Point = HitWall.point;
                }
            }
        }
    }

    private void Clear()
    {
        StateMachine.CharController.enabled = true;
        Point = Vector3.zero;
        Checking = false;
        MeshColl.transform.localPosition = Vector3.zero;
    }
   
}