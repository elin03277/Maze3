using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//script for player one
public class PlayerOne : MonoBehaviour {
	//speed for this paddle
	public float speed = 9f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame. move paddle one vertically
	void Update () {
		transform.Translate (0f, Input.GetAxis ("Vertical") * speed * Time.deltaTime, 0f);
	}
}
