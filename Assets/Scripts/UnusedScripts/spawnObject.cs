using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class spawnObject : MonoBehaviour
{
    [SerializeField] private float threshold = 0.1f;
    [SerializeField] private float deadZone = 0.025f;

    public GameObject largeRoom;
    public Transform spawnPoint;
    public UnityEvent onPressed, onReleased;

    private bool _isPressed;
    private Vector3 _startPos;
    private ConfigurableJoint _joint;

    void Start()
    {
        _startPos = transform.localPosition;
        _joint = GetComponent<ConfigurableJoint>();
    }

    void Update()
    {
        if ( !_isPressed && getValue() + threshold >= 1 )
        {
            Pressed();
        }
        if (_isPressed && getValue() - threshold <= 1)
        {
            Released();
        }
    }

    private float getValue()
    {
        var value = Vector3.Distance( _startPos, transform.localPosition ) / _joint.linearLimit.limit;

        if (System.Math.Abs(value) < deadZone)
        {
            value = 0;
        }

        return Mathf.Clamp(value, -1f, 1f);
    }

    private void Pressed()
    {
        _isPressed = true;
        onPressed.Invoke();
        Instantiate(largeRoom, spawnPoint.position, Quaternion.identity);
    }

    private void Released()
    {
        _isPressed = false;
        onReleased.Invoke();
    }
}
