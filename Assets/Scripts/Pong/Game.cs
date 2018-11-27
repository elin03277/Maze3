using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//script controls the flow of the game
public class Game : MonoBehaviour {
	//score a player must reach to win
	public int winningScore = 3;
	//available states a game could be in
	public enum GameState
	{
		gameOver,
		playing,
        ready
	}
	public GameState gameState = GameState.ready;

	private int playerOneScore;
	private int playerTwoScore;

	private GameObject hudCanvas;
	private GameObject ball;
	private HUD hud;

	// Use this for initialization. Set gamestate to ready
	void Start () {
		ball = GameObject.Find ("Ball");
		hudCanvas = GameObject.Find ("HUD Canvas");
		hud = hudCanvas.GetComponent<HUD>();
        gameState = GameState.ready;
	}
	
	// Update is called once per frame. If gamestate is ready and spacebar or joystickbutton1 (x) is pressed, launch ball and set gamestate to playing
	//if gamestate is gameOver and spacebar or joystickbutton1 (x) is pressed, call StartGame method
	void Update () {
		CheckScore ();
		if ((Input.GetKeyUp (KeyCode.Space) || Input.GetKeyUp(KeyCode.JoystickButton1)) && gameState == GameState.ready) {
            gameState = GameState.playing;
			hud.playAgain.enabled = false;
			ball.GetComponent<Ball> ().LaunchBall ();
		} else if ((Input.GetKeyUp (KeyCode.Space) || Input.GetKeyUp(KeyCode.JoystickButton1)) && gameState == GameState.gameOver) {
            SceneManager.LoadScene("MazeScene");
		}
	}

	//check the score to see if a player has won
	void CheckScore(){
		if (playerOneScore >= winningScore) {
			PlayerOneWins ();
		} else if (playerTwoScore >= winningScore) {
			PlayerTwoWins ();
		}
	}
	//enables the player 1 win text and calls GameOver method
	void PlayerOneWins(){
		hud.win1.enabled = true;
		GameOver ();
	}
	//enables the player 2 win text and calls GameOver method
	void PlayerTwoWins(){
		hud.win2.enabled = true;
		GameOver ();
	}
	//enables gameover text and puts game in gameover state
	void GameOver(){
		hud.playAgain.text = "PRESS SPACEBAR OR X TO EXIT";
		gameState = GameState.gameOver;
	}
	//resets all the hud components and puts game in ready state, making it ready for the next game
	void StartGame(){
		playerOneScore = 0;
		playerTwoScore = 0;
		hud.playAgain.text = "PRESS SPACEBAR OR X TO PLAY";
		hud.score1.text = "0";
		hud.score2.text = "0";
		hud.win1.enabled = false;
		hud.win2.enabled = false;
		gameState = GameState.ready;
	}
	//player one scores a point, incrementing its score, and puts game in ready state, making it ready for the next round
	public void playerOnePoint(){
		playerOneScore++;
		hud.score1.text = playerOneScore.ToString ();
		hud.playAgain.enabled = true;
        gameState = GameState.ready;
    }
	//player two scores a point, incrementing its score, and puts game in ready state, making it ready for the next round
	public void playerTwoPoint(){
		playerTwoScore++;
		hud.score2.text = playerTwoScore.ToString ();
		hud.playAgain.enabled = true;
        gameState = GameState.ready;
	}
	//reset the ball to its initial location with 0 velocity
	public void reset(){
		ball.GetComponent<Rigidbody> ().velocity = new Vector3 (0f, 0f, 0f);
		ball.transform.position = new Vector3(0f,0f,0f);
	}
}
