using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class PaddleEvents : MonoBehaviour
{
    private GameObject _OriginalPos;


    private void Awake()
    {
        _OriginalPos = new GameObject();
        _OriginalPos.transform.SetParent(transform.parent);
        _OriginalPos.transform.position = transform.position;
        _OriginalPos.transform.rotation = transform.rotation;
    }

    public void DetatchFromHand()
    {
        transform.position = _OriginalPos.transform.position;
        transform.rotation = _OriginalPos.transform.rotation;
        transform.SetParent(_OriginalPos.transform.parent);
    }
}
