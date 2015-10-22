using UnityEngine;
using System.Collections;

public class LightTrigger : MonoBehaviour {
	public Light triggered;
	// Use this for initialization
	void Start () {
		triggered.enabled = false;
	}
	void OnTriggerEnter(Collider Player){
		if(Player.GetComponent<Collider>().gameObject.tag == "Player")
		triggered.enabled = true;
	}
	// Update is called once per frame
	void OnTriggerExit(Collider Player){
		if (Player.GetComponent<Collider>().gameObject.tag == "Player") {
			triggered.enabled = false;
		}
	}
}
