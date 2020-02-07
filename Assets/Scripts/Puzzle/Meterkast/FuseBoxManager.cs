using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class FuseBoxManager : MonoBehaviour
{
    [Header("Special Fuses")]
    [SerializeField] private FuseScript StartingFuse;
    [SerializeField] private FuseScript EndingFuse;
    [Space]
    [SerializeField] private int RequiredGoal;
    [SerializeField] private int ActivatedRequired;
    [Space]
    [SerializeField] private Transform MissingFusePivot;
    [SerializeField] private FuseScript MissingFuse;
    public bool FuseMissing;

    [Header("Fuses")]
    [SerializeField] private List<FuseScript> RequiredFuses = new List<FuseScript>();
    [Space]
    [SerializeField] private List<FuseScript> TotalFuses = new List<FuseScript>();

    [Header("Lights")]
    [SerializeField] private List<GameObject> CabinLights = new List<GameObject>();

    [SerializeField] private LightBulbScript AtticLightScript;

    private bool PuzzleSolved;

    private Rigidbody MissingFuseRb;
    private Throwable MissingFuseThrowable;
    private VelocityEstimator MissingFuseVelocity;

    [HideInInspector] public bool HasPower;

    void Start()
    {
        if (HasPower)
            ResetGame();

        MissingFuseRb = MissingFuse.GetComponent<Rigidbody>();
        MissingFuseThrowable = MissingFuse.GetComponent<Throwable>();
        MissingFuseVelocity = MissingFuse.GetComponent<VelocityEstimator>();
    }


    void Update()
    {
        if (!HasPower)
        {
            foreach (FuseScript Fuse in TotalFuses)
            {
                Fuse.Powerless();
            }
        }

        if (EndingFuse.IsActivated && !PuzzleSolved)
        {
            for (int i = 0; i < RequiredFuses.Count; i++)
            {
                if (RequiredFuses[i].IsActivated)
                    ActivatedRequired += 1;

                if (i == RequiredFuses.Count - 1)
                {
                    if (ActivatedRequired >= RequiredGoal)
                    {
                        LockFuses();

                        Debug.Log("Puzzel gehaald!");
                        PuzzleSolved = true;

                        foreach (GameObject Light in CabinLights)
                        {
                            Light.SetActive(true);
                        }

                        if (AtticLightScript != null)
                            AtticLightScript.HasPower = true;
                        break;
                    }
                    else
                    {
                        Debug.Log("Puzzel gefaald...");
                        ResetGame();
                    }
                }
            }
        }

        if (!FuseMissing)
            MissingFuse.transform.position = MissingFusePivot.position;
    }

    public void ResetGame()
    {
        RequiredGoal = RequiredFuses.Count;

        for (int i = 0; i < TotalFuses.Count; i++)
        {
            TotalFuses[i].ResetFuse();
        }

        StartingFuse.Activate();
        EndingFuse.EnableEnding();

        ActivatedRequired = 0;
    }

    private void LockFuses()
    {
        foreach (FuseScript _fuse in TotalFuses)
        {
            _fuse.Turnable = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == MissingFuse.name && FuseMissing)
        {
            //Remove the object from hand
            Hand[] _hands = FindObjectOfType<Player>().GetComponentsInChildren<Hand>();

            foreach (Hand _hand in _hands)
            {
                if (_hand.ObjectIsAttached(MissingFuse.gameObject))
                {
                    _hand.DetachObject(MissingFuse.gameObject);
                    break;
                }
            }

            Destroy(MissingFuseThrowable);
            Destroy(MissingFuseVelocity);

            MissingFuse.transform.position = MissingFusePivot.position;
            MissingFuse.transform.eulerAngles = new Vector3(0, 0, 0);

            MissingFuseRb.isKinematic = true;
            other.isTrigger = true;
            FuseMissing = false;
        }
    }
}
