using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour {

	GameObject target;
	GameObject character;
	Rigidbody charbod;
	public float pullstrength;
	bool moving;

	void moveTowardsTarget(GameObject targ, float mindist, float strength){

		if (Vector3.Distance (transform.position, targ.transform.position) > mindist) {
			moving = true;
			transform.LookAt (targ.transform);
			charbod.AddRelativeForce (transform.forward * strength, ForceMode.Force);

		} else {
			moving = false;
		}

	}

	// Use this for initialization
	void Start () {
		character = this.transform.parent.gameObject;
		charbod = character.GetComponent<Rigidbody> ();
		moving = false;
	}
	
	// Update is called once per frame
	void Update () {

		if (moving) {
			moveTowardsTarget (target, 50f, pullstrength);
		}

		if(Input.GetKeyDown(KeyCode.Mouse0)){

			Ray ray = new Ray(transform.position, transform.forward);
			RaycastHit hit;
			if(Physics.Raycast(ray, out hit, 100)){
				target = hit.transform.gameObject;

				moveTowardsTarget (target, 10f, pullstrength);

				Debug.Log (hit.distance);
			}
			
		}
		
	}
}
