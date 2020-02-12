using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candle : MonoBehaviour
{
    private Light CandleLight;

    List<ParticleSystem> Particles;

    // Start is called before the first frame update
    void Start()
    {
        CandleLight = GetComponentInChildren<Light>();
        Particles = new List<ParticleSystem>();
        ParticleSystem[] ChildParticles = GetComponentsInChildren<ParticleSystem>();
        for (int i = 0; i < ChildParticles.Length; i++)
        {
            Particles.Add(ChildParticles[i]);
        }
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
            OnPickup();
        if (Input.GetKeyDown(KeyCode.N))
            OnDrop();
    }

    public void OnPickup()
    {
        StartCoroutine(LerpLighting(0, 2));

        for (int i = 0; i < Particles.Count; i++)
        {
            Particles[i].Play();
        }
    }

    public void OnDrop()
    {
        StartCoroutine(LerpLighting(2, 0));

        for (int i = 0; i < Particles.Count; i++)
        {
            Particles[i].Stop();
        }
    }

    private IEnumerator LerpLighting(float start, float end)
    {
        float lerpValue = 0;

        while (true)
        {
            CandleLight.intensity = Mathf.Lerp(start, end, lerpValue);

            if (lerpValue >= 1)
                break;

            lerpValue += 2 * Time.deltaTime;
            yield return null;
        }
        yield return null;

    }
}
