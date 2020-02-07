using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterwheelScript : MonoBehaviour
{
    [Header("Waterwheel values")]
    [SerializeField] private float RotationSpeed;
    [SerializeField] private bool IsStuck;

    [Header("Related Objects")]
    public GameObject KeyObject;
    public GameObject StuckObject;

    [Header("Connected Objects")]
    [SerializeField] private MayanControlPanel MayanPanelScript;
    [SerializeField] private List<GameObject> LaserList = new List<GameObject>();

    private void FixedUpdate()
    {
        if(!IsStuck)
            transform.Rotate(new Vector3(0, 0, RotationSpeed * Time.fixedDeltaTime));
    }

    public void ReleaseWheel()
    {
        IsStuck = false;

        if (MayanPanelScript != null)
            MayanPanelScript.WaterWheelActivated = true;

        foreach (GameObject _laser in LaserList)
        {
            _laser.SetActive(true);
        }
    }
}
