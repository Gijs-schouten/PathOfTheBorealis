using System;
using UnityEngine;
using Valve.VR;

public class Lock : MonoBehaviour {
	public bool locked = true;
	private bool scanned = false;

	public event Action Unlocked;

	public Keycard card;
	public Keypad pad;

	void Start() {
		card.Scanned += Unlock;
		pad.Correct += Unlock;
	}

	private void Unlock() {
		if(scanned == false) {
			scanned = true;
			return;
		}

		locked = false;
	}
}
