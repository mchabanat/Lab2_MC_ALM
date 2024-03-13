using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumperScript : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // Quand une boule entre en collision avec le bumper on repousse la boule dans la direction opposée
        if (collision.gameObject.tag == "Ball")
        {
            collision.gameObject.GetComponent<Rigidbody>().AddForce(-collision.contacts[0].normal * 250);
        }
    }
}
