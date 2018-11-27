using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script for the ball
public class Ball : MonoBehaviour {

	//base speed of the ball
	float speed = 7.5f;
	//increases base speed by this number
    float iSpeed = 1.001f;

    Rigidbody rb;
	private Game game;

	// Use this for initialization
	void Start () {
		game = GameObject.Find ("Game").GetComponent<Game> ();
        rb = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
		//increase speed of the ball everytime the ball passes x < -11 or x > 11
        if (transform.position.x < -11) {
            GetComponent<Rigidbody>().velocity = new Vector3(rb.velocity.x*iSpeed, rb.velocity.y * iSpeed, rb.velocity.z * iSpeed);
        }
        if (transform.position.x > 11)
        {
            GetComponent<Rigidbody>().velocity = new Vector3(rb.velocity.x * iSpeed, rb.velocity.y * iSpeed, rb.velocity.z * iSpeed);
        }
		//if ball passes x < -15, player 2 gets a point. if ball passes x > 15, player 1 gets a point
		if (transform.position.x < -15) {
			game.playerTwoPoint ();
            speed = 7.5f;
			game.reset ();
		} else if (transform.position.x > 15) {
			game.playerOnePoint ();
            speed = 7.5f;
			game.reset ();
		}
	}

	//launches ball randomly either top left, top right, bottom left, or bottom right
	public void LaunchBall(){
		float sx = Random.Range (0, 2) == 0 ? -1 : 1;
		float sy = Random.Range (0, 2) == 0 ? -1 : 1;
		rb.velocity = new Vector3 (speed * sx, speed * sy, 0f);
	}
}
