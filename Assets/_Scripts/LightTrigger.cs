using UnityEngine;
using System.Collections;

public class LightTrigger : MonoBehaviour {
	public Light[] triggered;
	public AudioSource humming;
	public bool on;
	// Use this for initialization
	void Start () {
		if (!on) {
			for (int i = 0; i < triggered.Length; i++) {
				triggered [i].enabled = false;
			}

		} else {
			humming.Play ();
		}

	}
	void OnTriggerEnter(Collider Player){
		for (int i = 0; i < triggered.Length; i++) {
			if (Player.GetComponent<Collider> ().gameObject.tag == "Player"){
				if(triggered[i].enabled){
					triggered[i].enabled = false;
					humming.Stop();
					on = false;
				}
				else
				{
					triggered[i].enabled = true;
					humming.Play();
					on = true;
				}
			   }
			}
	}

}
