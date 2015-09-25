using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class SanityMeter : MonoBehaviour {

	private float sanity = 100.0f;
	public float sanityLoseSpeed = 5.0f;
	private Twirl vision;
	private float visionAffect = 0;

	// Use this for initialization
	void Start () {
		vision = gameObject.GetComponent<Twirl> ();
	}
	
	// Update is called once per frame
	void Update () {
		float numberLost = Time.deltaTime * sanityLoseSpeed;
		if (sanity > 0.0f) {
			sanity -= Time.deltaTime * sanityLoseSpeed;
		}
        if (sanity > 100.0f)
        {
            sanity = 100.0f;
        }
		visionAffect = 1 - sanity / 100.0f;
		vision.radius.x = visionAffect;
		vision.radius.y = visionAffect;
		Debug.Log ("numberLost: " + numberLost);
		Debug.Log ("visionAffect: " + visionAffect);
		Debug.Log ("Sanity: " + sanity);
	}

    public void IncreaseSanity(float increase)
    {
        sanity += increase;
    }

}
