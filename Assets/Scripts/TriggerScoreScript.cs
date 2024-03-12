using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerScoreScript : MonoBehaviour
{
    [SerializeField] private int scoreAmount = 1;
    [SerializeField] private GameObject GameManager;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Ball")
        {
            GameManager.GetComponent<GameManagerScript>().addScore(scoreAmount);
        }
    }

}
