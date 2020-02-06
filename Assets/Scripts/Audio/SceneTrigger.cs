using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SceneTrigger : MonoBehaviour
{

    [SerializeField]
    private string EventDescription;

    public TriggerEvent Event;


    private void OnTriggerEnter(Collider coll)
    {
        if (coll.transform.CompareTag("Player"))
            Event.Invoke(gameObject);
    }

}

[System.Serializable]
public class TriggerEvent : UnityEvent<GameObject> { }