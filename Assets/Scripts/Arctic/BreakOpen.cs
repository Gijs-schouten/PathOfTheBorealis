using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// simpel script voor het openen van de deur naar buiten met de crowbar
/// </summary>

public class BreakOpen : MonoBehaviour {
	public Animator anim;

	private void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Crowbar") {
			anim.SetBool("cool", true);
		}
	}
}
