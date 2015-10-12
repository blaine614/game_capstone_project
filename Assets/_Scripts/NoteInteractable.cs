using UnityEngine;
using System.Collections;

public class NoteInteractable : Audible {

	public string message;

	void Start () {
		SetupAudioSource ();
	}
	
	public override void Action () {
		audioSource.Play ();
	}

	public string ReceiveNote () {
		return message;
	}
}
