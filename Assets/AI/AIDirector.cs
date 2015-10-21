using UnityEngine;
using System.Collections;

public class AIDirector : MonoBehaviour {

	public float scaredMeter;
	new public Camera camera;

	public enum stage{minorScare, majorScare, comedy};
	private Vector3 lastCameraDirection;
	private float relaxCounter;
	private float betweenScareCounter;
	private float scareMeterCounter;
	private float cameraDirectionChange;
	private bool relaxOn;
	private bool breakOn;
	private bool executing;
	private bool buildUp;
	private const float MEAN = 0.5f;
	private const float STD_DEV = 0.5f / 3;
	private const float COUNTER_MULTIPLIER = 60;
	private const float SCARE_COUNTER_MULT = 30;
	private const float BUILD_UP_PROBABILITY = 0.9f;
	private const float MAX_SCARED = 50;
	private const float SCARE_STEP = 5;
	private const float SCARE_MULT = 10;

	// Use this for initialization
	void Start () {
		scaredMeter = 0;
		cameraDirectionChange = 0;
		relaxCounter = ReturnNormal () * COUNTER_MULTIPLIER;
		betweenScareCounter = ReturnNormal () * SCARE_COUNTER_MULT;
		relaxOn = true;
		executing = false;
		buildUp = ReturnBernoulli (BUILD_UP_PROBABILITY);
		scareMeterCounter = 0;
		lastCameraDirection = camera.transform.forward;
	}
	
	// Update is called once per frame
	void Update () {
		relaxCounter -= Time.deltaTime;
		//Debug.Log ("relaxCounter = " + relaxCounter + ", relaxOn = " + relaxOn + ", executing = " + executing);
		if (relaxCounter <= 0 && relaxOn && !executing) {
			//Debug.Log ("minor vs comedy");
			relaxOn = false;
			if (buildUp) {
				ExecuteStage (stage.minorScare);
			} else {
				ExecuteStage (stage.comedy);
				buildUp = ReturnBernoulli (BUILD_UP_PROBABILITY);
			}
		} else if (!relaxOn && !executing && betweenScareCounter <= 0) {
			//Debug.Log ("minor vs major, " + scaredMeter);
			if (scaredMeter >= MAX_SCARED) {
				ExecuteStage (stage.majorScare);
				buildUp = ReturnBernoulli (BUILD_UP_PROBABILITY);
			} else {
				ExecuteStage (stage.minorScare);
			}
		} else if (!relaxOn && !executing) {
			betweenScareCounter -= Time.deltaTime;
		}
		
		if (scareMeterCounter > 0) {
			Vector3 currentCameraDirection = camera.transform.forward;
			Vector3 change = new Vector3 (Mathf.Abs (lastCameraDirection.x - currentCameraDirection.x), Mathf.Abs (lastCameraDirection.y - currentCameraDirection.y), Mathf.Abs (lastCameraDirection.z - currentCameraDirection.z));
			cameraDirectionChange += change.magnitude;
			lastCameraDirection = currentCameraDirection;
			scareMeterCounter -= Time.deltaTime;
		} else {
			scaredMeter += cameraDirectionChange * SCARE_MULT;
			cameraDirectionChange = 0;
		}
	}

	void ExecuteStage(stage stg) {
		executing = true;
		switch (stg) {
		case stage.minorScare:
			scaredMeter += SCARE_STEP;
			UpdateScaredMeter();
			ScareData.minorScareTimes minScare = UniformRandomMinScare();
			SendMessage("ActivateMinor", minScare, SendMessageOptions.DontRequireReceiver);
			betweenScareCounter = ReturnNormal() * SCARE_COUNTER_MULT;
			//Debug.Log("activate minor");
			break;
		case stage.majorScare:
			ScareData.majorScareTimes majScare;
			do {
				majScare = UniformRandomMajScare();
			} while (!ScareData.MajorScarePool.Contains(majScare.ToString()));
			SendMessage("ActivateMajor", majScare, SendMessageOptions.DontRequireReceiver);
			StartRelax();
			break;
		case stage.comedy:
			ScareData.comedyTimes comedy = UniformRandomComedy();
			SendMessage("ActivateComedy", comedy, SendMessageOptions.DontRequireReceiver);
			//Debug.Log ("activate comedy");
			StartRelax();
			break;
		default:
			break;
		}
	}

	public void FinishStage(bool minScare){
		executing = false;
		if (!minScare)
			relaxCounter = ReturnNormal () * COUNTER_MULTIPLIER;
	}

	void StartRelax() {
		//Debug.Log ("start relax");
		relaxOn = true;
		scaredMeter = 0;
	}

	ScareData.minorScareTimes UniformRandomMinScare() {
		int rand = Random.Range (1, 9);
		ScareData.minorScareTimes scare;

		//Debug.Log ("rand = " + rand);

		if (rand == 1)
			scare = ScareData.minorScareTimes.Scare1;
		else if (rand == 2)
			scare = ScareData.minorScareTimes.Scare2;
		else if (rand == 3)
			scare = ScareData.minorScareTimes.Scare3;
		else if (rand == 4)
			scare = ScareData.minorScareTimes.Scare4;
		else if (rand == 5)
			scare = ScareData.minorScareTimes.Scare5;
		else if (rand == 6)
			scare = ScareData.minorScareTimes.Scare6;
		else if (rand == 7)
			scare = ScareData.minorScareTimes.Scare7;
		else if (rand == 8)
			scare = ScareData.minorScareTimes.Scare8;
		else// if (rand == 9)
			scare = ScareData.minorScareTimes.Scare9;

		return scare;
	}

	ScareData.majorScareTimes UniformRandomMajScare() {
		/*int rand = Random.Range (1, 3);
		ScareData.majorScareTimes scare;

		if (rand ==1)
			scare = ScareData.majorScareTimes.Scare1;
		else //if (rand == 2)
			scare = ScareData.majorScareTimes.Scare2;
		/*else
			scare = ScareData.majorScareTimes.Scare3;*/

		//return scare;
		return ScareData.majorScareTimes.Scare2;
	}

	ScareData.comedyTimes UniformRandomComedy() {
		int rand = Random.Range (1, 4);
		ScareData.comedyTimes comedy;
	
		if (rand == 1)
			comedy = ScareData.comedyTimes.Comedy1;
		else if (rand == 2)
			comedy = ScareData.comedyTimes.Comedy2;
		else
			comedy = ScareData.comedyTimes.Comedy3;

		return comedy;
	}

	// Specifically for normal distribution with mean 0.5 and range [0, 1], returns data point
	float ReturnNormal() {
		//Box-Muller transform
		float r1 = Random.value;
		float r2 = Random.value;
		float stdNorm = Mathf.Sqrt (-2.0f * Mathf.Log (r1)) * Mathf.Sin (2.0f * Mathf.PI * r2);
		float norm = MEAN + STD_DEV * stdNorm;

		if (norm < 0 || norm > 1)
			norm = ReturnNormal ();
		return norm;
	}

	// Given a probability of a boolean event, returns whether it happens
	bool ReturnBernoulli(float probabilityT) {
		float p = Random.value;
		if (p <= probabilityT) {
			return true;
		}
		return false;
	}

	void UpdateScaredMeter() {
		const float MAX_COUNT = 2;

		lastCameraDirection = camera.transform.forward;
		scareMeterCounter = MAX_COUNT;
	}
}
