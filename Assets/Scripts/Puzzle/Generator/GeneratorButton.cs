using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorButton : MonoBehaviour
{
    private Generator GeneratorScript;

    void Start()
    {
        GeneratorScript = GetComponentInParent<Generator>();
    }

    public void PressButton()
    {
        if (GeneratorScript.CanEnablePower)
            GeneratorScript.ActivatePower();
    }
}