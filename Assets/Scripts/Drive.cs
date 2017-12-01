using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drive : MonoBehaviour {
	private MapGenerator mg;
	private Vector3[,] corners;
	public Vector3 destination;
	public Vector3 location;
	public float speed;

	// Use this for initialization
	void Start () {
		mg = GameObject.FindGameObjectWithTag ("MapSpawner").GetComponent<MapGenerator>();
		corners = mg.corners;
		destination = SelectDestination ();
		// Debug.Log ("Driving towards destination");
		speed = (Random.value / 2) + 1.0f;
	}
	
	// Update is called once per frame
	void Update () {
		Move ();
	}

	void Move(){
		Vector3 targetDir = destination - transform.position;
		Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, 1.0f, 0.0F);
		transform.rotation = Quaternion.LookRotation(newDir);

		transform.position = Vector3.MoveTowards (transform.position, destination, speed);
		if (transform.position == destination) {
			location = destination;
			destination = SelectDestination ();
		}
	}

	Vector3 SelectDestination(){
		bool validDest = false;

		Vector3 destination = Vector3.zero;

		while (validDest == false) {
			int x = Random.Range (0, mg.height);
			int y = Random.Range (0, mg.width);
			Vector3 newDest = corners [x, y];
			if (newDest == location || ((newDest.x != location.x) && (newDest.z != location.z))) {
				continue;
			} else {
				validDest = true;
				destination = newDest;
			}
		}

		return destination;
	}
}
