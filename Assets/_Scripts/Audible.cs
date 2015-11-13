using UnityEngine;
using System.Collections;

public abstract class Audible : MonoBehaviour, Interactable {
	
	public AudioClip keyClip;
	private bool transitionable = false;
	protected AudioSource audioSource;

	protected void SetupAudioSource() {
		audioSource = gameObject.AddComponent<AudioSource>();
		audioSource.clip = keyClip;
		audioSource.volume = 0.5f;
		audioSource.dopplerLevel = 0.0f;
		audioSource.loop = false;
		audioSource.maxDistance = 1.5f;
		audioSource.minDistance = 0.5f;
		audioSource.spatialBlend = 1.0f;
	}

	public abstract void Action ();
}
