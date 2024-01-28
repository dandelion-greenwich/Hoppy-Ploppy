using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    public enum GameState { MainMenu, Paused, Playing, GameOver };
    public GameState currentState;
    public GameObject mainMenuPanel, pauseMenuPanel, gameOverPanel;
    public GameObject[] titleText, resultText;
    public CharacterController player;

    private void Awake()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            CheckGameState(GameState.MainMenu);
        }
        else
        {
            CheckGameState(GameState.Playing);
        }
    }

    public void CheckGameState(GameState newGameState)
    {
        currentState = newGameState;
        switch (currentState)
        {
            case GameState.MainMenu:
                MainMenuSetup();
                break;
            case GameState.Paused:
                GamePaused();
                Time.timeScale = 0f;
                player.gamePaused = true;
                break;
            case GameState.Playing:
                GameActive();
                Time.timeScale = 1f;
                player.gamePaused = false;
                break;
            case GameState.GameOver:
                GameOver();
                Time.timeScale = 0f;
                player.gamePaused = true;
                break;
        }
    }

    public void MainMenuSetup()
    {
        mainMenuPanel.SetActive(true);
        pauseMenuPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        titleText[0].SetActive(true);
        titleText[1].SetActive(true);
        resultText[0].SetActive(true);
    }

    public void GameActive()
    {
        mainMenuPanel.SetActive(false);
        pauseMenuPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        titleText[0].SetActive(false);
        titleText[1].SetActive(false);
    }

    public void GamePaused()
    {
        mainMenuPanel.SetActive(false);
        pauseMenuPanel.SetActive(true);
        gameOverPanel.SetActive(false);
        titleText[0].SetActive(true);
        titleText[1].SetActive(true);
        resultText[0].SetActive(true);
    }

    public void GameOver()
    {
        mainMenuPanel.SetActive(false);
        pauseMenuPanel.SetActive(false);
        gameOverPanel.SetActive(true);
        titleText[0].SetActive(true);
        titleText[1].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        CheckInputs();
    }

    void CheckInputs()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentState == GameState.Playing)
            {
                CheckGameState(GameState.Paused);
            }
            else if (currentState == GameState.Paused)
            {
                CheckGameState(GameState.Playing);
            }
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("LePark");
        CheckGameState(GameState.Playing);
    }

    public void PauseGame()
    {
        CheckGameState(GameState.Paused);
    }

    public void ResumeGame()
    {
        CheckGameState(GameState.Playing);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        CheckGameState(GameState.MainMenu);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
