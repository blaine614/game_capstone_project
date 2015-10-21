using UnityEngine;
using System.Collections;

public class SoundMinorScare : MonoBehaviour {

	new private AudioSource audio;
	private float timer;
	private GameObject director;

	// Use this for initialization
	void Start () {
		audio = gameObject.GetComponent<AudioSource> ();
		timer = audio.clip.length;
		director = GameObject.Find ("Director");
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		if (timer <= 0) {
			director.SendMessage("FinishStage", true, SendMessageOptions.RequireReceiver);
			Destroy(this.gameObject);
		}
	}
}
