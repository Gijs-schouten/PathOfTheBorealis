using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotemScript : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private Transform CrystalPivot;
    [SerializeField] private GameObject CrystalObject;

    [Header("Connected Objects")]
    [SerializeField] private MayanControlPanel MayanPanelScript;

    private bool CrystalInserted;

    private void Update()
    {
        if (CrystalInserted)
        {
            CrystalObject.transform.position = new Vector3(CrystalPivot.position.x, CrystalPivot.position.y, CrystalPivot.position.z);
            CrystalObject.gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == CrystalObject && !CrystalInserted)
        {
            CrystalInserted = true;
            ActivateTotem();
        }
    }

    private void ActivateTotem()
    {
        if (MayanPanelScript != null)
            MayanPanelScript.TotemPoleActivated = true;
    }
}
