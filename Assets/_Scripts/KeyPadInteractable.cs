using UnityEngine;
using System.Collections;

public class KeyPadInteractable : Audible {
	
	private int order;

	void Start () {
		SetupAudioSource ();
	}

	public override void Action () {
		audioSource.Play ();
		SoundPuzzleMaster soundPuzzleMaster = transform.parent.GetComponent<SoundPuzzleMaster> ();
		soundPuzzleMaster.ReceiveSequence (order);
	}

	public void SetOrder (int val) {
		order = val;
	}
}
