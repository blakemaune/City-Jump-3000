using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantAccelerator : MonoBehaviour {
	private Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		rb.velocity = Vector3.forward * 1.0f;
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log ("Accelerating");
		rb.velocity *= 1.01f;
	}
}
