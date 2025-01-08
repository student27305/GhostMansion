using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    KeyCode _moveKey = KeyCode.Mouse0;
    [SerializeField]
    KeyCode _pickupAndDropKey = KeyCode.Mouse1;
    [SerializeField]
    KeyCode _useItem = KeyCode.Mouse2;

    NavMeshAgent _agent;

    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(_moveKey)){
            Vector3 mousePositionNormalised = new Vector3(Input.mousePosition.x/ Screen.width, Input.mousePosition.y / Screen.height,0);
            Ray cursorRay = Camera.main.ViewportPointToRay(mousePositionNormalised);
            //Debug.DrawRay(Camera.main.transform.position, cursorRay.direction, Color.green, 5f);
            if( Physics.Raycast(cursorRay, out RaycastHit hit)){
                MoveToPosition(hit.point);
            }
        }
    }
    void PickupItem()
    {

    }
    void DropItem()
    {

    }
    void UseItem()
    {

    }
    void MoveToPosition(Vector3 position)
    {
        _agent.SetDestination(position);
    }
    void Die()
    {

    }
}
