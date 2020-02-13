using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public enum Direction
{
    Left,
    Right,
    Up,
    Down
}

public class FuseScript : MonoBehaviour
{
    [SerializeField] private FuseBoxManager FuseManagerScript;

    [Header("Fuse Properties")]
    public bool IsActivated;
    [Space]
    public bool IsStart;
    public bool IsRequired;
    public bool IsEnding;

    [Tooltip("When checked, the fuse cannot be twisted.")]
    public bool StaticFuse;

    [Header("Surrounding fuses")]
    [SerializeField] private FuseScript LeftFuse;
    [SerializeField] private FuseScript RightFuse;
    [SerializeField] private FuseScript UpFuse;
    [SerializeField] private FuseScript DownFuse;

    [Header("Effects")]
    [SerializeField] private GameObject Laser;
    [SerializeField] private GameObject Light;
    [SerializeField] private Renderer LightRenderer;

    [HideInInspector] public FuseScript PreviousFuse;

    private Vector3 OriginalDirection;
    private Vector3 CurrentDirection;

    private CircularDrive m_CircularDrive;

    private BoxCollider FuseColl;

    [HideInInspector] public bool Turnable;

    void Awake()
    {
        LightRenderer = Light.GetComponent<Renderer>();
        m_CircularDrive = GetComponent<CircularDrive>();
        FuseColl = GetComponent<BoxCollider>();

        if (!IsEnding)
        {
            if (IsActivated)
                Activate();
            else
                Deactivate();
        }

        CurrentDirection = transform.eulerAngles;
        OriginalDirection = CurrentDirection;

        Laser.SetActive(false);

        //if (StaticFuse)
        //    m_CircularDrive.enabled = false;
        //else
        //    m_CircularDrive.enabled = true;

        Turnable = true;
    }

    void Update()
    {
        if (!FuseManagerScript.FuseMissing)
        {
            if (IsActivated)
            {
                if (transform.eulerAngles.z > 0 && transform.eulerAngles.z <= 90)
                {
                    ChangeDirection();

                    if (LeftFuse != null)
                    {
                        LeftFuse.Activate();
                        Laser.transform.eulerAngles = new Vector3(-90, 90, 0);
                        Laser.SetActive(true);
                    }
                    else
                    {
                        Laser.SetActive(false);
                    }

                    PreviousFuse = LeftFuse;
                }
                if (transform.eulerAngles.z > 90 && transform.eulerAngles.z <= 180)
                {
                    ChangeDirection();

                    if (DownFuse != null)
                    {
                        DownFuse.Activate();
                        Laser.transform.eulerAngles = new Vector3(180, 90, 0);
                        Laser.SetActive(true);
                    }
                    else
                    {
                        Laser.SetActive(false);
                    }

                    PreviousFuse = DownFuse;
                }
                if (transform.eulerAngles.z > 180 && transform.eulerAngles.z <= 270)
                {
                    ChangeDirection();

                    if (RightFuse != null)
                    {
                        RightFuse.Activate();
                        Laser.transform.eulerAngles = new Vector3(90, 90, 0);
                        Laser.SetActive(true);
                    }
                    else
                    {
                        Laser.SetActive(false);
                    }

                    PreviousFuse = RightFuse;
                }
                if (transform.eulerAngles.z > 270 && transform.eulerAngles.z <= 360)
                {
                    ChangeDirection();

                    if (UpFuse != null)
                    {
                        UpFuse.Activate();
                        Laser.transform.eulerAngles = new Vector3(0, 90, 0);
                        Laser.SetActive(true);
                    }
                    else
                    {
                        Laser.SetActive(false);
                    }

                    PreviousFuse = UpFuse;
                }
            }
            else
            {
                if (PreviousFuse != null && !PreviousFuse.IsStart)
                    PreviousFuse.Deactivate();

                Laser.SetActive(false);
            }

            if (!Turnable)
                FuseColl.enabled = false;
        }
    }

    private void ChangeDirection()
    {
        if (PreviousFuse != null)
            PreviousFuse.Deactivate();
    }

    public void Activate()
    {
        IsActivated = true;

        if(!IsEnding)
            LightRenderer.material.SetColor("_EmissionColor", Color.blue);
    }

    public void Deactivate()
    {
        if (!IsStart)
        {
            IsActivated = false;

            if (!IsEnding)
            {
                if (IsRequired)
                    LightRenderer.material.SetColor("_EmissionColor", Color.yellow);
                else
                    LightRenderer.material.SetColor("_EmissionColor", Color.black);
            }
        }
    }

    public void EnableRequired()
    {
        IsRequired = true;
        LightRenderer.material.SetColor("_EmissionColor", Color.yellow);
    }

    public void EnableEnding()
    {
        IsEnding = true;
        LightRenderer.material.SetColor("_EmissionColor", Color.green);
    }

    public void ResetFuse()
    {
        IsActivated = false;
        Laser.SetActive(false);

        if(!IsEnding)
            Deactivate();

        transform.eulerAngles = OriginalDirection;
    }

    public void Powerless()
    {
        LightRenderer.material.SetColor("_EmissionColor", Color.black);
    }
}