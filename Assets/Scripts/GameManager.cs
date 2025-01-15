using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{


    const int Easy = 0;
    const int Normal = 1;
    const int Hard = 2;

    public static GameManager Instance { get; private set; }

    public int Difficulty { get; private set; }

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else { Destroy(this); }
    }

    void Start()
    {
        SubscribeToMainMenu();
    }
    void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    void GameOver()
    {

    }
    void QuitGame()
    {
        Application.Quit();
    }
    void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    void ChangeDifficulty(int difficulty)
    {
        Difficulty = difficulty;
    }

    void SubscribeToMainMenu()
    {
        MainMenu mainMenu = GameObject.FindWithTag("MainMenu")?.GetComponent<MainMenu>();
        if (mainMenu != null)
        {
            mainMenu.ExitClicked.AddListener(QuitGame);
            mainMenu.PlayClicked.AddListener(StartGame);
            mainMenu.DifficultyChanged.AddListener(ChangeDifficulty);
        }
    }

    void OnLevelWasLoaded(int level)
    {
        SubscribeToMainMenu();
    }
}
