using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using System.Collections;

public class InteractWithObject : MonoBehaviour {
	GameObject mainCamera;
	public float distance;

	private bool readingNote;
	// Use this for initialization
	void Start () {
		mainCamera = GameObject.FindWithTag("MainCamera");
		readingNote = false;
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

            GameObject p = hit.collider.gameObject;
            if (p.CompareTag("Pill")){
                p.SetActive(false);
                gameObject.GetComponent<SanityMeter>().IncreaseSanity(50.0f);
            } else if (p.CompareTag("Note")){
				ReadNote ();
			}
		}
	}

	void ReadNote() {
		gameObject.GetComponent<RigidbodyFirstPersonController> ().movementSettings.ForwardSpeed = 0.0f;
		gameObject.GetComponent<RigidbodyFirstPersonController> ().movementSettings.BackwardSpeed = 0.0f;
		gameObject.GetComponent<RigidbodyFirstPersonController> ().movementSettings.StrafeSpeed = 0.0f;

		gameObject.GetComponent<RigidbodyFirstPersonController> ().movementSettings.ForwardSpeed = 6.0f;
		gameObject.GetComponent<RigidbodyFirstPersonController> ().movementSettings.BackwardSpeed = 3.0f;
		gameObject.GetComponent<RigidbodyFirstPersonController> ().movementSettings.StrafeSpeed = 3.0f;
	}
}

