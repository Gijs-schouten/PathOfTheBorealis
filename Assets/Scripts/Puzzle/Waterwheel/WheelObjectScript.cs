using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelObjectScript : MonoBehaviour
{
    [Header("Waterwheel script")]
    [SerializeField] private WaterwheelScript Waterwheel;

    [Header("Objects")]
    [SerializeField] private GameObject BigObject;
    [SerializeField] private GameObject[] SmallObjects;
    
    [Header("Values")]
    [SerializeField] private int HitCount;
    [SerializeField] private int SmallRockCount;

    private Collider ObjectCollider;

    private bool BigRockBroken;
    private bool WheelFreed;

    void Start()
    {
        ObjectCollider = GetComponent<Collider>();

        foreach (GameObject _object in SmallObjects)
        {
            _object.SetActive(false);
        }
    }

    private void Update()
    {
        if (BigRockBroken && SmallRockCount < 1 && !WheelFreed)
        {
            Waterwheel.ReleaseWheel();
            WheelFreed = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!WheelFreed)
        {
            if (other.gameObject == Waterwheel.KeyObject && !BigRockBroken)
            {
                HitCount -= 1;

                //Debug.Log(HitCount);
                //Debug.Log(gameObject);

                if (HitCount < 1)
                {
                    BigObject.SetActive(false);

                    foreach (GameObject _object in SmallObjects)
                    {
                        _object.SetActive(true);
                    }

                    BigRockBroken = true;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!WheelFreed)
        {
            if (other.name.Contains("SmallRock"))
            {
                SmallRockCount -= 1;
            }
        }
    }

    //Code voor object SLIDE in plaats van HAK
    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.gameObject.name == "KeyObject")
    //    {
    //        transform.Translate(new Vector3(0, 0, 0.1f * Slidespeed) * Time.deltaTime);
    //        SlideCount += 1;

    //        if (SlideCount > 150)
    //        {
    //            Waterwheel.ReleaseWheel();
    //            ObjectCollider.enabled = false;
    //        }
    //    }
    //}
}
