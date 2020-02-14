using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]

public class RandomFlyer : MonoBehaviour
{
    [Header("Bird behaviour en animatie")]
    [Space(10)]
    [SerializeField] float idleSpeed, turnSpeed, switchSeconds, idleRatio;
    [SerializeField] Vector2 animSpeedMinMax, moveSpeedMinMax, changeAnimEveryFromTo, changeTargetEveryFromTo;
    [Header("Targets")]
    [SerializeField] Transform homeTarget, flyingTarget;
    [Header("Range Settings")]
    [SerializeField] Vector2 radiusMinMax;
    [SerializeField] Vector2 yMinMax;
    [Header("Base Settings")]
    [SerializeField] public bool returnToBase = false;
    [SerializeField] public bool isInsect = false;
    [SerializeField] public float randomBaseOffset = 5, delayStart = 0f;

    private Animator animator;
    private Rigidbody body;
    [System.NonSerialized] public float changeTarget = 0f, changeAnim = 0f, timeSinceTarget = 0f, timeSinceAnim = 0f, prevAnim, currentAnim = 0f, prevSpeed, speed, zturn, prevz, turnSpeedBackup;
    private Vector3 rotateTarget, position, direction, velocity, randomizedBase;
    private Quaternion lookRotation;
    [HideInInspector] public float distanceFromBase = 0f, distanceFromTarget = 0f;

    // Start is called before the first frame update
    void Start()
    {

        //Initialize
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody>();
        turnSpeedBackup = turnSpeed;
        direction = Quaternion.Euler(transform.eulerAngles) * (Vector3.forward);
        if (delayStart < 0f) body.velocity = idleSpeed * direction;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (delayStart > 0f)
        {
            delayStart -= Time.fixedDeltaTime;
            return;
        }

        //Calculate distances
        distanceFromBase = Vector3.Magnitude(randomizedBase - body.position);
        distanceFromTarget = Vector3.Magnitude(flyingTarget.position - body.position);

        //Allow drastic turns close to base to ensure target can be reached
        if (returnToBase && distanceFromBase < 10f)
        {
            if (turnSpeed != 300f && body.velocity.magnitude != 0f)
            {
                turnSpeedBackup = turnSpeed;
                turnSpeed = 300f;
            }
            else if (distanceFromBase <= 1f)
            {
                body.velocity = Vector3.zero;
                turnSpeed = turnSpeedBackup;
                return;
            }
        }

        if (changeAnim < 0f)
        {
            prevAnim = currentAnim;
            currentAnim = ChangeAnim(currentAnim);
            changeAnim = Random.Range(changeAnimEveryFromTo.x, changeAnimEveryFromTo.y);
            timeSinceAnim = 0f;
            prevSpeed = speed;
            if (currentAnim == 0) speed = idleSpeed;
            else speed = Mathf.Lerp(moveSpeedMinMax.x, moveSpeedMinMax.y,(currentAnim - animSpeedMinMax.x)/(animSpeedMinMax.y - animSpeedMinMax.x));
        }

        if (changeTarget < 0f)
        {
            rotateTarget = ChangeDirection(body.transform.position);
            if (returnToBase) changeTarget = 0.2f; else changeTarget = Random.Range(changeTargetEveryFromTo.x, changeTargetEveryFromTo.y);
            timeSinceTarget = 0f;
        }

        if (body.transform.position.y < yMinMax.x + 10f || body.transform.position.y > yMinMax.y -10f)
        {
            if (body.transform.position.y < yMinMax.x + 10f) rotateTarget.y = 1f; else rotateTarget.y = -1f;

        }

        zturn = Mathf.Clamp(Vector3.SignedAngle(rotateTarget, direction, Vector3.up), -45f , 45f);
        changeAnim -= Time.fixedDeltaTime;
        changeTarget -= Time.fixedDeltaTime;
        timeSinceTarget += Time.fixedDeltaTime;
        timeSinceAnim += Time.fixedDeltaTime;

        //Rotate towards target

        if (rotateTarget != Vector3.zero) lookRotation = Quaternion.LookRotation(rotateTarget, Vector3.up);
        Vector3 rotation = Quaternion.RotateTowards(body.transform.rotation, lookRotation, turnSpeed * Time.fixedDeltaTime).eulerAngles;
        body.transform.eulerAngles = rotation;

        float temp = prevz;
        if (prevz < zturn) prevz += Mathf.Min(turnSpeed * Time.fixedDeltaTime, zturn - prevz);
            else if (prevz >= zturn) prevz -= Mathf.Min(turnSpeed * Time.fixedDeltaTime, prevz - zturn);
        prevz = Mathf.Clamp(prevz, -45f, 45f);

        body.transform.Rotate(0f,0f, prevz-temp, Space.Self);
        //Move Flyer
        direction = Quaternion.Euler(transform.eulerAngles) * Vector3.forward;
        if (returnToBase && distanceFromBase < idleSpeed)
        {
            body.velocity = Mathf.Min(idleSpeed, distanceFromBase) * direction;
        }
        else
        {

        }
        body.velocity = Mathf.Lerp(prevSpeed, speed, Mathf.Clamp(timeSinceAnim / switchSeconds, 0f, 1f)) * direction;

        ClampPosition();

    }

    private void ClampPosition()
    {

        if (body.transform.position.y < yMinMax.x || body.transform.position.y > yMinMax.y)
        {
            position = body.transform.position;
            position.y = Mathf.Clamp(position.y, yMinMax.x, yMinMax.y);
            body.transform.position = position;
        }
    }

    private float ChangeAnim(float currentAnim)
    {
        float newState;
        if (Random.Range(0f, 1f) < idleRatio) newState = 0f; else
        {
            newState = Random.Range(animSpeedMinMax.x, animSpeedMinMax.y);
        }
        if (newState != currentAnim)
        {
            animator.SetFloat("flySpeed", newState);
            if (newState == 0) animator.speed = 1f; else animator.speed = newState;
        }
        return newState;
    }

    private Vector3 ChangeDirection(Vector3 currentPosition)
    {
        Vector3 newDir;
        if (returnToBase)
        {
            randomizedBase = homeTarget.position;
            randomizedBase.y = Random.Range(-randomBaseOffset, randomBaseOffset);
            newDir = randomizedBase - currentPosition; 
        }
        else if (distanceFromTarget > radiusMinMax.y)
        {

            newDir = flyingTarget.position - currentPosition;
        }
        else if (distanceFromTarget < radiusMinMax.x)
        {
            newDir = currentPosition - flyingTarget.position;
        }
        else
        {
            float angleXZ = Random.Range(-Mathf.PI, Mathf.PI);
            float angleY = Random.Range(-Mathf.PI / 48f, Mathf.PI / 48f);

            newDir = Mathf.Sin(angleXZ) * Vector3.forward + Mathf.Cos(angleXZ) * Vector3.right + Mathf.Sin(angleY) * Vector3.up;
        }
        return newDir;
    }
    void OnParticleCollision(GameObject other)
    {
        body.useGravity = true;
        Destroy(transform.gameObject.GetComponent<Collider>());
        Destroy(this);
        Destroy(gameObject,3.0f);
    }
}
