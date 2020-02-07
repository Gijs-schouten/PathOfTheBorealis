using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingSceneScript : MonoBehaviour
{
    [Header("Loading Objects")]
    [SerializeField] private GameObject LoadingParticles;

    private void Update()
    {
        //Dit is een debuggie
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    EnableNormalLoading();
        //}
    }

    /// <summary>
    /// Dit zorgt voor een zwart scherm met het woord "LOADING" in plaats van de laad-particles.
    /// </summary>
    private void EnableNormalLoading()
    {
        LoadingParticles.SetActive(false);
        PlayerCanvasScript.Instance.EnableLoadscreen();
    }

    /// <summary>
    /// Hiermee krijg je de standaard laadparticles.
    /// </summary>
    private void DisableNormalLoading()
    {
        LoadingParticles.SetActive(true);
        PlayerCanvasScript.Instance.DisableLoadscreen();
    }
}
