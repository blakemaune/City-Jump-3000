using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectGoal : MonoBehaviour {
	public Text scoreboard;
	public Text timeText;
	private Timer time;
	public MapGenerator mg;

	void Start(){
		mg = GameObject.FindGameObjectWithTag ("MapSpawner").GetComponent<MapGenerator>();
		time = timeText.GetComponent<Timer> ();
	}


	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Goal") {
			Debug.Log ("Poof");
			scoreboard.GetComponent<KeepScore> ().score++;
			time.AddTime (10.0f);
			mg.NewGoal ();
			other.gameObject.GetComponent<Goal> ().PlaySuccess ();
			Destroy (other.gameObject, 1.0f);
		}
	}
}
