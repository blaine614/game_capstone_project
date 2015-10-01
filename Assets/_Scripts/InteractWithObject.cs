using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using System.Collections;
using UnityEngine.UI;

public class InteractWithObject : MonoBehaviour {
	GameObject mainCamera;
	GameObject canvas;
	public float distance;

	private bool readingNote;
	// Use this for initialization
	void Start () {
		mainCamera = GameObject.FindWithTag("MainCamera");
		canvas = GameObject.FindWithTag ("Canvas");
		readingNote = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.E)) {
			if(readingNote) {
				StopNote ();
			} else {
				Interact ();
			}
		}
	}
	
	void Interact() {
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
				ReadNote ("Some note.");
			}
		}
	}

	void ReadNote(string note) {
		readingNote = true;
		canvas.GetComponent<Canvas>().enabled = true;
		canvas.GetComponent<Text> ().text = note;
		gameObject.GetComponent<RigidbodyFirstPersonController> ().mouseLook.XSensitivity = 0.0f;
		gameObject.GetComponent<RigidbodyFirstPersonController> ().mouseLook.YSensitivity = 0.0f;
		gameObject.GetComponent<RigidbodyFirstPersonController> ().movementSettings.ForwardSpeed = 0.0f;
		gameObject.GetComponent<RigidbodyFirstPersonController> ().movementSettings.BackwardSpeed = 0.0f;
		gameObject.GetComponent<RigidbodyFirstPersonController> ().movementSettings.StrafeSpeed = 0.0f;
	}

	void StopNote() {
		canvas.GetComponent<Canvas>().enabled = false;
		gameObject.GetComponent<RigidbodyFirstPersonController> ().mouseLook.XSensitivity = 2.0f;
		gameObject.GetComponent<RigidbodyFirstPersonController> ().mouseLook.YSensitivity = 2.0f;
		gameObject.GetComponent<RigidbodyFirstPersonController> ().movementSettings.ForwardSpeed = 6.0f;
		gameObject.GetComponent<RigidbodyFirstPersonController> ().movementSettings.BackwardSpeed = 3.0f;
		gameObject.GetComponent<RigidbodyFirstPersonController> ().movementSettings.StrafeSpeed = 3.0f;
	}
}

