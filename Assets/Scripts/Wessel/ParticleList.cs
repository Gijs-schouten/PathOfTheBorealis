using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Particle List", menuName = "Particle Assets", order = 1)]
public class ParticleList : ScriptableObject
{
    public List<ParticleSystem> Particle;

}