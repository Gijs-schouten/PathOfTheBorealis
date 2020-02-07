using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class CamPostProcessing : MonoBehaviour
{
    public static CamPostProcessing Instance;
    private PostProcessVolume Volume;
    private PlayerStatemachine Player;

    public PostProcessProfile WalkingProfile;
    public PostProcessProfile UnderwaterProfile;


    private void Awake()
    {
        Instance = this;
        Volume = GetComponent<PostProcessVolume>();
        Player = transform.root.GetComponent<PlayerStatemachine>();
    }


    public void EnterWater()
    {
        Volume.profile = UnderwaterProfile;
    }
    public void ExitWater()
    {
        Volume.profile = WalkingProfile;
    }
}
