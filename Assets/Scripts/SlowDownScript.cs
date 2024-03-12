using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDownScript : MonoBehaviour
{
    [SerializeField] private float slowDownFactor = 0.8f;
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            collision.gameObject.GetComponent<Rigidbody>().velocity *= slowDownFactor;
        }
    }
}
