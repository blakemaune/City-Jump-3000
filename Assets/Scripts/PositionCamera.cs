using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionCamera : MonoBehaviour {
	public MapGenerator mg;
	private int width, height;
	// Use this for initialization
	void Start () {
		mg = GameObject.FindGameObjectWithTag ("MapSpawner").GetComponent<MapGenerator>();
		width = mg.width;
		height = mg.height;
		float z = (100f * width)/2f - 50f;
		float x = (100f * height)/2f - 50f;
		float y = width*100f;
		transform.position = new Vector3 (x, y, z);
	}
}
