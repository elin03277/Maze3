using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    private Button[] menuButtons;

    private void Awake()
    {
        menuButtons = GetComponentsInChildren<Button>();
        menuButtons[0].Select();
    }

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
