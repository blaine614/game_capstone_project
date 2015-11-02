using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using System.Collections;
using UnityEngine.UI;

public class InteractWithObject : Audible {
	public float distance;
	public Font font;
	public GameObject mainCamera;
	public GameObject canvas;
	public GameObject text;

	private Camera camera;
	private bool readingNote = false;

	void Start () {
		camera = mainCamera.GetComponent<Camera>();
		SetupAudioSource ();
		audioSource.loop = true;
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

		if (IsMoving () && !audioSource.isPlaying) {
			audioSource.Play ();
		} else if (!IsMoving () && audioSource.isPlaying) {
			audioSource.Stop ();
		}
	}

	public override void Action() {
		audioSource.Play ();
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
				gameObject.GetComponentInChildren<SanityMeter>().IncreaseSanity(-2.5f);
			}
			
			GameObject p = hit.collider.gameObject;
            if (p.CompareTag("Pill")){
                p.SetActive(false);
                gameObject.GetComponentInChildren<SanityMeter>().IncreaseSanity(25.0f);
            }
		}
	}

	void ReadNote(string note) {
		readingNote = true;
		canvas.SetActive (true);
		text.GetComponent<Text> ().text = note;
		SetMovement (0.0f, 0.0f, 0.0f, 0.0f, 0.0f);
	}

	void StopNote() {
		readingNote = false;
		canvas.SetActive (false);
		text.GetComponent<Text> ().text = "";
		SetMovement (2.0f, 2.0f, 6.0f, 3.0f, 3.0f);
	}

	void SetMovement(float xSense, float ySense, float forwardSpeed, float backwardSpeed, float strafeSpeed) {
		RigidbodyFirstPersonController rfpc = gameObject.GetComponent<RigidbodyFirstPersonController> ();
		rfpc.mouseLook.XSensitivity = ySense;
		rfpc.mouseLook.YSensitivity = xSense;
		rfpc.movementSettings.ForwardSpeed = forwardSpeed;
		rfpc.movementSettings.BackwardSpeed = backwardSpeed;
		rfpc.movementSettings.StrafeSpeed = strafeSpeed;
	}

	bool IsMoving() {
		if (Input.GetKey(KeyCode.W) || Input.GetKey (KeyCode.A) ||
		    Input.GetKey (KeyCode.S) || Input.GetKey (KeyCode.D)) {
			return true;
		} else {
			return false;
		}
	}
}

