using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncPos : MonoBehaviour
{

    public Transform PosToSync;

    private void Update()
    {
        transform.position = PosToSync.position;
        transform.rotation = PosToSync.rotation;
        transform.localScale = PosToSync.localScale;
    }

}
