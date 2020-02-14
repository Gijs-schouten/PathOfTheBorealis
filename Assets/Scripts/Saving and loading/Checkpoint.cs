using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private SaveManager _SaveManager;

    private void Start()
    {
        _SaveManager = SaveManager.Instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.root.CompareTag("Player"))
        {
            _SaveManager.ReachedCheckpoint(this);
        }
    }

}
