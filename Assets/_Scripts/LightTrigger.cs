using UnityEngine;
using System.Collections;

public class LightTrigger : MonoBehaviour {
	public Light[] triggered;

	// Use this for initialization
	void Start () {
		for (int i = 0; i < triggered.Length; i++) 
		{
			triggered[i].enabled = false;
		}

	}
	void OnTriggerEnter(Collider Player){
		for (int i = 0; i < triggered.Length; i++) {
			if (Player.GetComponent<Collider> ().gameObject.tag == "Player"){
				if(triggered[i].enabled){
					triggered[i].enabled = false;
				}
				else
				{
					triggered[i].enabled = true;

				}
			   }
			}
	}

}
