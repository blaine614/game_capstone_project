using UnityEngine;
using System.Collections;

public class InteractWithObject : MonoBehaviour {
	GameObject mainCamera;
	public float distance;
	// Use this for initialization
	void Start () {
		mainCamera = GameObject.FindWithTag("MainCamera");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.E)) {
			interact ();
		}
	}
	
	void interact() {
		int x = Screen.width / 2;
		int y = Screen.height / 2;
			
		Ray ray = mainCamera.GetComponent<Camera>().ScreenPointToRay(new Vector3(x,y));
		RaycastHit hit;
		if(Physics.Raycast(ray, out hit)) {
			Interactable i = hit.collider.GetComponent<Interactable>();
			if(i != null) {
				i.Action();
			}
		}
	}
}

