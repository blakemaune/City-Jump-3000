using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCollider : MonoBehaviour {

	// Use this for initialization
	void Start () {
		bool hasBounds = false;
		Bounds bounds = new Bounds(Vector3.zero, Vector3.zero);

		for (int i = 0; i < gameObject.transform.childCount; ++i) {
			Renderer childRenderer = gameObject.transform.GetChild(i).GetComponent<Renderer>();
			if (childRenderer != null) {
				if (hasBounds) {
					bounds.Encapsulate(childRenderer.bounds);
				}
				else {
					bounds = childRenderer.bounds;
					hasBounds = true;
				}
			}
		}

		BoxCollider collider = (BoxCollider)gameObject.AddComponent<BoxCollider>();
		collider.center = bounds.center - gameObject.transform.position;
		collider.size = bounds.size;
	}
}
