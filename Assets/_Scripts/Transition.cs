using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;
using UnityStandardAssets.Characters.FirstPerson;


public class Transition : MonoBehaviour {

	public int level;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator Fade()
	{
		FirstPersonController controller = gameObject.GetComponent<FirstPersonController>();
		if (controller.enabled) {
			controller.enabled = false;
		}

		float fadeTime = gameObject.GetComponent<Fading>().BeginFade(1);
		yield return new WaitForSeconds(fadeTime);
		Application.LoadLevel (level);
	}

	public void Change() {
		StartCoroutine (Fade ());
	}
}
