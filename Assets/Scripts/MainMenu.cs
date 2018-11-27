using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    private Button[] menuButtons;
    private int cur = 0;

    private void Awake()
    {
        menuButtons = GetComponentsInChildren<Button>();
        menuButtons[0].Select();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.JoystickButton0))
            menuButtons[cur].onClick.Invoke();
        if (Input.GetKeyDown(KeyCode.JoystickButton12))
        {
            cur++;
            if (cur > menuButtons.Length)
                cur = menuButtons.Length;
        } else if (Input.GetKeyDown(KeyCode.JoystickButton10))
        {
            cur--;
            if (cur < 0)
                cur = 0;
        }
        menuButtons[cur].Select();
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
