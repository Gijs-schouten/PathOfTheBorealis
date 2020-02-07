using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingRecepe : MonoBehaviour
{
    [SerializeField]
    private CraftList recepelist;
    

    public static CraftingRecepe instance;

    private void Awake()
    {
       instance = this;
    }

    public GameObject CheckRecepe(string ItemOne, string ItemTwo)
    {
     
        GameObject sucess = null;
        for (int i = 0; i < recepelist.ItemOne.Count; i++)
        {
            Recepe current = recepelist.ItemOne[i];
            if (current.ItemOne == ItemOne && current.ItemTwo == ItemTwo || current.ItemOne == ItemTwo && current.ItemTwo == ItemOne)
            {
                sucess = recepelist.ItemOne[i].OutPut;
            }
        }

        return sucess;
    }
}

