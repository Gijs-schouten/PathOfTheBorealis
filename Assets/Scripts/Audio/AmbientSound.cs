using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum AmbientState
{
    ForestOutdoor,
    ForestIndoor,
    Underwater,
    Beach,
    Jungle
}

public class AmbientSound : MonoBehaviour
{
    [SerializeField]
    private AmbientState State;

    [SerializeField]
    private List<AudioClip> ForestOutdoor, ForestIndoor, Underwater, Beach, Jungle;

    private AudioSource MainSource;

    private void Awake()
    {
        MainSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (!MainSource.isPlaying)
            MainSource.PlayOneShot(GetClip());
    }

    private AudioClip GetClip()
    {
        int rand;
        switch (State)
        {
            case AmbientState.ForestOutdoor:

                rand = Random.Range(0, ForestOutdoor.Count);

                return ForestOutdoor[rand];
            case AmbientState.ForestIndoor:

                rand = Random.Range(0, ForestIndoor.Count);

                return ForestIndoor[rand];
            case AmbientState.Underwater:

                rand = Random.Range(0, Underwater.Count);

                return Underwater[rand];
            case AmbientState.Beach:

                rand = Random.Range(0, Beach.Count);

                return Beach[rand];
            case AmbientState.Jungle:

                rand = Random.Range(0, Jungle.Count);

                return Jungle[rand];
            default:
                return null;
        }
    }
}
