using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpRangeFinder : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void setSize(float scale){
		transform.localScale = scale * Vector3.one;
	}
}
