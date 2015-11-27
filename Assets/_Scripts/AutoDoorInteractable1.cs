using UnityEngine;
using System.Collections;

public class AutoDoorInteractable1 : Audible {

	// Use this for initialization
	void Start () {
		SetupAudioSource ();
		audioSource.volume = 100.0f;
	}

	public override void Action() {
		audioSource.Play ();
		foreach (Transform child in gameObject.transform) {
			Debug.Log (child.name);
			if(child.name == "automatic_door" || child.name == "automatic_door 8") {
				Debug.Log ("hit");
				child.transform.Translate(new Vector3(-1.5f,0.0f, 0.0f));
			} else if(child.name == "automatic_door 1" || child.name == "automatic_door 7") {
				Debug.Log ("hit");
				child.transform.Translate(new Vector3(1.5f,0.0f, 0.0f));
			}
		}
	}
}
