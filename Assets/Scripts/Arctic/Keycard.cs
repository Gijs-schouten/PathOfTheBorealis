using System;
using UnityEngine;

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

		Scanned += Scan;
		time.TimerDone += Done;
	}

	private void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.tag == "Scanner") {
			if (scanned == false) {
				Scanned?.Invoke();
			}
		}
	}

	private void Scan() {
		time.StartTimer(4f);
		scanned = true;
		source.Play();
		SetEmission(true, new Color(1f, 1f, 1f, 1f), scan.material);
	}

	private void Done() {
		scanned = false;
		SetEmission(false, new Color(0f, 0f, 0f, 0f), scan.material);
	}

	private void SetEmission(bool state, Color color, Material mat) {
		mat.SetColor("_EMISSION", color);

		if (state) {
			mat.EnableKeyword("_EMISSION");
		} else {
			mat.DisableKeyword("_EMISSION");
		}
	}
}
