using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public Maze mazePrefab;
	public Camera cam;
    public Light flashlightLight;
    public Light directional;
    public GameObject ball;

	private Maze mazeInstance;
	private GameObject player;
    private GameObject enemy;
	private Transform playerTransform;
    private Transform enemyTransform;
    private DeferredFogEffect fogEffect;

	private void Awake () {
		player = GameObject.FindGameObjectWithTag ("Player");
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        enemyTransform = enemy.transform;
		playerTransform = player.transform;
        if (GameController.control.loaded) {
            playerTransform.position = GameController.control.playerPos;
            enemyTransform.position = GameController.control.enemyPos;
            ScoreManager.score = GameController.control.score;
            GameController.control.loaded = false;
        } else
            playerTransform.position = new Vector3(-9.5f, 0.5f, -9.5f);
        playerTransform.rotation = Quaternion.Euler (0f,0f,0f);
		fogEffect = cam.GetComponent<DeferredFogEffect>();
		BeginGame();
	}
	
	private void Update () {
		if (Input.GetKeyDown(KeyCode.Home) || Input.GetKeyUp(KeyCode.JoystickButton3)) {
			RestartPlayer();
		}
		if (Input.GetKeyDown (KeyCode.E) || Input.GetKeyUp(KeyCode.JoystickButton1))
			player.GetComponent<Collider> ().enabled = !player.GetComponent<Collider> ().enabled;
		if (Input.GetKeyDown (KeyCode.Q) || Input.GetKeyUp(KeyCode.JoystickButton2))
			ToggleDay ();
		if (Input.GetKeyDown(KeyCode.F) || Input.GetKeyUp(KeyCode.JoystickButton0))
			ToggleFog();
        if (Input.GetKeyDown(KeyCode.C) || Input.GetKeyUp(KeyCode.JoystickButton3))
            ToggleLight();
        if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyUp(KeyCode.JoystickButton4))
            ThrowBall();
    }

	private void BeginGame () {
		mazeInstance = Instantiate(mazePrefab) as Maze;
		mazeInstance.Generate();
	}

	private void RestartGame () {
		Destroy(mazeInstance.gameObject);
		BeginGame();
	}

	private void RestartPlayer () {
        /*playerTransform.position = new Vector3 (-9.5f, 0.5f, -9.5f);
		playerTransform.rotation = Quaternion.Euler (0f,0f,0f);
		cam.transform.rotation = Quaternion.Euler (0f,0f,0f);
        ScoreManager.score = 0;
		fogEffect.enabled = false;
        directional.color = Color.white;
        flashlightLight.enabled = false;
		player.GetComponent<Collider>().enabled = true;*/
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

	private void ToggleDay(){
        if (directional.color == Color.white)
            directional.color = Color.black;
        else
            directional.color = Color.white;
	}

	private void ToggleFog() {
		fogEffect.enabled = !fogEffect.enabled;
	}

    private void ToggleLight() {
        flashlightLight.enabled = !flashlightLight.enabled;
    }

    private void ThrowBall() {
        GameObject clone = Instantiate(ball, cam.transform.position + new Vector3(0,.2f,0), cam.transform.rotation);
        clone.GetComponent<Rigidbody>().AddForce(cam.transform.forward * 350);
        Destroy(clone, 5f);
    }
}