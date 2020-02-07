using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegisterItem : MonoBehaviour
{
    [SerializeField] private GameObject item;
    [SerializeField] private PuzzleAnimalController pac;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == item)
        {
            pac.hasObjective = true;
        }
    }
}
