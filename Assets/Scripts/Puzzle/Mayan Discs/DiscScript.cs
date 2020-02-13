using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscScript : MonoBehaviour
{
    [SerializeField] private MayanControlPanel ControlPanelScript;

    [Header("Rotation Values")]
    [SerializeField] private float ForwardRotation;
    [SerializeField] private float BackwardRotation;
    [Space]
    [Range(0, 2)]
    [SerializeField] private float RotationSpeed;
    [Space]
    [SerializeField] private float CorrectAngle;

    [Header("Linked Disc")]
    [SerializeField] private DiscScript LinkedDisc;
    [SerializeField] private bool LinkedInvertedRotation;

    private Vector3 CurrentAngle;
    private Vector3 TargetAngle;

    private float RotateAngle;

    private bool CanRotate = true;

    [HideInInspector] public bool IsCorrect;

    [Header("Super lelijke hardcode oplossing")]
    [SerializeField] private bool isOuterRing;

    private void Update()
    {
        if (!isOuterRing)
        {
            if (Mathf.Approximately(transform.eulerAngles.y, CorrectAngle))
            {
                //Debug.Log(gameObject + " staat goed");
                IsCorrect = true;
            }
            else
            {
                //Debug.Log(gameObject + " staat NIET goed");
                IsCorrect = false;
            }
        }
        else
        {
            //Holy shit dit is echt een fucking lelijke hardcode wtf maar het is zodat deze puzzel werkt oke
            if (transform.eulerAngles.y == 100 || transform.eulerAngles.y == CorrectAngle)
            {
                //Debug.Log(gameObject + " staat goed");
                IsCorrect = true;
            }
            else
            {
                //Debug.Log(gameObject + " staat NIET goed");
                IsCorrect = false;
            }
        }
    }

    public void RotateDisc(bool rotateForward, bool rotateLinked)
    {
        if (CanRotate)
        {
            CurrentAngle = transform.eulerAngles;

            if (rotateForward)
            {
                TargetAngle = new Vector3(0, transform.eulerAngles.y + ForwardRotation, 0);

                if(LinkedDisc != null && rotateLinked)
                {
                    if (LinkedInvertedRotation)
                        LinkedDisc.RotateDisc(false, false);
                    else
                        LinkedDisc.RotateDisc(true, false);
                }
            }
            else
            {
                TargetAngle = new Vector3(0, transform.eulerAngles.y - BackwardRotation, 0);

                if(LinkedDisc != null && rotateLinked)
                {
                    if (LinkedInvertedRotation)
                        LinkedDisc.RotateDisc(true, false);
                    else
                        LinkedDisc.RotateDisc(false, false);
                }
            }

            StartCoroutine(RotationCooldown());
        }
    }

    private IEnumerator RotationCooldown()
    {
        float timer = 0;
        float maxTime = 1;

        CanRotate = false;

        while (true)
        {
            if (timer >= maxTime)
                break;

            ControlPanelScript.IsRotating = true;

            transform.eulerAngles = new Vector3(0, (int)Mathf.Lerp(CurrentAngle.y, TargetAngle.y, timer), 0);
            timer += RotationSpeed * Time.deltaTime;

            yield return null;
        }

        transform.eulerAngles = TargetAngle;
        ControlPanelScript.IsRotating = false;
        CanRotate = true;
    }
}