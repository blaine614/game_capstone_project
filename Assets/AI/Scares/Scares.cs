using UnityEngine;
using System.Collections;
using UnityEditor;

public class Scares : MonoBehaviour {

	new public Camera camera;

	private const string MIN_SCARE_NAMES = "Min";
	private const string MAJ_SCARE_NAMES = "Maj";

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		/*ArrayList remove = new ArrayList ();
		foreach (GameObject scare in activeScares) {
			scare.GetComponent<ScareData>().Timer -= Time.deltaTime;
			if (scare.GetComponent<ScareData>().Timer <= 0 && scare.activeSelf) {
				scare.SetActive(false);
				remove.Add(scare);
				//Debug.Log("finishing stage...");
				SendMessage("FinishStage", scare.name);
			}
		}
		foreach (GameObject scare in remove) {
			activeScares.Remove(scare);
		}
		remove.Clear ();*/
	}

	void ActivateMinor(ScareData.minorScareTimes scareNum) {
		GeneralActivation (AIDirector.stage.minorScare, scareNum, ScareData.majorScareTimes.Scare1, ScareData.comedyTimes.Comedy1);
	}

	void ActivateMajor (ScareData.majorScareTimes scareNum) {
		GeneralActivation (AIDirector.stage.majorScare, ScareData.minorScareTimes.Scare1, scareNum, ScareData.comedyTimes.Comedy1);
	}

	void ActivateComedy (ScareData.comedyTimes comedyNum) {
		GeneralActivation (AIDirector.stage.comedy, ScareData.minorScareTimes.Scare1, ScareData.majorScareTimes.Scare1, comedyNum);
	}

	void GeneralActivation (AIDirector.stage stage, ScareData.minorScareTimes minScare, ScareData.majorScareTimes majScare, ScareData.comedyTimes comedy) {
		string prefabPath = "Assets/AI/Scares/";
		string timerMethod = "Set";
		switch (stage) {
		case AIDirector.stage.minorScare:
			prefabPath += "MinorScaresPrefabs/" + MIN_SCARE_NAMES + minScare.ToString();
			timerMethod += "Min";
			break;
		case AIDirector.stage.majorScare:
			prefabPath += "MajorScaresPrefabs/" + MAJ_SCARE_NAMES + majScare.ToString();
			timerMethod += "Maj";
			break;
		case AIDirector.stage.comedy:
			prefabPath += "ComedyPrefabs/" + comedy.ToString();
			timerMethod += "Com";
			break;
		}
		prefabPath += ".prefab";
		timerMethod += "TimerAndType";
		Vector3 scarePosition = new Vector3 (0, 0, 0);
		Quaternion scareRotation = new Quaternion (0, 0, 0, 0);
		GameObject scare = new GameObject ();
		Object prefab = AssetDatabase.LoadAssetAtPath (prefabPath, (typeof(GameObject))) as GameObject;

		scare = (GameObject)Instantiate (prefab, scarePosition, scareRotation);
		scare.AddComponent<ScareData> ();
		switch (stage) {
		case AIDirector.stage.minorScare:
			scare.GetComponent<ScareData> ().SendMessage (timerMethod, minScare, SendMessageOptions.DontRequireReceiver);
			break;
		case AIDirector.stage.majorScare:
			scare.GetComponent<ScareData> ().SendMessage (timerMethod, majScare, SendMessageOptions.DontRequireReceiver);
			break;
		case AIDirector.stage.comedy:
			scare.GetComponent<ScareData> ().SendMessage (timerMethod, comedy, SendMessageOptions.DontRequireReceiver);
			break;
		default:
			break;
		}
		scare.transform.position = FindScarePosition (scare.GetComponent<ScareData> ().Type);
		CreateHierarchy (scare);
		//activeScares.Add (scare);
	}

	void CreateHierarchy(GameObject scare) {
		switch (scare.GetComponent<ScareData>().Type) {
		case ScareData.scareTypes.ceiling:
			scare.transform.parent = gameObject.transform;
			break;
		case ScareData.scareTypes.player:
			scare.transform.parent = gameObject.transform;
			break;
		default:
			break;
		}
	}

	Vector3 FindScarePosition(ScareData.scareTypes scareType) {
		const float SCARE_DEPTH = 4;
		const float FAR_SCARE_DEPTH = 10;
		const float SCARE_VARIANCE = 5;
		//const float HEIGHT_ERROR = 1.4f;
		float scareHeight = camera.transform.position.y;
		Vector3 cameraPosition = camera.transform.position;
		Vector3 scarePosition = cameraPosition;
		Vector3 cameraDirection = camera.transform.forward;

		switch (scareType) {
		case ScareData.scareTypes.jump:
			scarePosition = cameraPosition + cameraDirection * SCARE_DEPTH;
			break;
		case ScareData.scareTypes.door:
			scarePosition = new Vector3(Random.value, Random.value, Random.value) * SCARE_VARIANCE;
			scarePosition += cameraDirection * SCARE_DEPTH;
			break;
		case ScareData.scareTypes.player:
			scarePosition = cameraPosition;
			break;
		case ScareData.scareTypes.ceiling:
			scarePosition = cameraPosition;
			scarePosition.y += 2;
			break;
		case ScareData.scareTypes.far:
			scarePosition = new Vector3(Random.value, Random.value, Random.value) * SCARE_VARIANCE;
			scarePosition += cameraDirection * FAR_SCARE_DEPTH;
			break;
		case ScareData.scareTypes.behind:
			scarePosition = cameraPosition - cameraDirection;// * SCARE_DEPTH;
			break;
		default:
			break;
		}

		scarePosition.y = scareHeight;

		return scarePosition;
	}
}
