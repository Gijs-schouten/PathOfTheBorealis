using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MayanHeadScript : MonoBehaviour
{
    [SerializeField] private GameObject Mouth;
    [SerializeField] private GameObject MouthTarget;

    [HideInInspector] public bool CanOpen;

    private void FixedUpdate()
    {
        if(CanOpen && Mouth.transform.position != MouthTarget.transform.position)
            Mouth.transform.position = Vector3.MoveTowards(Mouth.transform.position, MouthTarget.transform.position, 0.5f * Time.fixedDeltaTime);
    }
}