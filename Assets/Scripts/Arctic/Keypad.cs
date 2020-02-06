using System;
using UnityEngine;

public class Keypad : MonoBehaviour {
	public int code;

	public GameObject[] buttons;

	public event Action Correct;
	public event Action Incorrect;

	private int? a;
	private int? b;
	private int? c;
	private int? d;
	 
	private void Start() {
		Subscribe(buttons, Press);
		Incorrect += ResetKeys;
	}

	public void Subscribe(GameObject[] array, Action<int> method) {
		for(int i = 0; i < buttons.Length; i++) {
			array[i].GetComponent<KeypadButton>().ButtonPressed += method;
		}
	}

	private void Press(int value) {
		if(value < 10) {
			AddNumber(value);
		}else if(value == 10) {
			Check();
		} else if(value == 11) {
			ResetKeys();
		}
	}

	private void AddNumber(int value) {
		if (a == null) a = value;
		else if (b == null) b = value;
		else if (c == null) c = value;
		else if (d == null) d = value;
		else {
			a = b;
			b = c;
			c = d;
			d = value;
		}

		print($"{a} {b} {c} {d}");
	}

	private void Check() {
		if(a == null) {
			Incorrect?.Invoke();
			return;
		}

		if (code == Combine(a, b, c, d)) {
			Correct?.Invoke();
		} else {
			Incorrect?.Invoke();
		}
	}

	private void ResetKeys() {
		a = null;
		b = null;
		c = null;
		d = null;
	}

	public int Combine(int? a, int? b, int? c, int? d) {
		return Convert.ToInt32(string.Format("{0}{1}{2}{3}", a, b, c, d));
	}
}
