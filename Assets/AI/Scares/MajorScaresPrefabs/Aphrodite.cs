using UnityEngine;
using System.Collections;

public class Aphrodite : MonoBehaviour {

	new private Camera camera;

	private NavMeshAgent agent;
	private Vector3 target;
	private bool beingWatched;
	private bool destroyObj;
	private bool glitchAdded;
	private AudioSource sound1;
	private AudioSource sound2;
	private const float DEPTH = 2;

	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent> ();
		camera = Camera.main;
		beingWatched = false;
		destroyObj = false;
		glitchAdded = false;
		AudioSource[] audios = GetComponents<AudioSource> ();
		sound1 = audios [0];
		sound2 = audios [1];
		sound2.Pause ();
	}
	
	// Update is called once per frame
	void Update () {
		const float ROTATION_SPEED = 10;
		target = camera.transform.position - camera.transform.forward * DEPTH;
		//check raycast
		if (GetComponentInChildren<Renderer>().isVisible)
			beingWatched = true;
		if (destroyObj && !sound2.isPlaying) {
			Destroy(camera.gameObject.GetComponent("GlitchEffect"));
			Destroy (gameObject);
		}
		else if (!beingWatched && !sound2.isPlaying) {
			if (!glitchAdded) {
				//camera.gameObject.AddComponent<GlitchEffect>();
				//Shader glitchShader = Shader.Find("GlitchShader");
				//camera.RenderWithShader(glitchShader, null);
				glitchAdded = true;
				//camera.gameObject.GetComponent<GlitchEffect>().Intensity = 20;
			}
			Vector3 lookPos = camera.transform.position - transform.position;
			lookPos.y = 0;
			Quaternion quat = Quaternion.LookRotation (lookPos);
			transform.rotation = Quaternion.Slerp (transform.rotation, quat, Time.deltaTime * ROTATION_SPEED);
			agent.SetDestination (target);
			agent.Resume ();
			if (!sound1.isPlaying)
				sound1.UnPause ();
		} else if (!sound2.isPlaying) {
			sound1.Pause ();
			agent.Stop ();
			sound2.UnPause();
			destroyObj = true;
		}
	}
}
