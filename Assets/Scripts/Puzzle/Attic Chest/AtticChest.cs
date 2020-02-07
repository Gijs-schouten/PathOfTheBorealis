using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class AtticChest : MonoBehaviour
{
    [Header("Chest")]
    [SerializeField] private GameObject ChestCover;

    [Header("Items")]
    [SerializeField] private List<GameObject> ItemsList = new List<GameObject>();

    private BoxCollider CoverCollider;

    private void Start()
    {
        CoverCollider = ChestCover.GetComponent<BoxCollider>();
        CoverCollider.enabled = false;
    }

    public void UnlockChest()
    {
        CoverCollider.enabled = true;
    }

    public void MakeItemsPickable()
    {
        foreach (GameObject _item in ItemsList)
        {
            _item.AddComponent<Interactable>();
            _item.AddComponent<Throwable>();
        }
    }
}
