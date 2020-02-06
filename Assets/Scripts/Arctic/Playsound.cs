using UnityEngine;
using System.Collections;

public class Playsound : MonoBehaviour {
	private AudioSource source;

	private Keypad keypad;

	public AudioClip buttonSound;
	public AudioClip incorrect;
	public AudioClip correct;

	private void Start() {
		source = gameObject.AddComponent<AudioSource>();
		keypad = GetComponent<Keypad>();
		keypad.Subscribe(keypad.buttons, Clicky);
		keypad.Correct += GoodClicky;
		keypad.Incorrect += BadClicky;
	}

	public void Clicky(int value) {
		if (value != 10) {
			PlaySound(buttonSound);
		}
	}

	private void BadClicky() {
		PlaySound(incorrect);
	}

	private void GoodClicky() {
		PlaySound(correct);
	}

	private void PlaySound(AudioClip clip) {
		source.clip = clip;
		source.Play();
	}

}
