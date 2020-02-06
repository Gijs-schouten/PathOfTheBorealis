using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Valve.VR;

public class LaserInput : MonoBehaviour {
	public SteamVR_Input_Sources hand;
	public SteamVR_Action_Boolean click;

	private RaycastHit hits;
	private Animator last;

	private void Start() {
		click.AddOnStateDownListener(TriggerDown, hand);
	}

	private void Update() {
		if (Physics.Raycast(transform.position, transform.forward, out hits, 100)) {
			if (hits.collider.tag == "Button") {
				if (last == null) {
					last = hits.collider.gameObject.GetComponent<Animator>();
				}

				if (last != hits.collider.GetComponent<Animator>()) {
					last.SetTrigger("Normal");
					last = hits.collider.gameObject.GetComponent<Animator>();
					last.SetTrigger("Highlighted");
				}

			}

			if(hits.collider.tag == "canvas") {
				last.SetTrigger("Normal");
			}
		}
	}

	public void TriggerDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource) {
		Click(hits.collider);
	}

	private void Click(Collider collider) {
		if(collider.gameObject.GetComponent<IPointerClickHandler>() == null) {
			return;
		}

		IPointerClickHandler clickHandler = collider.gameObject.GetComponent<IPointerClickHandler>();
		PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
		clickHandler.OnPointerClick(pointerEventData);
	}
}