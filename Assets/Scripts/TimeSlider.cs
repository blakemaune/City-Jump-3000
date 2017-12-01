using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeSlider : MonoBehaviour {
	public Text indicator;

	// Update is called once per frame
	void Update () {
		Timer t = GameObject.Find ("Timer").GetComponent<Timer> ();
		if (t != null) {
			float time = GetComponent<Slider> ().value;
			t.maxTime = time;
			t.ResetTime ();
			indicator.text = time.ToString ();
		}
	}
}
