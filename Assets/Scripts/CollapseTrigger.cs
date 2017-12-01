using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollapseTrigger : MonoBehaviour {

	void OnCollisionStay(Collision collision)
	{
		Collapse c = collision.gameObject.GetComponent<Collapse> ();
		if (c != null) {
			//Vector3 oldPos = transform.position;
			//transform.SetParent (collision.gameObject.transform, true);
			//transform.position = oldPos;

		 	// transform.parent = collision.gameObject.transform;
			// Debug.Log ("Triggering collapse on " + collision.gameObject.name);
			c.setCollapse(true);
		}
	}

	void OnCollisionExit(Collision collision) {
		Collapse c = collision.gameObject.GetComponent<Collapse> ();
		if (c != null) {
			// Debug.Log ("Cancelling collapse on " + collision.gameObject.name);
			c.setCollapse(false);
			//transform.SetParent (null);
		}
	}

}
