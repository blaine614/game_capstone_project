using UnityEngine;
using System.Collections;

//From https://www.youtube.com/watch?v=b3FWw0rfKxY

public class LoadingScreenScript : MonoBehaviour {

	public string levelToLoad;

	public GameObject background;
	public GameObject text;
	public GameObject progressBar;

	// Use this for initialization
	void Start () {
		background.SetActive (false);
		text.SetActive (false);
		progressBar.SetActive (false);

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("space")) {
			StartCoroutine(DisplayLoadingScreen());
		}
	}

	IEnumerator DisplayLoadingScreen(){
		background.SetActive (true);
		text.SetActive (true);
		progressBar.SetActive (true);

		progressBar.transform.localScale = new Vector3 (0, progressBar.transform.localScale.y, progressBar.transform.localScale.z);

		AsyncOperation async = Application.LoadLevelAsync (levelToLoad);
		while (!async.isDone) {
			progressBar.transform.localScale = new Vector3(async.progress, progressBar.transform.localScale.y, progressBar.transform.localScale.z);
			yield return null;
		}
	}
}
