using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleRotation : MonoBehaviour
{
    [SerializeField] private float RotationSpeed;

    private void FixedUpdate()
    {
        transform.Rotate(transform.rotation.x, transform.rotation.y, RotationSpeed * Time.fixedDeltaTime);
    }
}
