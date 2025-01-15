using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    Vector3 _offset;
    [SerializeField]
    Transform _targetTransform;


    private void Start()
    {
        _offset = transform.position - _targetTransform.position;
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = _targetTransform.position + _offset;
    }
}
