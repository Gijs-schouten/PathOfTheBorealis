using System;
using UnityEngine;

public class KeypadButton : MonoBehaviour {
	public int value;
	public event Action<int> ButtonPressed;
	
	//roept zijn value naar Keypad.cs
	public void Pressed() {
		ButtonPressed(value);
	}
}