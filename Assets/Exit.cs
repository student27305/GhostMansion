using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Exit : MonoBehaviour
{
    public UnityEvent PlayerReachedExitEvent { get; private set; }
    GameObject _player;
    [SerializeField]
    Collider _triggerCollider;

    void Awake()
    {
        PlayerReachedExitEvent = new UnityEvent();
        _player = GameObject.FindWithTag("Player");
    }
    void Update()
    {
        if (_triggerCollider.bounds.Contains(_player.transform.position))
        {
            PlayerReachedExitEvent.Invoke();
        }
    }
}
