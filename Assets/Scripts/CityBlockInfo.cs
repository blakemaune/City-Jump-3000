using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityBlockInfo : MonoBehaviour {
	public bool unique;
	public bool exists;
	public float height;

	// Use this for initialization
	void Start () {
		float maxHeight = 0;
		foreach (Transform child in transform) {
			if (child.gameObject.GetComponent<Renderer> () != null) {
				maxHeight = Mathf.Max(maxHeight, child.gameObject.GetComponent<Renderer> ().bounds.size.y);
			}
		}
		height = maxHeight;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
