using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorPanel : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer _callButton;
    private bool _isPrepared;
    [SerializeField]
    private int _coinRequirement;
    
    [SerializeField]
    private Elevator _elevator;

    private bool _isCalled;

    private void Update()
    {
        if (_isPrepared && Input.GetKeyDown(KeyCode.E))
        {
            
            if (_isCalled == false) 
            {
                _isCalled = true;
                _callButton.material.color = Color.green;
                _elevator.OperateElevator();
            }
            else 
            {
                _isCalled = false;
                _callButton.material.color = Color.red;
                _elevator.OperateElevator();
            }
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Player player = other.gameObject.GetComponent<Player>();

            if (player.GetCoin() >= _coinRequirement)
            {
                _isPrepared = true;
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            _isPrepared = false;

        }
    }


}
