﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook5 : MonoBehaviour {

	GameObject target;
	GameObject character;
	GameObject rope;
	GameObject camera; // Need this to cast ray forward
	LineRenderer roperender;
	Rigidbody charbod;
	public float pullstrength;
	bool flying;
	Vector3 linepos;
	RaycastHit hit;
	float grapplecd, timer;


	// Draws lines
	GameObject drawLine(Vector3 start, Vector3 end, Color color, float duration = 0.2f)
	{
		GameObject myLine = new GameObject();
		myLine.transform.position = start;
		myLine.AddComponent<LineRenderer>();
		LineRenderer lr = myLine.GetComponent<LineRenderer>();
		lr.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
		lr.startColor = color;
		lr.endColor = color;
		lr.startWidth = 0.1f;
		lr.endWidth = 0.1f;
		lr.SetPosition(0, start);
		lr.SetPosition(1, end);
		return myLine;
		//GameObject.Destroy(myLine, duration);
	}

	// Use this for initialization
	void Start () {
		character = this.gameObject;
		charbod = character.GetComponent<Rigidbody> (); // Getting this rigidbody instead of from parent
		camera = this.gameObject.transform.GetChild (0).gameObject; // long one-liner to get the actual camera game object
		flying = false;
		grapplecd = 1f;
		timer = 0f;
	}

	void FixedUpdate(){

		if (timer < 0) {

			if (Input.GetKeyDown (KeyCode.Mouse0)) {

				GameObject.Destroy (rope);

				Ray ray = new Ray (transform.position, camera.transform.forward); // Changed this to camera.transform.forward but still bad crosshair bug
				if(Physics.Raycast(ray, out hit, 100)){

					charbod.AddForce (Vector3.up * 300f);
					flying = true;			

					target = hit.transform.gameObject;
					rope = drawLine (linepos, hit.point, Color.blue);
					roperender = rope.GetComponent<LineRenderer>();

					timer = grapplecd;

					Debug.Log (hit.distance);
				}

			}

		}

	}

	// Update is called once per frame
	void Update () {

		timer -= Time.deltaTime;

		linepos.Set (transform.position.x, 
			transform.position.y - 0.5f, transform.position.z);

		if (flying) {
			if (hit.transform!=null) {
				float dist = Vector3.Distance (hit.transform.position, transform.position);
				if (dist > 10f) {
					charbod.AddForce ((hit.transform.position - transform.position).normalized * pullstrength * Time.smoothDeltaTime);
					roperender.SetPosition (0, linepos);
					roperender.SetPosition (1, hit.point);
				} else {
					GameObject.Destroy (rope);
					flying = false;
				}
			}
		}

		if (Input.GetKeyDown (KeyCode.Space)) {

			charbod.AddForce (Vector3.up * 300f);

		}

		if (Input.GetKeyDown (KeyCode.Mouse1)) {

			flying = false;
			GameObject.Destroy (rope);

		}

		/*if (timer < 0) {

			if (Input.GetKeyDown (KeyCode.Mouse0)) {

				GameObject.Destroy (rope);

				Ray ray = new Ray (transform.position, camera.transform.forward); // Changed this to camera.transform.forward but still bad crosshair bug
				if(Physics.Raycast(ray, out hit, 100)){

					charbod.AddForce (Vector3.up * 300f);
					flying = true;			

					target = hit.transform.gameObject;
					rope = drawLine (linepos, hit.point, Color.blue);
					roperender = rope.GetComponent<LineRenderer>();

					timer = grapplecd;

					Debug.Log (hit.distance);
				}

			}

		}*/

	}
}
