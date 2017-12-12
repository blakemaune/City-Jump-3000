using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder : MonoBehaviour {
	public bool armed;
	public GameObject weapon;
	public AudioClip shoot;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (armed && Input.GetMouseButtonDown(0)) {
			fire ();
		}
		
	}

	void fire(){
		GameObject projectile;
		projectile = weapon.transform.GetChild (3).gameObject;
		projectile.transform.SetParent (null);
		Debug.Log ("Weapon is: " + projectile.name);
		Rigidbody rb = projectile.GetComponent<Rigidbody> ();
		rb.isKinematic = false;
		rb.velocity = Camera.main.transform.forward * 50.0f;
		Debug.Log ("Firing Weapon");
		Destroy (weapon);
		MapGenerator mg = GameObject.FindGameObjectWithTag ("MapSpawner").GetComponent<MapGenerator>();
		mg.NewWeaponGoal ();

		GetComponent<AudioSource> ().clip = shoot;
		GetComponent<AudioSource> ().Play ();
	}
}
