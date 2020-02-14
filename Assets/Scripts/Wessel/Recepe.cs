using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item List", menuName = "recepe", order = 1)]
public class Recepe : ScriptableObject
{
    public string ItemOne;
    public string ItemTwo;
    public GameObject OutPut;
}