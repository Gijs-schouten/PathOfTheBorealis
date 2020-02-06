using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Particle to pool", menuName = "Particle To Pool")]
public class ParticleToPool : ScriptableObject
{
    public ParticleEnum ParticleType;
    public ParticleSystem Particle;
}
