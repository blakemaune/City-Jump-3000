using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotorRotate : MonoBehaviour {
	public bool tailRotor;
	private Vector3 center;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		center = GetComponent<Renderer> ().bounds.center;
		Vector3 axis;
		if (tailRotor) {
			axis = transform.right;
		} else {
			axis = transform.up;
		}
		transform.RotateAround (center, axis, 20.0f);
	}
}
