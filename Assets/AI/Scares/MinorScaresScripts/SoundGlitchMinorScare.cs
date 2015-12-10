using UnityEngine;
using System.Collections;

public class SoundGlitchMinorScare : MonoBehaviour {

	new private Camera camera;
	new private AudioSource audio;
	private float timer;
	private GameObject director;

	// Use this for initialization
	void Start () {
		audio = gameObject.GetComponent<AudioSource> ();
		timer = audio.clip.length;
		director = GameObject.Find ("Director");
		camera = Camera.main;
		camera.gameObject.GetComponent<GlitchEffect> ().enabled = true;
		camera.gameObject.GetComponent<GlitchEffect> ().intensity = 0.1f;
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		if (timer <= 0) {
			director.SendMessage ("FinishStage", true, SendMessageOptions.RequireReceiver);
			camera.gameObject.GetComponent<GlitchEffect>().enabled = false;
			Destroy (this.gameObject);
		} else {
			camera.gameObject.GetComponent<GlitchEffect>().intensity += Time.deltaTime;
		}
	}
}
