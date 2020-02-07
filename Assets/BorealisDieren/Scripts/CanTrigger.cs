using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanTrigger : MonoBehaviour
{
    [SerializeField] GameObject ps;

    private void Update()
    {
        SprayCan();
    }
    private void SprayCan()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            //GetComponent<SpiderKillerController>().OnUse();
            ps.SetActive(true);
        }
    }
}
