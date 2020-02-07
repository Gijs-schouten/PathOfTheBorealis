using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Letter : MonoBehaviour
{
    private Animator Anim;
    [SerializeField]
    private Light UnderDoorLight;
    [SerializeField]
    private AudioSource KnockingSound, SlidingSound;

    private void Awake()
    {
        Anim = GetComponent<Animator>();
    }

    public void Kocking()
    {
        StartCoroutine(LerpLighting(0, 100));
        Anim.SetTrigger("SlideLetter");
    }

    public void Slideletter()
    {
        KnockingSound.Play();
        SlidingSound.Play();
    }
    
    public void DisableLight()
    {
        StartCoroutine(LerpLighting(100, 0));
    }

    private IEnumerator LerpLighting(float start, float end)
    {
        float lerpValue = 0;

        while(true)
        {
            UnderDoorLight.intensity = Mathf.Lerp(start, end, lerpValue);

            if(lerpValue >= 1)
                break;

            lerpValue += 1 * Time.deltaTime;
            yield return null;
        }
        yield return null;

    }
}
