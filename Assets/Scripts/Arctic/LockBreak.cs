using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockBreak : MonoBehaviour {
	public GameObject particleObject;
	private ParticleSystem particle;

	void Start() {
		particle = particleObject.GetComponent<ParticleSystem>();
		particle.Stop();
	}

	private void OnCollisionEnter(Collision collision) {
		if(collision.gameObject.tag == "Crowbar") {
			Break();
		}
	}

	private void Break() {
		particle.Play();
		Destroy(gameObject);
	}
}
