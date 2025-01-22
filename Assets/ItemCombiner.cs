using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ItemCombiner : MonoBehaviour
{
    [SerializeField]
    GameObject[] _craftingComponents;
    [SerializeField]
    GameObject _craftingResult;
    [SerializeField]
    Transform _cratedItemPosition;

    BoxCollider _craftingArea;
    // Start is called before the first frame update
    void Start()
    {
        _craftingArea = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        int itemsCount = 0;
        foreach (var item in _craftingComponents) 
        {
            if (_craftingArea.bounds.Contains(item.transform.position)) 
            {
                itemsCount++;
            }
        }
        if (itemsCount == _craftingComponents.Length) { CraftItem(); }
    }

    void CraftItem()
    {
        foreach (var component in _craftingComponents)
        {
            component.transform.position = new Vector3(0f, -100f, 0f);
            component.SetActive(false);
        }
        _craftingResult.transform.position = _cratedItemPosition.position;
    }
}
