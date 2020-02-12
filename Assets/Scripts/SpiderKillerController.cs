using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderKillerController : MonoBehaviour
{
    [SerializeField] private GameObject spraycanButton, ps;
    private ParticleSystem psS;

    public KeyCode code;

    private bool isPressing, didLowerPosition;
    private Vector3 normalPosition;

    private void Start()
    {
        normalPosition = spraycanButton.transform.localPosition;
        psS = ps.GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(code))
        {
            isPressing = true;
        }
        else if (Input.GetKeyUp(code)){
            isPressing = false;
        }

        if (isPressing)
        {
            SprayCan();
        }
        else
        {
            if (psS.isPlaying) psS.Stop();
            spraycanButton.transform.localPosition = new Vector3(spraycanButton.transform.localPosition.x, normalPosition.y, spraycanButton.transform.localPosition.z);
        }
    }

    private void SprayCan()
    {
        if (!psS.isPlaying) psS.Play();
        spraycanButton.transform.localPosition = new Vector3(spraycanButton.transform.localPosition.x, normalPosition.y - 0.002f, spraycanButton.transform.localPosition.z);

        /*RaycastHit hit;

        Physics.Raycast(new Ray(Vector3.zero, Vector3.forward), out hit);

        if (hit.collider.gameObject.GetComponent<PuzzleAnimalControllerNoNavmesh>() != null)
        {
            hit.collider.gameObject.GetComponent<PuzzleAnimalControllerNoNavmesh>().hasObjective = true;
        }*/
    }
}
