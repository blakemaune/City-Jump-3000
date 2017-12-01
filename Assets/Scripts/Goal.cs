using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Goal : MonoBehaviour {
	private MapGenerator mg;
	private AudioSource audio;
	public AudioClip idle;
	public AudioClip success;

	// Use this for initialization
	void Start () {
		mg = GameObject.FindGameObjectWithTag ("MapSpawner").GetComponent<MapGenerator>();
		audio = GetComponent<AudioSource> ();
		PlayIdle ();
	}

	void PlayIdle(){
		audio.volume = .5f;
		audio.clip = idle;
		audio.loop = true;
		audio.Play ();
	}

	public void PlaySuccess(){
		audio.volume = 1.0f;
		audio.Stop ();
		audio.clip = success;
		audio.loop = true;
		audio.Play ();
	}

	void OnCollisionStay(Collision collision)
	{
		if (collision.gameObject.tag == "Goal") {
			Debug.Log ("Collided with goal");
			mg.NewGoal ();
			Destroy (gameObject);
		}
	}
}
