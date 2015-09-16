using UnityEngine;
using System.Collections;

public class KeyPadInteractable : MonoBehaviour, Interactable {

	public AudioClip keyClip;
	private AudioSource audioSource;

	// Use this for initialization
	void Start () {
		SetupAudioSource ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Action () {
		audioSource.Play ();
	}

	void SetupAudioSource() {
		audioSource = gameObject.AddComponent<AudioSource>();
		audioSource.clip = keyClip;
		audioSource.volume = 0.5f;
		audioSource.dopplerLevel = 0.0f;
		audioSource.loop = false;
		audioSource.maxDistance = 1.5f;
		audioSource.minDistance = 0.5f;
		audioSource.spatialBlend = 1.0f;
	}
}
