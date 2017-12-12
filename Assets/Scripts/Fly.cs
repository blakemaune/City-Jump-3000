using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : MonoBehaviour {
	private GameObject player;
	public float speed;
	public GameObject target;
	private MapGenerator mg;
	public AudioClip explode;


	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		target = SelectTarget();
		mg = GameObject.FindGameObjectWithTag ("MapSpawner").GetComponent<MapGenerator>();
	}
	
	// Update is called once per frame
	void Update () {
		if (target == null) {
			target = SelectTarget ();
		}
		// Debug.Log ("Distance to target " + target.name + ": " + Vector3.Distance (transform.position, target.transform.position));
		if (Vector3.Distance (transform.position, target.transform.position) > 200) {
			// Debug.Log ("Moving helicopter");
			Move ();
		} else {
			Destroy (target);
			target = SelectTarget ();
			mg.NewGoal ();
		}
	}

	void OnCollisionEnter(Collision collision)
	{
		Debug.Log ("Detected Collision");
		if (collision.gameObject.tag == "Projectile") {
			Debug.Log ("Hit with weapon!");
			Destroy (collision.gameObject);

			MapGenerator mg = GameObject.FindGameObjectWithTag ("MapSpawner").GetComponent<MapGenerator>();


			GetComponent<AudioSource> ().clip = explode;
			GetComponent<AudioSource> ().Play ();

			GetComponent<Rigidbody> ().AddExplosionForce (50f, gameObject.transform.position, 15f);
			GameObject.Find ("ScoreText").GetComponent<KeepScore> ().score += 2;

			StartCoroutine (SpawnDelay ());

			GameObject newHeli = Instantiate(mg.helicopter);
			Fly f = newHeli.GetComponent<Fly> ();
			f.speed *= 1.5f * speed;

			speed = 0f;
			Destroy (gameObject, 2.5f);
		}
	}

	IEnumerator SpawnDelay()
	{
		print(Time.time);
		yield return new WaitForSeconds(10);
		print(Time.time);
	}

	void Move(){
		
		// Vector3 newDir = Vector3.RotateTowards(transform.forward, target.transform.position, speed, 0.0F);
		// transform.rotation = Quaternion.LookRotation(newDir);
		// transform.rotation = Quaternion.RotateTowards (transform.rotation, target.transform.rotation, speed);


		Vector3 targetPos = new Vector3 (target.transform.position.x, transform.position.y, target.transform.position.z);
		Vector3 newPos = Vector3.MoveTowards (transform.position, targetPos, speed);
		transform.LookAt (targetPos);
		transform.position = newPos;
	}

	void Shoot(){

	}

	void MoveToTarget(){
		Vector3 newPos = Vector3.MoveTowards (transform.position, target.transform.position, speed);
		newPos = new Vector3 (newPos.x, transform.position.y, newPos.z);
		transform.position = newPos;
	}

	GameObject SelectTarget(){
		GameObject target;
		GameObject[] options = GameObject.FindGameObjectsWithTag ("Goal");

		int index = Random.Range (0, options.Length);
		target = options [index];
		return target;
	}
}
