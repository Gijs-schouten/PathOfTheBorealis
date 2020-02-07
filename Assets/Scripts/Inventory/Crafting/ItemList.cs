using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item List", menuName = "Items", order = 1)]
public class ItemList : ScriptableObject
{
    public List<Recepe> items;

}

