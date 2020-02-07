using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.gameObject.CompareTag("Player"))
        { 

        }
    }

    private IEnumerator DelayedSceneSwitch()
    {

        yield return new WaitForSeconds(3);

        SceneManager.LoadScene(0);
    }
}
