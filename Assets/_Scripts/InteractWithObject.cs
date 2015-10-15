using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using System.Collections;
using UnityEngine.UI;

public class InteractWithObject : MonoBehaviour {
	public float distance;
	public Font font;

	private Camera mainCamera;
	private GameObject display;
	private bool readingNote = false;

	void Start () {
		mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
		display = new GameObject ("Canvas");
		SetupCanvas ();
		SetupText ();
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

		Ray ray = mainCamera.ScreenPointToRay(new Vector3(x,y));
		RaycastHit hit;
		if(Physics.Raycast(ray, out hit, distance)) {

			Interactable a = hit.collider.GetComponent<Interactable>();
			if(a != null) {
				a.Action();
			}

			NoteInteractable b = hit.collider.GetComponent<NoteInteractable>();
			if(b != null) {
				ReadNote (b.ReceiveNote());
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
		display.GetComponent<Canvas>().enabled = true;
		display.GetComponent<Text> ().text = note;
		SetMovement (0.0f, 0.0f, 0.0f, 0.0f, 0.0f);
	}

	void StopNote() {
		readingNote = false;
		display.GetComponent<Canvas>().enabled = false;
		display.GetComponent<Text> ().text = "";
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

	void SetupCanvas () {
		Canvas canvas = display.AddComponent<Canvas> ();
		canvas.enabled = false;
		canvas.renderMode = RenderMode.ScreenSpaceCamera;
		canvas.pixelPerfect = false;
		canvas.worldCamera = mainCamera;
		canvas.planeDistance = 0.35f;
	}

	void SetupText () {
		Text text = display.AddComponent<Text> ();
		text.font = font;
		text.fontStyle = FontStyle.BoldAndItalic;
		text.fontSize = 12;
		text.supportRichText = true;
		text.alignment = TextAnchor.MiddleCenter;
		text.horizontalOverflow = HorizontalWrapMode.Wrap;
		text.verticalOverflow = VerticalWrapMode.Truncate;
		text.resizeTextForBestFit = true;
		text.color = Color.white;
	}
}

