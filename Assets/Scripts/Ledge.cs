using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ledge : MonoBehaviour
{
    [SerializeField]
    private Vector3 _handPos, _standPos;

    public Vector3 StandPos => _standPos;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LedgeGrabChecker"))
        {

            Player player = other.transform.parent.GetComponent<Player>();

            if (player != null)
            {
                player.GrabLedge(_handPos, this);
            }

        }
    }
}
