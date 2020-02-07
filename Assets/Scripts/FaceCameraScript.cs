using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCameraScript : MonoBehaviour
{
    //Script to make UI elements face the camera at ALL times

    [SerializeField] private Camera MainCam;

    private void Awake()
    {
        //Als je geen camera meegeeft pakt hij de main camera
        if(MainCam == null)
            MainCam = Camera.main;
    }

    private void LateUpdate()
    {
        transform.LookAt(2 * transform.position - MainCam.transform.position);
    }
}