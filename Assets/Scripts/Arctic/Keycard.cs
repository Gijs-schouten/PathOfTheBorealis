using System;
using UnityEngine;

/// <summary>
/// Script op de keycard om de deur naar volgende portal te openen
/// </summary>

public class Keycard : MonoBehaviour {
	private bool scanned = false;

	private Timer time;
	public GameObject scanner;
	private Renderer scan;
	private AudioSource source;

	public event Action Scanned;

	void Start() {
		source = GetComponent<AudioSource>();
		time = GetComponent<Timer>();
		scan = scanner.GetComponent<Renderer>();

		//subs
		Scanned += Scan;
		time.TimerDone += Done;
	}

	//voert action Scanned uit als de keycard er tegen aan gehouden wordt
	private void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.tag == "Scanner") {
			if (scanned == false) {
				Scanned?.Invoke();
			}
		}
	}

	//gesubscribed aan Scanned
	private void Scan() {
		time.StartTimer(4f);
		scanned = true;
		source.Play();
		SetEmission(true, new Color(1f, 1f, 1f, 1f), scan.material);
	}
	//gesubscribed aan TimerDone
	private void Done() {
		scanned = false;
		SetEmission(false, new Color(0f, 0f, 0f, 0f), scan.material);
	}

	//zet de emission map van een shader aan of uit, hierbij het groene van de keycard scanner
	private void SetEmission(bool state, Color color, Material mat) {
		mat.SetColor("_EMISSION", color);

		if (state) {
			mat.EnableKeyword("_EMISSION");
		} else {
			mat.DisableKeyword("_EMISSION");
		}
	}
}
