using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    [SerializeField] private bool multiBallModeIsActivated = false;
    [SerializeField] private int numberOfBallsMaxPerGame = 6;
    [SerializeField] private int numberOfBallsLeft;
    [SerializeField] private GameObject ballSpawner;

    private int score;
    [SerializeField] private GameObject HUD;



    void Start()
    {
        numberOfBallsLeft = numberOfBallsMaxPerGame;

        // On spawn la premi�re boule
        spawnBall();

        // On s'abonne � l'�v�nement "BallDestroyed" de la classe BallScript
        //BallScript.BallDestroyed += OnBallDestroyed;
        score = 0;
    }

    void Update()
    {
        // Quand on appuie sur G, on active/desactive le mode multiboules
        if (Input.GetKeyDown(KeyCode.G))
        {
            multiBallModeIsActivated = !multiBallModeIsActivated;
        }


    }

    public void spawnBall()
    {
        if (numberOfBallsLeft > 0)
        {
            // on fait spawn une boule
            ballSpawner.GetComponent<BallSpawnerScript>().SpawnBall();

            // On d�cr�mente le nombre de boules restantes
            numberOfBallsLeft--;
        }
        else
        {
            // Fin
            Debug.Log("Game Over");
        }

    }

    public void addScore(int scoreAmnt)
    {
        score += scoreAmnt;
        HUD.GetComponent<HUDScript>().updateScore(score);
    }
}
