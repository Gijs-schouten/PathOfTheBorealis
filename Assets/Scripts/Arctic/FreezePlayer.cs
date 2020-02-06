using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezePlayer : MonoBehaviour {
	[SerializeField]
	private Vector3 pos;

	void Update() {
		gameObject.transform.position = pos;
	}
}
