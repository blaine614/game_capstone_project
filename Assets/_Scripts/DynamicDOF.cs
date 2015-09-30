using UnityEngine;
using System.Collections;

//https://www.youtube.com/watch?v=d1GnMVKkinQ 

public class DynamicDOF : MonoBehaviour {
    public Transform origin;
    public GameObject target;
	
	// Update is called once per frame
	void Update () {
        Ray ray = new Ray(origin.transform.position, origin.transform.forward);
        RaycastHit hit = new RaycastHit();

        if(Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            target.transform.position = hit.point;
        }
	}
}
