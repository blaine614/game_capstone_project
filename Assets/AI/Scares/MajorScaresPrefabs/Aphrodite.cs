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
	private float glitchTimer = 4.0f;

	// Use this for initialization
	void Start () {
		NavMeshHit hit;
		NavMesh.SamplePosition (transform.position, out hit, 1.0f, NavMesh.AllAreas);
		//transform.position = hit.position;
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
		//RaycastHit rayHit;
		//Physics.Raycast (camera.transform.position, camera.transform.forward, out rayHit); 
		//if (Physics.Raycast(camera.ScreenPointToRay(camera.transform.forward), out rayHit))
		//	beingWatched = true;
		if (destroyObj && !sound2.isPlaying) {
			Destroy (gameObject);
		} else if (!beingWatched && !sound2.isPlaying) {
			if (!glitchAdded) {
				glitchAdded = true;
				//camera.gameObject.GetComponent<GlitchEffect>().Intensity = 20;
			}
			Vector3 lookPos = camera.transform.position - transform.position;
			lookPos.y = 0;
			Quaternion quat = Quaternion.LookRotation (lookPos);
			transform.rotation = Quaternion.Slerp (transform.rotation, quat, Time.deltaTime * ROTATION_SPEED);
			agent.SetDestination (target);
			agent.Resume ();
			if ((agent.transform.position - target).magnitude < DEPTH) {
				sound1.Pause ();
			} else if (!sound1.isPlaying)
				sound1.UnPause ();
		} else if (!sound2.isPlaying) {
			sound1.Pause ();
			agent.Stop ();
			sound2.UnPause ();
			destroyObj = true;
		} else if (sound2.isPlaying) {
			glitchTimer -= Time.deltaTime;
			if (glitchTimer <= 0) {
				transform.position = new Vector3(-100, -100, -100);
				camera.gameObject.GetComponent<GlitchEffect>().intensity = 0.1f;
//				camera.gameObject.GetComponent<GlitchEffect>().glitchOn = false;
			} else {
				camera.gameObject.GetComponent<GlitchEffect>().intensity += 0.01f;
			}
		}
	}

	void OnBecameVisible() {
		beingWatched = true;
		//camera.gameObject.GetComponent<GlitchEffect> ().glitchOn = true;
	}
}
