using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MayanControlPanel : MonoBehaviour
{
    [SerializeField] MayanHeadScript HeadScript;

    [Header("Discs")]
    [SerializeField] private List<DiscScript> DiscList = new List<DiscScript>();
    [SerializeField] private int CurrentDisc;

    [Header("Conditions")]
    public bool TotemPoleActivated;
    public bool WaterWheelActivated;

    [Header("Decoration")]
    [SerializeField] private GameObject BigCrystal;

    private bool PuzzleSolved;

    [HideInInspector] public bool IsRotating;

    private Renderer CrystalRenderer;

    private void Start()
    {
        CrystalRenderer = BigCrystal.GetComponent<Renderer>();
    }

    void Update()
    {
        //Klein debugje zodat ik niet steeds met de VR headset hoef te testen :D
        /*
        if (Input.GetKeyDown(KeyCode.Space))
            RotateRight();
        if (Input.GetKeyDown(KeyCode.C))
            RotateLeft();

        if (Input.GetKeyDown(KeyCode.X))
            SwitchDisc();
        */
        //Einde van klein debugje

        if (DiscList[0].IsCorrect && DiscList[1].IsCorrect && DiscList[2].IsCorrect)
        {
            Debug.Log("Puzzle solved!");

            PuzzleSolved = true;
            HeadScript.CanOpen = true;

            CrystalRenderer.material.SetColor("_EmissionColor", new Color(0.2156863f, 0.9921569f, 0.419269f));
        }
    }

    public void SwitchDisc()
    {
        if(TotemPoleActivated && !PuzzleSolved)
        {
            if (CurrentDisc == DiscList.Count - 1)
                CurrentDisc = 0;
            else
                CurrentDisc += 1;
        }
    }

    public void RotateRight()
    {
        if (TotemPoleActivated && !PuzzleSolved && !IsRotating)
            DiscList[CurrentDisc].RotateDisc(true, true);
    }

    public void RotateLeft()
    {
        if (TotemPoleActivated && !PuzzleSolved && !IsRotating)
            DiscList[CurrentDisc].RotateDisc(false, true);
    }
}
