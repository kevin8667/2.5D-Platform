using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField]
    private GameObject _startPoint;
    [SerializeField]
    private GameObject _endPoint;
    [SerializeField]
    private float _speed = 1.0f;
    [SerializeField]
    private float _waitingTime = 1f;

    private bool _directionSwitch;

    // Start is called before the first frame update
    void Start()
    {
         transform.position = _startPoint.transform.position;
    }

    void FixedUpdate()
    {
        if (_directionSwitch == false) 
        {
            transform.position = Vector3.MoveTowards(transform.position, _endPoint.transform.position, _speed * Time.deltaTime);
        }
        else 
        {
            transform.position = Vector3.MoveTowards(transform.position, _startPoint.transform.position, _speed * Time.deltaTime);
        }


        if (transform.position == _endPoint.transform.position || transform.position == _startPoint.transform.position)
        {
            StartCoroutine(WaitRoutine());
        }

    }

    IEnumerator WaitRoutine() 
    {
        float tempSpeed = _speed;

        _speed = 0f;

        yield return new WaitForSeconds(_waitingTime);

        _directionSwitch = !_directionSwitch;
        _speed = tempSpeed;

        StopAllCoroutines();
        
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
