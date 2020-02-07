using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class PortalScript : MonoBehaviour
{
    //[SerializeField] private UnityEvent PortalTrigger;

    [SerializeField] private BoxCollider BlockCollider;

    [SerializeField] private Texture SecondEmissionMap, ThirdEmissionMap, FourthEmissionMap, FifthEmissionMap;

    //[SerializeField] private List<ParticleSystem> WindupParticles, ExplodeParticles;

    [Space(10)]

    [SerializeField] private GameObject PortalInterior;

    private MeshRenderer Renderer;

    private int StoneIndex;

    //private bool Playing;

    private void Awake()
    {
        //PortalTrigger.AddListener(ChangeEmissionMap);
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

        //if (WindupParticles[0].isPlaying)
        //{
        //    Playing = true;
        //}
        //else if (!WindupParticles[0].isPlaying && Playing == true)
        //{
        //    for (int i = 0; i < ExplodeParticles.Count; i++)
        //    {
        //        ExplodeParticles[i].Play();
        //    }

        //    Playing = false;

        //    EnablePortalInside();
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.transform.root.gameObject.CompareTag("Player"))
        //{
        //    PortalTrigger.Invoke();
        //}

        if (other.gameObject == GameObject.Find("Player"))
        {
            if(StoneIndex != 0 && StoneIndex != 1)
                LoadingManager.Instance.LoadNewScene(StoneIndex);
        }

        PortalStoneScript _portalStone = other.GetComponent<PortalStoneScript>();

        if (_portalStone != null)
            StoneIndex = _portalStone.StoneNumber;
    }

    //private void ChangeEmissionMap()
    //{
    //    // Speel emission geluidje

    //    for (int i = 0; i < WindupParticles.Count; i++)
    //    {
    //        WindupParticles[i].Play();
    //    }
    //}

    public void EnablePortalInside()
    {
        PortalInterior.SetActive(true);
    }
}

//Hey bruno ik wil even snel zeggen dat je kutcode hebt. oke doei.
