using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  zet sceneswitch object op true waar steamvr sceneload op staat, die switcht scene onAwake, dit gaat af wanneer de deur hendel wordt over gehaald met circular drive maxangle
/// </summary>

public class OpenDoor : MonoBehaviour {
	public Lock locks;
	public GameObject sceneSwitch;

	public void SwitchScene() {
		if (locks.locked == true) return;

		sceneSwitch.SetActive(true);
	}
}
