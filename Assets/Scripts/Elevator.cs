using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    private bool _isGoingDown;
    [SerializeField]
    private Transform _upperPoint, _lowerPoint;
    [SerializeField]
    private float _speed;

    public void OperateElevator()
    {
        _isGoingDown = !_isGoingDown;
    }

    void FixedUpdate()
    {
        if (_isGoingDown == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, _lowerPoint.transform.position, _speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, _upperPoint.transform.position, _speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.parent = transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.parent = null;
        }
    }


}
