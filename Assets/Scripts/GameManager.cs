using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    const int Easy = 0;
    const int Normal = 1;
    const int Hard = 2;

    const int Enemies_Easy = 3;
    const int Enemies_Normal = 5;
    const int Enemies_Hard = 7;

    LevelManager _levelManager;

    public static GameManager Instance { get; private set; }

    public int Difficulty { get; private set; }

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
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
        RefreshSubscriptions();
    }

    void ChangeDifficulty(int difficulty)
    {
        Difficulty = difficulty;
    }

    void SetupLevel()
    {
        
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

    void SubscriveToLevelManager()
    {
        GameObject levelManager = GameObject.FindGameObjectWithTag("LevelManager");
        _levelManager = levelManager.GetComponent<LevelManager>();
    }

    void RefreshSubscriptions() // Oh no!
    {
        SubscribeToMainMenu();
        SubscriveToLevelManager();
    }
    private void OnLevelWasLoaded(int level)
    {
        RefreshSubscriptions();
        if (level == 1) { SetupLevel(); }
    }

}
