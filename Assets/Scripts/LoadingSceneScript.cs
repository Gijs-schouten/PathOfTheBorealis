using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingSceneScript : MonoBehaviour
{
    [Header("Loading Objects")]
    [SerializeField] private GameObject LoadingParticles;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            EnableNormalLoading();
        }
    }

    private void EnableNormalLoading()
    {
        LoadingParticles.SetActive(false);
        PlayerCanvasScript.Instance.EnableLoadscreen();
    }

    private void DisableNormalLoading()
    {
        LoadingParticles.SetActive(true);
        PlayerCanvasScript.Instance.DisableLoadscreen();
    }
}
