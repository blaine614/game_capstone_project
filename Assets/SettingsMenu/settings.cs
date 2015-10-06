using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class settings : MonoBehaviour {

	public Slider musicSlider;
	public Slider ambianceSlider;
	public Image musicSliderFill;
	public Image ambianceSliderFill;
	public AudioSource music;
	public AudioSource ambiance;

	private Slider currentSlider;
	private Slider[] sliders;
	private Image currentSliderFill;
	private Image[] fills;
	private int current;

	private Color CURRENT_COLOR = Color.red;
	private Color DEFAULT_COLOR = Color.white;
	private const int NUM_OF_SLIDERS = 2;
	private const float STEP_SIZE = 0.25f;

	// Use this for initialization
	void Start () {
		GetComponent<Canvas> ().enabled = false;
		current = 0;

		sliders = new Slider[NUM_OF_SLIDERS];
		sliders [0] = musicSlider;
		sliders [1] = ambianceSlider;
		fills = new Image [NUM_OF_SLIDERS];
		fills [0] = musicSliderFill;
		fills [1] = ambianceSliderFill;

		UpdateCurrent ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Q)) {
			GetComponent<Canvas>().enabled = !GetComponent<Canvas>().enabled;
		}
		if (GetComponent<Canvas> ().enabled) {
			if (Input.GetKeyDown (KeyCode.T) || Input.GetKeyDown (KeyCode.G))
				SetCurrent ();
			if (Input.GetKeyDown (KeyCode.F))
				currentSlider.value -= STEP_SIZE;
			if (Input.GetKeyDown(KeyCode.H))
				currentSlider.value += STEP_SIZE;
		}
		music.volume = musicSlider.value;
		ambiance.volume = ambianceSlider.value;
	}

	void SetCurrent () {
		currentSliderFill.color = DEFAULT_COLOR;
		if (Input.GetKeyDown (KeyCode.T))
			current--;
		else if (Input.GetKeyDown (KeyCode.G))
			current++;
		if (current < 0)
			current = NUM_OF_SLIDERS - 1;
		else if (current >= NUM_OF_SLIDERS)
			current = 0;
		UpdateCurrent ();
	}

	void UpdateCurrent() {
		currentSlider = sliders [current];
		currentSliderFill = fills [current];
		currentSliderFill.color = CURRENT_COLOR;
	}
}
