using UnityEngine;
using System.Collections;

public class GenericMinorScare : MonoBehaviour {

	public GameObject player;

	private Vector3 goalPosition;

	// Use this for initialization
	void Start () {
		goalPosition = transform.position;
		float xDiff = (player.transform.position.x - transform.position.x);
		float zDiff = (player.transform.position.z - transform.position.z);

		transform.position.Set (player.transform.position.x + xDiff, transform.position.y, player.transform.position.z + zDiff);
	}
	
	// Update is called once per frame
	void Update () {
		//transform.RotateAround (player.transform.position, Vector3.up, 20 * Time.deltaTime);
	}
}
