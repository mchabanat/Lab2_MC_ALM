using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlungerScript : MonoBehaviour
{
    [SerializeField] private float power;
    private float minPower = 0f;
    private float maxPower = 500f;
    [SerializeField] private Slider powerSlider;
    private List<Rigidbody> ballList;
    private bool ballReady;


    void Start()
    {
        powerSlider.minValue = minPower;
        powerSlider.maxValue = maxPower;
        ballList = new List<Rigidbody>();
    }

    void Update()
    {
        if (ballReady)
        {
            powerSlider.gameObject.SetActive(true);
        } else
        {
            powerSlider.gameObject.SetActive(false);
        }

        powerSlider.value = power;

        if (ballList.Count > 0)
        {
            ballReady = true;
            if (Input.GetKey(KeyCode.Return))
            {
                if (power <= maxPower)
                {
                    power += 500f * Time.deltaTime;
                }
            }
            if (Input.GetKeyUp(KeyCode.Return))
            {
                foreach (Rigidbody ball in ballList)
                {
                    ball.AddForce(power * Vector3.forward);
                }
            }
        } else
        {
            ballReady = false;
            power = 0f;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            ballList.Add(other.GetComponent<Rigidbody>());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            ballList.Remove(other.GetComponent<Rigidbody>());
            power = 0f;
        }
    }
}
