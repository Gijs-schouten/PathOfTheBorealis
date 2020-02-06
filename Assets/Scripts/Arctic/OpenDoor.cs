using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour {
	public Lock locks;
	public GameObject sceneSwitch;

	public void SwitchScene() {
		if (locks.locked == true) return;

		sceneSwitch.SetActive(true);
	}
}
