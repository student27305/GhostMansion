using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField]
    DoorKey _key;
    [SerializeField]
    AnimationClip _openClip;
    [SerializeField]
    AnimationClip _closeClip;

    Animation _animation;
    Renderer _renderer;
    public bool IsOpen { get; private set; }

    void Start()
    {
        _animation = GetComponent<Animation>();
        _renderer = GetComponent<Renderer>();
        _renderer.material.color = _key.GetKeyColor();
    }

    public void TryKey(DoorKey key)
    {
        if(_key == key && !IsOpen)
        {
            Open();
        }
    }
    void Open()
    {
        _animation.Play("door_open");
        IsOpen = true;
    }
    void Close()
    {
        _animation.Play("door_close");
        IsOpen = false;
    }
}
