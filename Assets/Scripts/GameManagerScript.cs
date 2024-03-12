using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    [SerializeField] private bool multiBallModeIsActivated = false;
    [SerializeField] private int numberOfBallsMaxPerGame = 6;
    [SerializeField] private int numberOfBallsLeft;
    [SerializeField] private GameObject ballSpawner;

    //Score
    private int score;
    [SerializeField] private GameObject HUD;

    //TopScores
    private List<int> bestScores;
    [SerializeField] private int maxScoreToKeep;

    void Start()
    {
        numberOfBallsLeft = numberOfBallsMaxPerGame;

        // On spawn la première boule
        spawnBall();

        // On s'abonne à l'événement "BallDestroyed" de la classe BallScript
        //BallScript.BallDestroyed += OnBallDestroyed;
        score = 0;
        loadBestScores();
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

            // On décrémente le nombre de boules restantes
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

    private void loadBestScores()
    {
        if (PlayerPrefs.HasKey("BestScores"))
        {
            string scoresString = PlayerPrefs.GetString("BestScores");
            bestScores = scoresString.Split(',').Select(int.Parse).ToList();
        }
    }

    private void saveBestScores()
    {
        string scoresString = string.Join(",", bestScores.Select(x => x.ToString()).ToArray());

        PlayerPrefs.SetString("BestScores", scoresString);
        PlayerPrefs.Save();
    }

    private void saveActualScore()
    {
        bestScores.Add(score);
        bestScores = bestScores.OrderByDescending(s => s).Take(maxScoreToKeep).ToList();
        saveBestScores();
    }
}
