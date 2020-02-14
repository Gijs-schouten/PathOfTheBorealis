using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;


public class FlashLightBlend : MonoBehaviour
{
    SteamVR_Skeleton_Poser poser;
    public string test;
       private void Start()
    {
        poser = GetComponent<SteamVR_Skeleton_Poser>();
        float test = poser.GetBlendingBehaviourValue("FlashLight");

    }

    void Update()
    {
        poser.SetBlendingBehaviourValue("FlashLight", Mathf.Sin(Time.time * 10) / 2 + 0.5f);
    
    
    }
}
