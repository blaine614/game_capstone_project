using UnityEngine;
using System.Collections;

public class KeyInteractable : Audible {

	public GameObject lockObject;
	public AudioClip farClip;
	public AudioClip nearClip;
	public AudioClip hereClip;
	public AudioClip collisionClip;

	private AudioSource noiseSource;
	private bool hit = false;

	// Use this for initialization
	void Start () {
		SetupAudioSource ();
		SetupNoiseSource ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!hit) {
			CheckAudio ();
		}
	}

	public override void Action () {
		audioSource.Play ();
	}

	void CheckAudio() {
		float distance = Vector3.Distance (lockObject.transform.position, transform.position);

		if (distance > 25.0f && noiseSource.clip != farClip) {
			ChangeAudio (farClip, noiseSource.time);
		} else if (distance <= 25.0f && distance > 10.0f && noiseSource.clip != nearClip) {
			ChangeAudio (nearClip, noiseSource.time);
		} else if (distance <= 10.0f && distance > 0.5f && noiseSource.clip != hereClip) {
			ChangeAudio (hereClip, noiseSource.time);
		} else if (distance <= 0.5f && noiseSource.clip != collisionClip) {
			noiseSource.loop = false;
			ChangeAudio (collisionClip, 0.0f);
			hit = true;
		}
	}

	void ChangeAudio(AudioClip audioClip, float time) {
		noiseSource.Stop ();
		noiseSource.clip = audioClip;
		noiseSource.time = time;
		noiseSource.Play ();
	}

	void SetupNoiseSource() {
		noiseSource = gameObject.AddComponent<AudioSource>();
		noiseSource.clip = farClip;
		noiseSource.dopplerLevel = 0.0f;
		noiseSource.volume = 0.1f;
		noiseSource.loop = true;
		noiseSource.maxDistance = 4.0f;
		noiseSource.minDistance = 1.5f;
		noiseSource.spatialBlend = 1.0f;
		audioSource.dopplerLevel = 0.0f;
		noiseSource.Play ();
	}
}
