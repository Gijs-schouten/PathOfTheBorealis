using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private GameObject FuelPivot;
    [SerializeField] private GameObject PowerButton;
    [Space]
    [SerializeField] private List<GameObject> CabinLights = new List<GameObject>();
    [SerializeField] FuseBoxManager FuseboxScript;

    [SerializeField] private Jerrycan JerrycanObject;
    private Jerrycan JerrycanObject;
    private Animator GeneratorAnimator;

    private bool CanPressButton;

    [HideInInspector] public bool CanEnablePower;

    void Start()
    {
        GeneratorAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        //Debugje
        if (Input.GetKeyDown(KeyCode.Space))
            ActivatePower();
    }

    public void RecieveFuel()
    {
        CanEnablePower = true;
    }

    public void ActivatePower()
    {
        if (FuseboxScript != null)
        {
            FuseboxScript.HasPower = true;
            FuseboxScript.ResetGame();
        }

        GeneratorAnimator.SetTrigger("IsActivated");
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            ActivatePower();
    }
}
