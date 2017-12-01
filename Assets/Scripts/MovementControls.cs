using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MovementControls : MonoBehaviour {
	private Rigidbody rb;
	private Camera c;
	private float default_walkSpeed;
	private float default_jumpForce;
	private float distToGround;
	public float frictionForce;
	public float jumpForce;
	public float walkSpeed;
	public bool initLanding;
	public bool airControl;
	public Slider jumpIndicator;

	private AudioSource audioSource;
	public AudioClip jump;
	public AudioClip charge;
	public AudioClip chargeHold;
	public AudioClip landing;

	// Use this for initialization
	void Start () {
		Debug.Log ("Setting slider values");
		jumpIndicator = jumpIndicator.GetComponent<Slider> ();
		jumpIndicator.minValue = default_jumpForce;
		jumpIndicator.maxValue = default_jumpForce * 5;


		rb = GetComponent<Rigidbody> ();



		audioSource = GetComponent<AudioSource> ();
		c = Camera.main;
		default_jumpForce = jumpForce;
		default_walkSpeed = walkSpeed;

		distToGround = GetComponent<CapsuleCollider>().bounds.extents.y;
	}
	
	// Update is called once per frame
	void Update () {
		// Sprint functionality
		if (Input.GetKey (KeyCode.LeftShift) && IsGrounded()) {
			walkSpeed = 1.5f * default_walkSpeed;
		}else {walkSpeed = default_walkSpeed;}

		if (IsGrounded () || airControl) {
			// WASD Movement
			Vector3 curVel = rb.velocity;
			Vector3 newVel = new Vector3 ();
			if (Input.GetKey (KeyCode.W)) {
				// rb.AddRelativeForce (Vector3.forward * 1.0f, ForceMode.VelocityChange);
				newVel += transform.forward * walkSpeed;
			}	
			if (Input.GetKey (KeyCode.A)) {
				// rb.AddRelativeForce (Vector3.left * 1.0f, ForceMode.VelocityChange);
				newVel += transform.right * -walkSpeed;

			}
			if (Input.GetKey (KeyCode.S)) {
				// rb.AddRelativeForce (Vector3.back * 1.0f, ForceMode.VelocityChange);
				newVel += transform.forward * -walkSpeed;
			}

			if (Input.GetKey (KeyCode.D)) {
				// rb.AddRelativeForce (Vector3.right * 1.0f, ForceMode.VelocityChange);
				newVel += transform.right * walkSpeed;
			}
			newVel = new Vector3 (newVel.x, curVel.y, newVel.z);
			rb.velocity = newVel;

			// DEBUG
			// Debug.Log ("Grounded: " + IsGrounded ());

			/**
			// Jumping
			if (Input.GetKey (KeyCode.Space)) {
				rb.AddForce (Vector3.up * jumpForce, ForceMode.Impulse);
			}
			if (Input.GetKey (KeyCode.Space)) {
				jumpForce += 1.0f;
			} else {
				jumpForce = default_jumpForce;
			}
			**/

			// Test New Jump
			if (IsGrounded ()) {
				if (Input.GetKey (KeyCode.Space) && Input.GetKey (KeyCode.LeftShift)) {
					// Jump limit
					//THIS IS WHERE CHARGE JUMP HAPPENS
					if (jumpForce <= 5f * default_jumpForce) {
						jumpForce += 1.0f;
						if (!audioSource.isPlaying) {
							audioSource.clip = chargeHold;
							audioSource.Play ();
						}
						audioSource.pitch = .7f * jumpForce / (5.0f * default_jumpForce);
					}
				} else if (Input.GetKeyDown (KeyCode.Space)) {
					rb.AddForce (Vector3.up * jumpForce, ForceMode.Impulse);
					jumpForce = default_jumpForce;

					audioSource.Stop ();
					audioSource.clip = jump;
					audioSource.Play ();
				} else if (Input.GetKeyUp (KeyCode.Space)  && jumpForce != default_jumpForce) {
					rb.AddForce (Vector3.up * jumpForce, ForceMode.Impulse);
					jumpForce = default_jumpForce;

					audioSource.Stop ();
					audioSource.clip = jump;
					audioSource.Play ();
				}
			}

			/**
			Stop if no key held down
			if (!Input.anyKey) {
				rb.velocity = new Vector3 (frictionForce * rb.velocity.x, rb.velocity.y, frictionForce * rb.velocity.z);
			}
			**/
		}
		if (!IsGrounded ()) {
			// Slow adjustability
			walkSpeed = 0.25f *default_walkSpeed;

			// Ground pound
			if (Input.GetKeyDown (KeyCode.Space)) {
				rb.velocity = new Vector3 (
					rb.velocity.x,
					Mathf.Min(Mathf.Abs(rb.velocity.y) * -1.05f, -35.0f),
					rb.velocity.z
				);
			}
		}
	
		jumpIndicator.minValue = default_jumpForce;
		jumpIndicator.maxValue = 5.0f * default_jumpForce;
		jumpIndicator.value = jumpForce;
	}

	private bool IsGrounded(){
		RaycastHit ground;
		bool grounded = Physics.Raycast (transform.position, Vector3.down, out ground, distToGround + 1.0f);
		if (grounded && !initLanding) {
			initLanding = true;
			Timer t = GameObject.Find ("Timer").GetComponent<Timer> ();
			t.TogglePause ();
		}
		return grounded;
	}
}
