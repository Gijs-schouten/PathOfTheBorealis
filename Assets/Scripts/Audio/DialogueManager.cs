using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    private Animator Anim;
    [HideInInspector] public AudioSource AudioPlayer;

    public List<EventFunctions> EventList;

    private void Awake()
    {
        Instance = this;
        Anim = GetComponent<Animator>();
        AudioPlayer = GetComponent<AudioSource>();
        
    }

    public void ContinueDialogue()
    {
        Anim.SetTrigger("Continue");
    }


}

[System.Serializable]
public class EventFunctions : UnityEvent { }