using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public enum PlayerSFX
{
    Grab,
    Climb,
}

public class PlayerSoundManager : MonoBehaviour
{
    public static PlayerSoundManager Instance;

    [SerializeField] private AudioSource LeftHandSource;
    [SerializeField] private AudioSource RightHandSource;

    [SerializeField] private List<AudioClip> ClimbSoundEffects;

    private void Awake()
    {
        Instance = this;
    }

    public void PlaySFX(PlayerSFX sfxToPlay, SteamVR_Input_Sources source)
    {
        switch (sfxToPlay)
        {
            case PlayerSFX.Grab:



                break;
            case PlayerSFX.Climb:

                if (source == SteamVR_Input_Sources.LeftHand)
                {
                    int rand = Random.Range(0, ClimbSoundEffects.Count);
                    LeftHandSource.PlayOneShot(ClimbSoundEffects[rand]);
                    return;
                }
                if (source == SteamVR_Input_Sources.RightHand)
                {
                    int rand = Random.Range(0, ClimbSoundEffects.Count);
                    RightHandSource.PlayOneShot(ClimbSoundEffects[rand]);
                    return;
                }

                break;
        }
    }

}
