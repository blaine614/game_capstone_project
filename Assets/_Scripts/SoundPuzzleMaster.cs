using UnityEngine;
using System.Collections;

public class SoundPuzzleMaster : MonoBehaviour {

	public GameObject audioSourceGameObject;
	public AudioClip audioClip;
	public int A;
	public int B;
	public int C;
	public int D;
	public int E;
	public int F;

	private int currentPosition;
	private AudioSource audioSource;

	// Use this for initialization
	void Start () {
		SetOrder ();
		currentPosition = 1;
		audioSource = audioSourceGameObject.AddComponent<AudioSource>();
		audioSource.clip = audioClip;
		audioSource.dopplerLevel = 0.0f;
		audioSource.loop = true;
		audioSource.maxDistance = 10.0f;
		audioSource.minDistance = 3.0f;
		audioSource.spatialBlend = 1.0f;
		//audioSource.Play ();
	}
	
	// Update is called once per frame
	void Update () {
		if (currentPosition >= 4) {
			audioSource.Stop ();
		}
	}

	public void ReceiveSequence(int key) {
		if (currentPosition == key) {
			currentPosition++;
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
				Debug.Log ("Found A");
			} else if(keypad.CompareTag("KeyPadB")) {
				keyPadInteractable.SetOrder (B);
				Debug.Log ("Found B");
			} else if(keypad.CompareTag("KeyPadC")) {
				keyPadInteractable.SetOrder (C);
				Debug.Log ("Found C");
			} else if(keypad.CompareTag("KeyPadD")) {
				keyPadInteractable.SetOrder (D);
				Debug.Log ("Found D");
			} else if(keypad.CompareTag("KeyPadE")) {
				keyPadInteractable.SetOrder (E);
				Debug.Log ("Found E");
			} else if(keypad.CompareTag("KeyPadF")) {
				keyPadInteractable.SetOrder (F);
				Debug.Log ("Found F");
			}
		}
	}
}
