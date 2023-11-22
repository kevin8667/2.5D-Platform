using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePad : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Movable Box")
        {
            float distance = Vector3.Distance(new Vector3( transform.position.x,0,0), new Vector3(other.transform.position.x,0,0));

            if (distance < 0.1f)
            {
                Rigidbody rb = other.GetComponent<Rigidbody>();

                if (rb != null)
                {
                    rb.isKinematic = true;
                }

                MeshRenderer renderer = gameObject.GetComponent<MeshRenderer>();

                if (renderer != null)
                {
                    renderer.material.color = Color.blue;
                }

                Debug.Log("The pad has been pressed!");

                Destroy(this);
            }
        }
    }
}
