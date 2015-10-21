using UnityEngine;
using System.Collections;

public class AutoDoorInteractable : Audible {

	// Use this for initialization
	void Start () {
		SetupAudioSource ();
		audioSource.volume = 100.0f;
	}

	public override void Action() {
		audioSource.Play ();
		foreach (Transform child in gameObject.transform) {
			if(child.name == "doorleft" || child.name == "doorleft2") {
				child.transform.Translate(new Vector3(1.5f,0.0f, 0.0f));
			} else if(child.name == "doorright" || child.name == "doorright2") {
				child.transform.Translate(new Vector3(-1.5f,0.0f, 0.0f));
			}
		}
	}
}
