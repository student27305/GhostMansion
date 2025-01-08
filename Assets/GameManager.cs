using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else { Destroy(this); }
    }
}
