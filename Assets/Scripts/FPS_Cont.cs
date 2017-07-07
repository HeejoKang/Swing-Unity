using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPS_Cont : MonoBehaviour {

	public float speed;

	// Use this for initialization
	void Start () {

		Cursor.lockState = CursorLockMode.Locked;
		
	}
	
	// Update is called once per frame
	void Update () {

		float transl = Input.GetAxis ("Vertical") * speed;
		float strafe = Input.GetAxis ("Horizontal") * speed;

		transl *= Time.deltaTime;
		strafe *= Time.deltaTime;

		transform.Translate (strafe, 0, transl);

		if(Input.GetKeyDown(KeyCode.Escape)){
			Cursor.lockState = CursorLockMode.None;
		}
		
	}
}
