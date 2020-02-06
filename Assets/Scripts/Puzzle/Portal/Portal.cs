using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class Portal : MonoBehaviour
{

    [SerializeField]
    private UnityEvent PortalTrigger, LoadSceneEvent;

    private MeshRenderer Renderer;

    [SerializeField]
    private Texture SecondaryEmissionMap;
    [SerializeField]
    private List<ParticleSystem> WindupParticles, ExplodeParticles;

    private bool Playing;

    [SerializeField] private GameObject PortalInterior;

    private void Awake()
    {
        PortalTrigger.AddListener(ChangeEmissionMap);
        LoadSceneEvent.AddListener(LoadNextScene);
        Renderer = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PortalTrigger.Invoke();
        }

        if(WindupParticles[0].isPlaying)
        {
            Playing = true;
        }
        else if(!WindupParticles[0].isPlaying && Playing == true)
        {
            for (int i = 0; i < ExplodeParticles.Count; i++)
            {
                ExplodeParticles[i].Play();
            }
            Renderer.material.SetTexture("_EmissionMap", SecondaryEmissionMap);
            Playing = false;

            EnablePortalInside();   
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.root.gameObject.CompareTag("Player"))
        {
            PortalTrigger.Invoke();
        }
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void ChangeEmissionMap()
    {
        // play sound

        for (int i = 0; i < WindupParticles.Count; i++)
        {
            WindupParticles[i].Play();
        }
    }
    
    public void EnablePortalInside()
    {
        PortalInterior.SetActive(true);
    }
}
