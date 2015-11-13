using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using System.Collections;
using UnityEngine.UI;

public class InteractWithObject1 : MonoBehaviour {
	public float distance;
	public Font font;
	public GameObject mainCamera;
	public GameObject canvas;
	public GameObject text;

	private Camera camera;
	private bool readingNote = false;
	private bool isTransitioning = false;

	void Start () {
		camera = mainCamera.GetComponent<Camera>();
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

		Ray ray = camera.ScreenPointToRay(new Vector3(x,y));
		RaycastHit hit;
		if(Physics.Raycast(ray, out hit, distance)) {

			Interactable a = hit.collider.GetComponent<Interactable>();
			if(a != null) {
				a.Action();
			}

			NoteInteractable b = hit.collider.GetComponent<NoteInteractable>();
			if(b != null) {
				ReadNote (b.ReceiveNote());
				gameObject.GetComponentInChildren<SanityMeter1>().IncreaseSanity(-2.5f);
			}
			
			GameObject p = hit.collider.gameObject;
            if (p.CompareTag("Pill")){
                p.SetActive(false);
                gameObject.GetComponentInChildren<SanityMeter1>().IncreaseSanity(25.0f);
            }
		}
	}

	void ReadNote(string note) {
		readingNote = true;
		gameObject.GetComponent<FirstPersonController> ().enabled = false;
		canvas.SetActive (true);
		text.GetComponent<Text> ().text = note;
	}

	void StopNote() {
		readingNote = false;
		gameObject.GetComponent<FirstPersonController> ().enabled = true;
		canvas.SetActive (false);
		text.GetComponent<Text> ().text = "";
	}
}

