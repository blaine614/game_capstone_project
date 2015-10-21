using UnityEngine;
using System.Collections;

public class Flicker : MonoBehaviour {

	public Light flashingLight;
	// public Light secondFlashingLight;
	private float randomNumber;
	private Material[] Led_materials;
	public Material Change_UP, Change_Down;
	public GameObject Led;
	void Start(){
		Led_materials = Led.GetComponent<Renderer>().materials;
		flashingLight.enabled = false;
		//secondFlashingLight.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
		randomNumber = Random.value;
		
		if (randomNumber <= 0.95f) {
			
			flashingLight.enabled = true;
			Led_materials[3] = Change_UP;
			Led.GetComponent<Renderer>().materials = Led_materials;
		} else {
			Led_materials[3] = Change_Down;
			flashingLight.enabled = false;
			Led.GetComponent<Renderer>().materials = Led_materials;
		}
		

 }
}
