using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipTrigger : MonoBehaviour
{
    //Maak een leeg gameobject met een collider (isTrigger = true).
    //Zet dit script op het lege object.

    [Tooltip("De tooltip die verschijnt wanneer hij geactiveerd wordt.")]
    [SerializeField] private Floatie Tooltip;

    private bool IsActivated;

    private void Awake()
    {
        if(Tooltip.gameObject.activeSelf)
            Tooltip.gameObject.SetActive(false);
    }

    private void ActivateTooltip()
    {
        Tooltip.gameObject.SetActive(true);
        IsActivated = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Wanneer de player in de trigger staat gaat de tooltip aan.
        if (other.gameObject.name == "Player")
        {
            if (!IsActivated)
                ActivateTooltip();
        }
    }
}