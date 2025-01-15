using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorKey : Item
{
    [SerializeField]
    Color _color;
    [SerializeField]
    float _openRange = 3f;

    Renderer _renderer;

    public override void UseItem()
    {
        Door[] doors = FindObjectsOfType<Door>();
        foreach (Door door in doors) 
        {
            if (Vector3.Distance(door.transform.position, transform.position) < _openRange) 
            {
                door.TryKey(this);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<Renderer>();
        _renderer.material.color = _color;
    }

    public Color GetKeyColor()
    {
        return _color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
