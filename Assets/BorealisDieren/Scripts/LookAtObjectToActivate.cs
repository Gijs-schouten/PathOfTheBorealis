using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtObjectToActivate : MonoBehaviour
{

    [SerializeField] private GameObject birdNest;
    Renderer m_Renderer;

    void Start()
    {
        m_Renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Renderer.isVisible)
        {
            birdNest.SetActive(true);
        }
    }
}
