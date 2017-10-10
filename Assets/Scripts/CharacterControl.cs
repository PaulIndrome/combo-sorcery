using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour {

	public float speed;
	public float jumpHeight;
	public float characterGravity;
	public float jumpGravityScale;

	Vector3 velocity;
	CharacterController charCon;

	// Use this for initialization
	void Start () {
		charCon = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {

		if(charCon.isGrounded){
			velocity.y = 0;
			if(Input.GetButtonDown("Jump")){
				velocity.y = jumpHeight;
			}
		}
		if(Input.inputString.Contains("Key")){
			velocity.x = Input.GetAxis("HorizontalKey") * speed;
			velocity.z = Input.GetAxis("VerticalKey") * speed;
		} else {
			velocity.x = Input.GetAxis("HorizontalStick") * speed;
			velocity.z = Input.GetAxis("VerticalStick") * speed;
		}
		
		velocity.y += characterGravity * Time.deltaTime * ((Input.GetButton("Jump") && velocity.y > 0) ? jumpGravityScale : 1);
		
		charCon.Move(velocity * Time.deltaTime);
		transform.rotation = (Input.GetAxis("HorizontalStick") != 0 || Input.GetAxis("VerticalStick") != 0 || Input.GetAxis("VerticalKey") != 0 || Input.GetAxis("HorizontalKey") != 0) ? 
			Quaternion.LookRotation(new Vector3(velocity.x, 0, velocity.z)) : transform.rotation;
	}
}
