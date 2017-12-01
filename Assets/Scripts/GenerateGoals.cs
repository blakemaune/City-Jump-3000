using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateGoals : MonoBehaviour {
	public int goalCount;
	public GameObject goal;

	// Use this for initialization
	void Start () {
		MapGenerator mg = GameObject.FindGameObjectWithTag ("MapSpawner").GetComponent<MapGenerator>();
		for (int i = 0; i < goalCount; i++) {
			Instantiate (goal, new Vector3 (100*Random.Range(0, mg.width), 0, 100*Random.Range(0, mg.height)), goal.transform.rotation);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
