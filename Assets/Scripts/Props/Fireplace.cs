using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireplace : MonoBehaviour
{
    private ParticleSystem FireParticle;
    private AudioSource FireSound;
    private Light FireLight;

    private void Start()
    {
        FireParticle = GetComponentInChildren<ParticleSystem>();
        FireSound = FireParticle.GetComponentInChildren<AudioSource>();
        FireLight = FireParticle.GetComponentInChildren<Light>();
    }

    private void OnTriggerEnter(Collider Coll)
    {
        if(Coll.CompareTag("Candle"))
        {
            FireParticle.Play();
            FireSound.Play();
            StartCoroutine(LerpLighting(0, 5));
        }
    }

    private IEnumerator LerpLighting(float start, float end)
    {
        float lerpValue = 0;

        while (true)
        {
            FireLight.intensity = Mathf.Lerp(start, end, lerpValue);

            if (lerpValue >= 1)
                break;

            lerpValue += 1 * Time.deltaTime;
            yield return null;
        }
        yield return null;

    }
}
