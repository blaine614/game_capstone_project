using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class SanityMeter : MonoBehaviour {

	private float sanity = 100.0f;
	public float sanityLoseSpeed = 5.0f;
    private SanityFisheye warp;
    private float warpAffect = 0;
    private NoiseAndGrain color;
    private float colorAffect = 0;

	// Use this for initialization
	void Start () {
        warp = gameObject.GetComponent<SanityFisheye>();
        color = gameObject.GetComponent<NoiseAndGrain>();
	}
	
	// Update is called once per frame
	void Update () {
		//float numberLost = Time.deltaTime * sanityLoseSpeed;
		if (sanity > 0.0f) {
			sanity -= Time.deltaTime * sanityLoseSpeed;
		}
        if (sanity > 100.0f)
        {
            sanity = 100.0f;
        }
        if(sanity < 0.0f)
        {
            sanity = 0.0f;
        }

        
		warpAffect = sanity / 100.0f;
        warpAffect *= .75f;
        warpAffect = .75f - warpAffect;
        warp.strengthX = warpAffect;
        warp.strengthY = warpAffect;
        
        colorAffect = sanity / 100.0f;
        colorAffect *= 5.0f;
        colorAffect = 5.0f - colorAffect;
        color.intensityMultiplier = colorAffect;

		//Debug.Log ("numberLost: " + numberLost);
		//Debug.Log ("warpingAffect: " + visionAffect);
		Debug.Log ("Sanity: " + sanity);
	}

    public void IncreaseSanity(float increase)
    {
        sanity += increase;
    }

}
