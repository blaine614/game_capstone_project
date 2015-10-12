using UnityEngine;
using System.Collections;

public class SoundPuzzleMaster : MonoBehaviour {
	
	public Audible trigger;
	public int A;
	public int B;
	public int C;
	public int D;
	public int E;
	public int F;

	private int currentPosition;
	private AudioSource audioSource;

	void Start () {
		SetOrder ();
		currentPosition = 1;
	}
	
	void Update () {
		if (currentPosition >= 4) {
			trigger.Action ();
		}
	}

	public void ReceiveSequence(int key) {
		if (currentPosition == key) {
			currentPosition++;
			Debug.Log (currentPosition);
		} else {
			currentPosition = 1;
		}
		Debug.Log (currentPosition);
	}

	void SetOrder () {
		foreach (Transform keypad in transform) {
			KeyPadInteractable keyPadInteractable = keypad.GetComponent<KeyPadInteractable>();
			if(keypad.CompareTag("KeyPadA")) {
				keyPadInteractable.SetOrder (A);
			} else if(keypad.CompareTag("KeyPadB")) {
				keyPadInteractable.SetOrder (B);
			} else if(keypad.CompareTag("KeyPadC")) {
				keyPadInteractable.SetOrder (C);
			} else if(keypad.CompareTag("KeyPadD")) {
				keyPadInteractable.SetOrder (D);
			} else if(keypad.CompareTag("KeyPadE")) {
				keyPadInteractable.SetOrder (E);
			} else if(keypad.CompareTag("KeyPadF")) {
				keyPadInteractable.SetOrder (F);
			}
		}
	}
}
