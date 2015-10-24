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
		if (sanity > 0.0f) {
			sanity -= Time.deltaTime * sanityLoseSpeed;
		}
        if (sanity > 100.0f)
        {
            sanity = 100.0f;
        }
        if(sanity <= 0.0f)
        {
            StartCoroutine(Die());
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
	}

    public void IncreaseSanity(float increase)
    {
        sanity += increase;
    }

    IEnumerator Die()
    {
        if (controller.enabled) {
            controller.enabled = false;
        }

        if(transform.localRotation.eulerAngles.z < 90 || transform.localRotation.eulerAngles.z > 270)
        {
            transform.Rotate(Vector3.forward, Time.deltaTime * deathRotateSpeed, Space.World);
        }
        else
        {
            float fadeTime = gameObject.GetComponent<Fading>().BeginFade(1);
            yield return new WaitForSeconds(fadeTime);
            Application.LoadLevel(Application.loadedLevel);
        }

    }

}
