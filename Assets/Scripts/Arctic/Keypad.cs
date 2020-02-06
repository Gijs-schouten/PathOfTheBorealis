using System;
using UnityEngine;

/// <summary>
/// script voor de keypad, zie ook KeypadButton.cs
/// </summary>

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

	//subt all de button actions aan Press()
	public void Subscribe(GameObject[] array, Action<int> method) {
		for(int i = 0; i < buttons.Length; i++) {
			array[i].GetComponent<KeypadButton>().ButtonPressed += method;
		}
	}

	//gaat af wanneer een toets wordt ingedrukt 1 tm 10 zijn de knoppen ( 0 tm 9 ) 10 is groen (check) , 11 is rood (reset)
	private void Press(int value) {
		if(value < 10) {
			AddNumber(value);
		}else if(value == 10) {
			Check();
		} else if(value == 11) {
			ResetKeys();
		}
	}

	//voegt nummer toe aan de ingedrukte reeks, als de reeks vol zit schuift elk nummer 1 plaats op naar rechts en komt de nieuwe voor aan.
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

	//check voor de code
	private void Check() {
		if(d == null) {
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

	//zet alle nummers achter elkaar als 1 int (1 2 3 4 wordt 1234) zodat deze gecheckt kan worden
	public int Combine(int? a, int? b, int? c, int? d) {
		return Convert.ToInt32(string.Format("{0}{1}{2}{3}", a, b, c, d));
	}
}
