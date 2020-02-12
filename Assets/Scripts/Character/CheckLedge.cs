using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckLedge : MonoBehaviour
{
    Vector3 StandingPoint;
    Vector3 RefVelocity;
    MeshCollider Collider;
    Rigidbody Rigidbody;
    CharacterController Controller;
    GameObject CameraPos;
    

    [HideInInspector] public bool Checking;
    [SerializeField] private float ClimbThresholdOffset = 0.4f;

    private void Awake()
    {
        Collider = GetComponent<MeshCollider>();
        Rigidbody = GetComponent<Rigidbody>();
        Controller = transform.root.GetComponent<CharacterController>();
        CameraPos = Camera.main.gameObject;
    }

    private void Update()
    {
        if(Checking)
        {
            Collider.enabled = true;
        }
        if(transform.position.y < CameraPos.transform.position.y - ClimbThresholdOffset || !Checking)
        {
            Collider.enabled = false;
            transform.position = CameraPos.transform.position + new Vector3(CameraPos.transform.forward.x, .8f, CameraPos.transform.forward.z);
            transform.rotation = Quaternion.identity;

            Checking = false;
        }

        if(StandingPoint != Vector3.zero)
        {
            Vector3 offset = Controller.center; offset.y = -1;
            Vector3 target = StandingPoint - offset;
            if ((Controller.transform.position - target).magnitude > 0.25f)
            {
                Controller.enabled = false;
                Controller.transform.position = Vector3.SmoothDamp(Controller.transform.position, target, ref RefVelocity, 0.5f);
            }
            else
            {
                Controller.enabled = true;
                StandingPoint = Vector3.zero;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint[] contactPoints = new ContactPoint[collision.contactCount];
        collision.GetContacts(contactPoints);

        Vector3 average = new Vector3();
        Vector3 standingPoint = new Vector3();

        for (int i = 0; i < contactPoints.Length; i++)
        {
            standingPoint += contactPoints[i].point;
            average += contactPoints[i].normal;
        }

        average = average /= contactPoints.Length;
        if (average == Vector3.up)
            StandingPoint = standingPoint /= contactPoints.Length;

        Debug.Log(average, collision.gameObject);
    }
}
