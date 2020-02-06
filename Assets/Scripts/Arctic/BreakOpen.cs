using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakOpen : MonoBehaviour {
	public Animator anim;

	private void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Crowbar") {
			anim.SetBool("cool", true);
		}
	}
}
