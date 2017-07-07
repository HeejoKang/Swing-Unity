using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleMove : MonoBehaviour {

	GameObject player;
	SpringJoint sj;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		sj = GetComponent<SpringJoint> ();
		sj.connectedBody = player.GetComponent<Rigidbody> ();

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
