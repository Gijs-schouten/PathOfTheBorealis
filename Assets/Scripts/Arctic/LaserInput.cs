using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Valve.VR;

public class LaserInput : MonoBehaviour {
	//referentie naar hand
	public SteamVR_Input_Sources hand;
	//referentie naar custom click SteamVR event
	public SteamVR_Action_Boolean click;

	private RaycastHit hits;
	private Animator last;

	private void Start() {
		//voegt triggerdown() toe aan trigger van hand
		click.AddOnStateDownListener(TriggerDown, hand);
	}

	private void Update() {
		//highlight de main menu knoppen en zorgt dat ze klikbaar zijn
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

	//controller trigger functie
	public void TriggerDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource) {
		Click(hits.collider);
	}

	//zorgt dat de OnClick event van een button afgaat bij een raycast 
	private void Click(Collider collider) {
		if(collider.gameObject.GetComponent<IPointerClickHandler>() == null) {
			return;
		}

		IPointerClickHandler clickHandler = collider.gameObject.GetComponent<IPointerClickHandler>();
		PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
		clickHandler.OnPointerClick(pointerEventData);
	}
}