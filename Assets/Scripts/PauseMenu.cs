using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {

    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;

    public GameObject player;
    public GameObject enemy;

    public Button[] menuButtons;
    private int cur = 0;

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyUp(KeyCode.JoystickButton7)) {
            if (GameIsPaused) {
                Resume();
            } else {
                Pause();
                menuButtons[0].Select();
            }
        }
        if (GameIsPaused)
        {
            if (Input.GetKeyDown(KeyCode.JoystickButton0))
                menuButtons[cur].onClick.Invoke();
            if (Input.GetKeyDown(KeyCode.JoystickButton12))
            {
                cur++;
                if (cur > menuButtons.Length)
                    cur = menuButtons.Length;
            }
            else if (Input.GetKeyDown(KeyCode.JoystickButton10))
            {
                cur--;
                if (cur < 0)
                    cur = 0;
            }
            menuButtons[cur].Select();
        }
	}

    public void Resume() {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause() {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void SaveGame() {
        GameController.control.Save(ScoreManager.score, player.transform.position, enemy.transform.position);
    }

    public void QuitGame() {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
    }
}
