using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class ItemSlot : MonoBehaviour
{
    #region References
    private Rigidbody _Rb;
    #endregion

    #region Variables
    private GameObject _Slot;
    private Vector3 _RefVelocity;

    private float _MaxDist = .3f;
    #endregion

    private void Awake()
    {
        _Rb = GetComponent<Rigidbody>();
    }

    public void CheckForNearbySlot()
    {
        if(_Slot != null)
        {
            transform.SetParent(_Slot.transform);
            Inventarise(true);
        }
        else
        {
            transform.SetParent(null);
            Inventarise(false);
        }
    }

    private void Update()
    {
        if(_Slot != null && transform.parent == _Slot.transform)
        {
            Vector3 dist = transform.position - _Slot.transform.position;
            if (dist.magnitude > _MaxDist)
            {
                _Slot = null;
                transform.SetParent(null);
                Inventarise(false);
                return;
            }

            transform.position = Vector3.SmoothDamp(transform.position, _Slot.transform.position, ref _RefVelocity, 1.5f);
        }
    }

    private void Inventarise(bool enable)
    {
        _Rb.isKinematic = enable;
        _Rb.useGravity = !enable;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("ItemSlot") && other.transform.childCount == 0)
        {
            _Slot = other.gameObject;
        }
    }
}
