using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCanvasScript : MonoBehaviour
{
    public static PlayerCanvasScript Instance;

    [Header("Screen flash")]
    [SerializeField] private Image FlashImage;

    [Header("Loading screen")]
    [SerializeField] private GameObject LoadingscreenObject;

    private Animator FlashAnimator;

    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else
            Destroy(gameObject);
        
        if (LoadingscreenObject.activeSelf)
            LoadingscreenObject.SetActive(false);
    }

    private void Start()
    {
        FlashAnimator = FlashImage.GetComponent<Animator>();
    }

    public void ActivateFlash()
    {
        FlashAnimator?.SetBool("FlashActive", true);
    }

    public void StopFlash()
    {
        FlashAnimator?.SetBool("FlashActive", false);
    }

    public void EnableLoadscreen()
    {
        LoadingscreenObject.SetActive(true);
    }

    public void DisableLoadscreen()
    {
        LoadingscreenObject.SetActive(false);
    }
}
