using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//script for player two
public class PlayerTwo : MonoBehaviour {
	//speed for player two paddle
	public float speed = 9f;
	//bool which determines if ai is toggled
	public bool isPlayer;
	float floor;
	float roof;

	GameObject ball;
	Vector2 ballPos;

	// Use this for initialization
	void Start () {
		ball = GameObject.FindGameObjectWithTag ("Ball");
		floor = GameObject.FindGameObjectWithTag ("Floor").transform.position.y;
		roof = GameObject.FindGameObjectWithTag ("Roof").transform.position.y;
	}
	
	// Update is called once per frame. p key or joystickbutton2 (o) toggles paddle 2 AI
	//moves the paddle vertically
	void Update () {
		if (Input.GetKeyUp (KeyCode.P) || Input.GetKeyUp (KeyCode.JoystickButton2)) {
			isPlayer = !isPlayer;
		}
		if (isPlayer)
			transform.Translate (0f, Input.GetAxis ("Vertical2") * speed * Time.deltaTime, 0f);
		else
			Move ();
	}

	//movement for the AI. AI follows the ball's y position
	void Move(){
		ballPos = ball.transform.localPosition;
		if (transform.localPosition.y > floor && ballPos.y < transform.localPosition.y) {
			transform.localPosition += new Vector3 (0f, -speed * Time.deltaTime, 0f);
		}

		if (transform.localPosition.y < roof && ballPos.y > transform.localPosition.y) {
			transform.localPosition += new Vector3 (0f, speed * Time.deltaTime, 0f);
		}
	}
}