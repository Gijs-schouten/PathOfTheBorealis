using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using Valve.VR;

public class Phonecall : MonoBehaviour
{

    private SteamVR_Skeleton_Poser Poser;

    [SerializeField]
    private AudioSource VibratingPhone, FirstPhonecall, PickupSound;
    [SerializeField]
    private MeshRenderer ScreenRenderer;
    [SerializeField]
    private Material Off, Ringing, Calling, EndCall;

    private Interactable Interact;

    private SteamVR_Action_Vibration hapticAction = SteamVR_Input.GetAction<SteamVR_Action_Vibration>("Haptic");

    private void Awake()
    {
        Poser = GetComponent<SteamVR_Skeleton_Poser>();
        Interact = GetComponent<Interactable>();
    }

    private void Update()
    {
        if (VibratingPhone.isPlaying)
        {
            if (Interact.attachedToHand != null)
                hapticAction.Execute(0, 1, 100, .3f, Interact.attachedToHand.handType);
            if(Poser.GetBlendingBehaviourValue("PickupPhone") >= .9f)
                DialogueManager.Instance.ContinueDialogue();
        }
        
    }

    public void Ring()
    {
        VibratingPhone.Play();
        UpdateMat(Ringing);
    }

    public void PhoneCall()
    {
        UpdateMat(Calling);
        PickupSound.Play();
        VibratingPhone.Stop();
        StartCoroutine(KockAfterAudio()); 
    }

    private void UpdateMat(Material mat)
    {
        var materials = ScreenRenderer.sharedMaterials;
        materials[2] = mat;
        ScreenRenderer.sharedMaterials = materials;
    }

    private IEnumerator KockAfterAudio()
    {
        FirstPhonecall.Play();
        yield return new WaitUntil(() => FirstPhonecall.isPlaying == false);
        DialogueManager.Instance.ContinueDialogue();
        UpdateMat(EndCall);
    }
}
    