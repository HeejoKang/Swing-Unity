using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook3 : MonoBehaviour {

	GameObject target;
	GameObject character;
	public GameObject grappleprop;
	Rigidbody charbod;
	public float pullstrength;
	HingeJoint grapple;
	Vector3 linepos, targspot;

	// Draws lines
	void DrawLine(Vector3 start, Vector3 end, Color color, float duration = 0.2f)
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
		GameObject.Destroy(myLine, duration);
	}


	// Use this for initialization
	void Start () {
		character = this.transform.parent.gameObject;
		charbod = character.GetComponent<Rigidbody> ();
	}

	// Update is called once per frame
	void Update () {

		linepos.Set (transform.position.x, 
			transform.position.y - 0.5f, transform.position.z);


		if (Input.GetKeyDown (KeyCode.Space)) {

			charbod.AddForce (Vector3.up * 300f);

		}

		if(Input.GetKeyDown(KeyCode.Mouse0)){

			Ray ray = new Ray(transform.position, transform.forward);
			RaycastHit hit;
			if(Physics.Raycast(ray, out hit, 100f)){

				target = hit.transform.gameObject;
				DrawLine (linepos, hit.point, Color.blue);
				Instantiate (grappleprop);
				grappleprop.transform.position = hit.point;
				
				Debug.Log (hit.distance);
			}

		}

	}
}
