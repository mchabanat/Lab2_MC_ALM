using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerScoreScript : MonoBehaviour
{
    [SerializeField] private int scoreAmount = 1;
    [SerializeField] private GameObject GameManager;
    [SerializeField] private bool giveNewBall;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Ball")
        {
            GameManager.GetComponent<GameManagerScript>().addScore(scoreAmount);

            if(giveNewBall)
            {
                GameManager.GetComponent<GameManagerScript>().setNumberOfBallsLeft(GameManager.GetComponent<GameManagerScript>().getNumberOfBallsLeft()+1);
            }
        }
    }

}
