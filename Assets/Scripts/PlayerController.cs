using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    KeyCode _moveKey = KeyCode.Mouse0;
    [SerializeField]
    KeyCode _pickupAndDropKey = KeyCode.Mouse1;
    [SerializeField]
    KeyCode _useItem = KeyCode.Mouse2;

    [SerializeField]
    Vector3 _heldItemPostionOffset;
    [SerializeField]
    float _itemGrabRange = 3f;

    NavMeshAgent _agent;
    Item _heldItem;

    IEnumerator _deathCoroutine;
    bool _isAlive = true;

    public UnityEvent PlayerDiedEvent { get; private set; }

    void Awake()
    {
        _deathCoroutine = DeathRoutine();
        PlayerDiedEvent = new UnityEvent();
    }
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
        if (Input.GetKeyDown(_pickupAndDropKey)) 
        {
            if (_heldItem == null) 
            {
                TryGrabItem();
            }
            else
            {
                DropItem();
            }
        }
        if (Input.GetKeyDown(_useItem)) { TryUseItem(); }
    }
    void TryGrabItem()
    {
        Item[] items = FindObjectsOfType<Item>();
        Item bestGrab = null;
        float bestGrabDistance = Mathf.Infinity;
        // Find closest item within range
        foreach (Item item in items) 
        {
            float distance = Vector3.Distance(item.transform.position, transform.position);
            if ( distance < _itemGrabRange && distance < bestGrabDistance)
            {
                bestGrab = item;
                bestGrabDistance = distance;
            }
        }
        // Pickup choosen item
        if (bestGrab != null) 
        {
            PickupItem(bestGrab);
        }
    }
    void PickupItem(Item item)
    {
        item.transform.parent = transform;
        item.transform.localPosition = _heldItemPostionOffset;
        _heldItem = item;
    }
    void DropItem()
    {
        _heldItem.transform.parent = null;
        _heldItem.transform.position = new Vector3(transform.position.x, 0.1f, transform.position.z);
        _heldItem = null;
    }
    void TryUseItem()
    {
        if (_heldItem != null) 
        { 
            _heldItem.UseItem();
        }
    }
    void MoveToPosition(Vector3 position)
    {
        _agent.SetDestination(position);
    }

    IEnumerator DeathRoutine()
    {
        yield return new WaitForSeconds(2f);
        PlayerDiedEvent.Invoke();
    }

    public void Die()
    {
        if (_isAlive)
        {
            _isAlive = false;
            _agent.isStopped = true;
            _agent.autoRepath = false;
            _agent.speed = 0;
            _agent.enabled = false;
            transform.rotation = Quaternion.Euler(90f, 0f, 0f);
            Material renderedMaterial = GetComponent<Renderer>().material;
            renderedMaterial.color = Color.red;
            StartCoroutine(DeathRoutine());
            
        }
    }
}
