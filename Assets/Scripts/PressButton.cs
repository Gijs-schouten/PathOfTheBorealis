using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressButton : MonoBehaviour
{

    bool mondOpen = false;

    public GameObject mond;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            StartCoroutine(MondOpen());
        }


    }

    IEnumerator MondOpen()
    {
        while (mond.transform.position.y >= 25f)
        {
            Debug.Log(mond.transform.position.y);
            mond.transform.position = new Vector3(mond.transform.position.x, mond.transform.position.y - 0.02f, mond.transform.position.z);
            yield return new WaitForSecondsRealtime(0.05f);
        }
       

    }
}

