using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
	
	public void StartGame() {
        SceneManager.LoadScene("MazeScene");
    }

    public void QuitGame() {
        Application.Quit();
    }

    public void LoadGame() {
        GameController.control.Load();
    }
}
