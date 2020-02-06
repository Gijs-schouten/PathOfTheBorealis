using System;
using UnityEngine;
using Valve.VR;

/// <summary>
/// lock van deur naar portal, keypad en keycard moeten beide goed zijn
/// </summary>

public class Lock : MonoBehaviour {
	public bool locked = true;
	private bool scanned = false;

	public event Action Unlocked;

	public Keycard card;
	public Keypad pad;

	void Start() {
		//subs naar keycard en pad correct actions
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
