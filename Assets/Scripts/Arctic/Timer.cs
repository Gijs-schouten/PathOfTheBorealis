using UnityEngine;
using System;

public class Timer : MonoBehaviour {
	public event Action TimerDone;
	private float _time;
	private bool _timing = false;

	private void Start() {
		TimerDone += Stop;
	}

	private void Update() {
		if (_timing) {
			if(_time > 0) {
				_time -= Time.deltaTime;
			} else {
				TimerDone();
			}
		}
	}

	public void StartTimer(float time) {
		_time = time;
		_timing = true;
	}

	private void Stop() {
		_timing = false;
		_time = 0;
	}
}
