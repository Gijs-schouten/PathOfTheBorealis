using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class GateManager : MonoBehaviour
{
    [SerializeField]
    private List<GlyphPiece> CombinationList;
    [SerializeField]
    private List<GlyphPad> GlyphPads;
    [SerializeField]
    private List<CircularDrive> DoorInteractable;

    private void Start()
    {
        SwitchInteractableState(false);
    }

    public void OnLeverPull(CircularDrive circularDrive)
    {
        StartCoroutine(ReturnDrive(circularDrive, Vector3.zero, CircularDrive.Axis_t.XAxis));
        if(CheckCombination() == true)
        {
            //Play Audio que
            SwitchInteractableState(true);
        }
        else
        {
            //Play audio que failure
        }
    }

    public void OpenDoorLeft(CircularDrive circularDrive)
    {
        StartCoroutine(ReturnDrive(circularDrive, new Vector3(0, 15, 0), CircularDrive.Axis_t.YAxis));
    }
    public void OpenDoorRight(CircularDrive circularDrive)
    {
        StartCoroutine(ReturnDrive(circularDrive, new Vector3(0, 75, 0), CircularDrive.Axis_t.YAxis));
    }

    public void SwitchInteractableState(bool enabled)
    {
        for (int i = 0; i < DoorInteractable.Count; i++)
        {
            //DoorInteractable[i].updating = enabled;
        }
    }

    private IEnumerator ReturnDrive(CircularDrive circularDrive, Vector3 desiredAngle, CircularDrive.Axis_t angleChanged)
    {
        Vector3 currentAngle = circularDrive.transform.localEulerAngles;

        float timer = 0.0f;
        float time = 1;

        while (true)
        {

            circularDrive.transform.localEulerAngles = Vector3.Lerp(currentAngle, desiredAngle, timer);
            circularDrive.linearMapping.value = time - timer;

            switch (angleChanged)
            {
                case CircularDrive.Axis_t.XAxis:
                    circularDrive.outAngle = circularDrive.transform.localEulerAngles.x;
                    break;
                case CircularDrive.Axis_t.YAxis:
                    circularDrive.outAngle = circularDrive.transform.localEulerAngles.y;
                    break;
                case CircularDrive.Axis_t.ZAxis:
                    circularDrive.outAngle = circularDrive.transform.localEulerAngles.z;
                    break;
            }

            if (timer > time)
            {
                break;
            }

            timer += 1 * Time.deltaTime;
            yield return null;
        }
    }

    private bool CheckCombination()
    {
        for (int i = 0; i < GlyphPads.Count; i++)
        {
            if (GlyphPads[i].GlyphEnum != CombinationList[i])
                return false;
        }

        return true;
    }
}
