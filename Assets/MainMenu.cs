using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MainMenu : MonoBehaviour
{
    [HideInInspector]
    public UnityEvent PlayClicked { get; private set; }
    [HideInInspector]
    public UnityEvent ExitClicked { get; private set; }
    [HideInInspector]
    public UnityEvent<int> DifficultyChanged { get; private set; }

    private void Awake()
    {
        PlayClicked = new UnityEvent();
        ExitClicked = new UnityEvent();
        DifficultyChanged = new UnityEvent<int>();
    }
    public void OnPlayClicked()
    {
        PlayClicked.Invoke();
    }

    public void OnExitClicked()
    {
        ExitClicked.Invoke();
    }

    public void OnDifficultyChanged(int difficulty)
    {
        DifficultyChanged.Invoke(difficulty);
    }
}
