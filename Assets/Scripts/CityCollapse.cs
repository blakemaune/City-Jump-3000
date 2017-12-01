using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityCollapse : MonoBehaviour {
	public GameObject[] buildings;
	private int currentBuilding = 0;
	private float timeUntilCollapse;
	private bool collapseInProgress;
	// Use this for initialization
	void Start () {
		timeUntilCollapse = Time.time + 5f;
		collapseInProgress = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (timeUntilCollapse < Time.time && !collapseInProgress) {
			Debug.Log ("Collapsing Building");
			collapseInProgress = true;
		}

		if (collapseInProgress) {
			Vector3 newPosition = buildings [currentBuilding].transform.position;
			newPosition.y -= .1f;
			buildings [currentBuilding].transform.position = newPosition;
			if (buildings [currentBuilding].transform.position.y <= -2f) {
				collapseInProgress = false;
				timeUntilCollapse = Time.time + 5f;
				currentBuilding++;
			}
		}
	}
}
