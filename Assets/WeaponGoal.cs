using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponGoal : MonoBehaviour {
	public GameObject weapon;

	void Start(){
		weapon = gameObject.transform.GetChild (0).gameObject;
	}

	void OnTriggerEnter(Collider collision)
	{
		GameObject other = collision.gameObject;
		if (other.tag == "Player") {
			WeaponHolder holder = other.GetComponent<WeaponHolder> ();
			if (holder == null) {
				Debug.Log ("No holder found");
			}

			// Check if the player is already armed
			//if (holder.armed) {
				// Player already has weapon
			//} else {
				// Player doesn't already have weapon
			weapon = Instantiate(weapon);
			weapon.transform.SetParent(other.transform);
			weapon.transform.localPosition = new Vector3 (.5f, 0.0f, 1.25f); // Euler(Vector3 (20f, -90.0f, 0.3f)
			weapon.transform.localRotation = Quaternion.Euler(20f, -90.0f, 0.3f);
			weapon.transform.localScale = Vector3.one;
			// Vector3 newPos = other.transform.position;
			// weapon.transform.localScale = Vector3.one;
			// weapon.transform.rotation = other.transform.rotation;
			// weapon.transform.position = newPos;
			// weapon.transform.position += 1.0f * other.transform.forward;
			Destroy (weapon.GetComponent<Animator> ());

			holder.armed = true;
			holder.weapon = weapon;
			// }

			Destroy (gameObject);
		}
	}
}
