using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorScript : MonoBehaviour
{
    [SerializeField] private Animator HandleAnimator;

    private void Start()
    {
        HandleAnimator = GetComponent<Animator>();
    }

    public void HoldDoorHandle()
    {

    }

    public void ReleaseDoorHandle()
    {

    }
}
