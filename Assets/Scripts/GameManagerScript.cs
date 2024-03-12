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

    //PauseMenu
    private bool isPaused = false;

    //CameraShake
    [SerializeField] GameObject mainCam;
    void Start()
    {
        numberOfBallsLeft = numberOfBallsMaxPerGame;

        // On spawn la premi�re boule
        spawnBall();

        // On s'abonne � l'�v�nement "BallDestroyed" de la classe BallScript
        //BallScript.BallDestroyed += OnBallDestroyed;
        score = 0;
        loadBestScores();
        isPaused = false;
        HUD.GetComponent<HUDScript>().setGameManager(this);
    }

    void Update()
    {
        // Quand on appuie sur G, on active/desactive le mode multiboules
        if (Input.GetKeyDown(KeyCode.G))
        {
            multiBallModeIsActivated = !multiBallModeIsActivated;
        }

        //Pause
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pause();
        }
    }

    public void spawnBall()
    {
        if (numberOfBallsLeft > 0)
        {
            // on fait spawn une boule
            ballSpawner.GetComponent<BallSpawnerScript>().SetGameManager(this);
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

    private void loadBestScores()
    {
        bestScores = new List<int>();
        if (PlayerPrefs.HasKey("BestScores"))
        {
            string scoresString = PlayerPrefs.GetString("BestScores");
            if (scoresString.Length == 0)
            {
                return;
            }
            bestScores = scoresString.Split(',').Select(int.Parse).ToList();
        }
    }

    private void saveBestScores()
    {
        string scoresString = string.Join(",", bestScores.Select(x => x.ToString()).ToArray());

        PlayerPrefs.SetString("BestScores", scoresString);
        PlayerPrefs.Save();
    }

    public void saveActualScore()
    {
        bestScores.Add(score);
        bestScores = bestScores.OrderByDescending(s => s).Take(maxScoreToKeep).ToList();
        saveBestScores();
    }

    private void activePause(bool pause)
    {
        if (pause)
        {
            Time.timeScale = 0;
            HUD.GetComponent<HUDScript>().showPauseMenu(true);
            HUD.GetComponent<HUDScript>().updateTopBestScore(bestScores);
        }
        else
        {
            Time.timeScale = 1;
            HUD.GetComponent<HUDScript>().showPauseMenu(false);
        }

    }
    public void pause()
    {
        isPaused = !isPaused;
        activePause(isPaused);
    }

    public void retry()
    {
        saveActualScore();
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void shakeCamera(float force)
    {
        mainCam.GetComponent<cameraShake>().Shake(force);
    }
}
