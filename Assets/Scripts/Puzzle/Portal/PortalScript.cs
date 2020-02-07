using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class PortalScript : MonoBehaviour
{
    [SerializeField] private BoxCollider BlockCollider;

    [SerializeField] private Texture SecondEmissionMap, ThirdEmissionMap, FourthEmissionMap, FifthEmissionMap;

    [Space(10)]

    [SerializeField] private GameObject PortalInterior;

    private MeshRenderer Renderer;

    private int StoneIndex;

    private void Awake()
    {
        Renderer = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        //Haha hardcoden YEAH
        if (StoneIndex == 0 || StoneIndex == 1)
            BlockCollider.enabled = true;
        else
            BlockCollider.enabled = false;

        if (StoneIndex == 2)
            Renderer.material.SetTexture("_EmissionMap", SecondEmissionMap);
        if (StoneIndex == 3)
            Renderer.material.SetTexture("_EmissionMap", ThirdEmissionMap);
        if (StoneIndex == 4)
            Renderer.material.SetTexture("_EmissionMap", FourthEmissionMap);
        if (StoneIndex == 5)
            Renderer.material.SetTexture("_EmissionMap", FifthEmissionMap);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == GameObject.Find("Player"))
        {
            //Start het laden als je de juiste steen hebt.
            if(StoneIndex != 0 && StoneIndex != 1)
                LoadingManager.Instance.LoadNewScene(StoneIndex);
        }

        PortalStoneScript _portalStone = other.GetComponent<PortalStoneScript>();

        if (_portalStone != null)
            StoneIndex = _portalStone.StoneNumber;
    }
}
