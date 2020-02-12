using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePool : MonoBehaviour
{
    private List<ParticleSystem> ReadyPool = new List<ParticleSystem>();
    private List<ParticleSystem> NotReadyPool = new List<ParticleSystem>();

    private GameObject ReadyPoolChild;
    private GameObject NotReadyPoolChild;

    [HideInInspector] public ParticleSystem ParticleToPool;
    private int ParticleStartCount = 5;

    private void Start()
    {
        ReadyPoolChild = Instantiate(new GameObject()); ReadyPoolChild.transform.SetParent(transform);
        NotReadyPoolChild = Instantiate(new GameObject()); NotReadyPoolChild.transform.SetParent(transform);

        for (int i = 0; i < ParticleStartCount; i++)
        {
            ParticleSystem particleSystem = Instantiate(ParticleToPool);
            particleSystem.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            particleSystem.gameObject.SetActive(false);
            particleSystem.transform.SetParent(ReadyPoolChild.transform);
            ReadyPool.Add(particleSystem);
        }
    }

    public void GetParticle(Vector3 pos)
    {
        if(NotReadyPool.Count > 0)
            CheckNotReadyPool();
        if(ReadyPool.Count < 1)
            DoublePool();

        for (int i = 0; i < ReadyPool.Count; i++)
        {
            ParticleSystem particle = ReadyPool[i];
            if (!particle.isPlaying)
            {
                particle.transform.SetParent(NotReadyPoolChild.transform);
                particle.transform.position = pos;
                particle.gameObject.SetActive(true);
                particle.Play();
                NotReadyPool.Add(ReadyPool[i]);
                ReadyPool.Remove(particle);
                return;
            }
        }
    }

    private void DoublePool()
    {
        for (int i = 0; i < NotReadyPool.Count; i++)
        {
            ParticleSystem copy = Instantiate(NotReadyPool[i]);
            copy.gameObject.SetActive(false);
            copy.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            copy.transform.SetParent(ReadyPoolChild.transform);
            ReadyPool.Add(copy);
        }
    }

    private void CheckNotReadyPool()
    {
        for (int i = 0; i < NotReadyPool.Count; i++)
        {
            ParticleSystem particle = NotReadyPool[i];
            if(!particle.isPlaying)
            {
                particle.transform.SetParent(ReadyPoolChild.transform);
                particle.gameObject.SetActive(false);
                particle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                ReadyPool.Add(particle);
                NotReadyPool.Remove(particle);
            }
        }
    }
} 

