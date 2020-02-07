using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class Paddle : MonoBehaviour
{
    #region References
    private PaddleManager Manager;
    private Hand ClosestHand;
    private Hand FurthestHand;
    #endregion

    #region Variables
    public Vector3 WaterEnterPos;
    public Vector3 Distance;
    private float PaddleTime;

    [HideInInspector] public float Force;
    [HideInInspector] public bool InWater;
    private bool Blocked;
    #endregion

    private void Awake()
    {
        Manager = transform.root.GetComponent<PaddleManager>();
    }

    private void Update()
    {
        if(ClosestHand != null && FurthestHand != null)
        {
            float oceanHeight = Manager.Water.GetWaterHeightAtLocation(transform.position.x, transform.position.z);
            float height = transform.position.y;

            if (height > oceanHeight)
            {
                Blocked = false;
                InWater = false;
                PaddleTime = 0;
                Force = 0;
                Distance = Vector3.zero;
            }
            if (Blocked)
                return;

            if (height < oceanHeight && !InWater)
            {
                InWater = true;
                WaterEnterPos = Manager.transform.InverseTransformPoint(transform.position);
            }
            else if (height <= oceanHeight && InWater)
            {
                PaddleTime += 1 * Time.deltaTime;
                Distance = Manager.transform.InverseTransformPoint(transform.position) - WaterEnterPos;

                Force = Distance.magnitude / PaddleTime;

                if (PaddleTime > 4)
                    Blocked = true;
            }
        }
    }

    public void GetClosestHand()
    {
        Vector3 distLeftHand = transform.position - Manager.LeftHand.transform.position;
        Vector3 distRightHand = transform.position - Manager.RightHand.transform.position;

        if (distLeftHand.magnitude > distRightHand.magnitude)
        {
            ClosestHand = Manager.RightHand;
            FurthestHand = Manager.LeftHand;
        }
        else
        {
            ClosestHand = Manager.LeftHand;
            FurthestHand = Manager.RightHand;
        }
    }
}
