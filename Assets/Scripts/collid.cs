using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collid : MonoBehaviour {

	void OnCollisionEnter(Collision collision)
	{
		Destroy (gameObject);
	}
}
