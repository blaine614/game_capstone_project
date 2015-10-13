using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;
using UnityStandardAssets.Characters.FirstPerson;

public class SanityMeter : MonoBehaviour {

	private float sanity = 100.0f;
	public float sanityLoseSpeed = 0.5f;
    public int deathRotateSpeed = 50;
    private SanityFisheye warp;
    private float warpAffect = 0;
    private NoiseAndGrain color;
    private float colorAffect = 0;
    private RigidbodyFirstPersonController controller;
     

    // Use this for initialization
    void Start () {
        warp = gameObject.GetComponent<SanityFisheye>();
        color = gameObject.GetComponent<NoiseAndGrain>();
        controller = gameObject.GetComponentInParent<RigidbodyFirstPersonController>();
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
        if(sanity <= 0.0f)
        {
            Die();
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

		Debug.Log ("Sanity: " + sanity);
	}

    public void IncreaseSanity(float increase)
    {
        sanity += increase;
    }

    void Die()
    {
        if (controller.enabled) {
            //transform.Rotate(0, 0, 90);
            controller.enabled = false;
            //Debug.Log(transform.rotation);
        }

        if(transform.localRotation.eulerAngles.z < 90)
        {
            transform.Rotate(Vector3.forward, Time.deltaTime * deathRotateSpeed, Space.World);
        }
        else
        {
            Application.LoadLevel(Application.loadedLevel);
        }

    }

}
