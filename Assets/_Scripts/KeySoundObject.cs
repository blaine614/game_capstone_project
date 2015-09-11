using UnityEngine;
using System.Collections;

public class KeySoundObject : MonoBehaviour {

	public GameObject lockObject;
	public AudioClip farClip;
	public AudioClip nearClip;
	public AudioClip hereClip;
	public AudioClip collisionClip;

	private AudioSource audioSource;
	private bool hit = false;

	// Use this for initialization
	void Start () {
		SetupAudioSource ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!hit) {
			CheckAudio ();
		}
	}

	void CheckAudio() {
		float distance = Vector3.Distance (lockObject.transform.position, transform.position);

		if (distance > 25.0f && audioSource.clip != farClip) {
			ChangeAudio (farClip, audioSource.time);
		} else if (distance <= 25.0f && distance > 10.0f && audioSource.clip != nearClip) {
			ChangeAudio (nearClip, audioSource.time);
		} else if (distance <= 10.0f && distance > 0.5f && audioSource.clip != hereClip) {
			ChangeAudio (hereClip, audioSource.time);
		} else if (distance <= 0.5f && audioSource.clip != collisionClip) {
			audioSource.loop = false;
			ChangeAudio (collisionClip, 0.0f);
			hit = true;
		}
	}

	void ChangeAudio(AudioClip audioClip, float time) {
		audioSource.Stop ();
		audioSource.clip = audioClip;
		audioSource.time = time;
		audioSource.Play ();
	}

	void SetupAudioSource() {
		audioSource = gameObject.AddComponent<AudioSource>();
		//audioSource.clip = Resources.Load ("Assets/Sounds/helpmefar.wav") as AudioClip;
		//audioSource.Play ();
		audioSource.clip = farClip;
		audioSource.dopplerLevel = 0.0f;
		audioSource.loop = true;
		audioSource.maxDistance = 4.0f;
		audioSource.minDistance = 1.5f;
		audioSource.spatialBlend = 1.0f;
		audioSource.Play ();
	}
}
