using UnityEngine;
using System.Collections;

public class Flicker : MonoBehaviour {

	public Light flashingLight;
	// public Light secondFlashingLight;
	private float randomNumber;
	public Material[] materials;
	public GameObject sphere;
	void Start(){
		
		flashingLight.enabled = false;
		//secondFlashingLight.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
		randomNumber = Random.value;
		
		if (randomNumber <= 0.95f) {

			sphere.GetComponent<Renderer>().material = materials[0];
			flashingLight.enabled = true;
			// secondFlashingLight.enabled = true;
		} else {
			sphere.GetComponent<Renderer>().material = materials[1];
			flashingLight.enabled = false;
			//  secondFlashingLight.enabled = false;
			
		}
		

 }
}
