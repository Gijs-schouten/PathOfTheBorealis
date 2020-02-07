using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ParticleEnum
{
    SwimBubbles,
    BreathBubbles,
    WaterSplash
}

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance;

    [SerializeField] private List<ParticleToPool> ParticlesToPool;
    private Dictionary<ParticleEnum, ParticlePool> PoolDictionary = new Dictionary<ParticleEnum, ParticlePool>();

    private void Start()
    {
        Instance = this;

        for (int i = 0; i < ParticlesToPool.Count; i++)
        {
            ParticlePool pool = gameObject.AddComponent<ParticlePool>();
            pool.ParticleToPool = ParticlesToPool[i].Particle;
            PoolDictionary.Add(ParticlesToPool[i].ParticleType, pool);
        }
    }

    public void PlayParticle(ParticleEnum whatParticle, Vector3 position)
    {
        if (PoolDictionary.ContainsKey(whatParticle))
            PoolDictionary[whatParticle].GetParticle(position);
    }
}
