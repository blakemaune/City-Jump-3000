using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collapse: MonoBehaviour {
	public bool collapsing;

	// Use this for initialization
	void Start () {
		collapsing = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (collapsing) {
			Vector3 pos = transform.position;
			pos.y -= .1f;
			transform.position = pos;
		}
	}

	public void setCollapse(bool b){
		collapsing = b;
	}
}
