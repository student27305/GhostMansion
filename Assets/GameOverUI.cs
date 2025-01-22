using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public static GameOverUI Instance {  get; private set; }
    [SerializeField]
    GameObject _gameOverPanel;
    private void Awake()
    {
        Instance = this;
    }

    public  void DisplayScreen()
    {
        _gameOverPanel.SetActive(true);
    }

    public void OnReturnToMenuClicked()
    {
        SceneManager.LoadScene(0);
    }
}
