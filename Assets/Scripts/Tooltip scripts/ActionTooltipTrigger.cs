using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class ActionTooltipTrigger : MonoBehaviour
{
    //Tooltip bedoeld voor interacties met objecten.
    //Vanzelfsprekend.

    [SerializeField] private GameObject ActionTooltip;

    private void Awake()
    {
        ActionTooltip?.gameObject.SetActive(false);
    }

    private void ActivateTooltip()
    {
        ActionTooltip.gameObject.SetActive(true);
    }

    public void DeactivateTooltip()
    {
        ActionTooltip.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
            ActivateTooltip();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player")
            DeactivateTooltip();
    }

}
