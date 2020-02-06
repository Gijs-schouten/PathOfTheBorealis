using System;
using UnityEngine;

public class KeypadButton : MonoBehaviour {
	public int value;
	public event Action<int> ButtonPressed;
	
	public void Pressed() {
		ButtonPressed(value);
	}
}