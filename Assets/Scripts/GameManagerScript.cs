using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    [SerializeField] private bool multiBallModeIsActivated = false;
    [SerializeField] private int numberOfBallsMaxPerGame = 6;
    [SerializeField] private int numberOfBallsLeft;
    [SerializeField] private GameObject ballSpawner;

    void Start()
    {
        numberOfBallsLeft = numberOfBallsMaxPerGame;

        // On spawn la premi�re boule
        spawnBall();

        // On s'abonne � l'�v�nement "BallDestroyed" de la classe BallScript
        //BallScript.BallDestroyed += OnBallDestroyed;

    }

    void Update()
    {
        // Quand on appuie sur G, on active/desactive le mode multiboules
        if (Input.GetKeyDown(KeyCode.G))
        {
            multiBallModeIsActivated = !multiBallModeIsActivated;
        }


    }

    void spawnBall()
    {
        if (numberOfBallsLeft > 0)
        {
            // on fait spawn une boule
            ballSpawner.GetComponent<BallSpawnerScript>().SpawnBall();

            // On d�cr�mente le nombre de boules restantes
            numberOfBallsLeft--;
        } else
        {
            // Fin
            Debug.Log("Game Over");
        }
        
    }
}
