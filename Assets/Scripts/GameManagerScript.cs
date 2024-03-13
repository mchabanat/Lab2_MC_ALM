using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    [SerializeField] private bool multiBallModeIsActivated;
    [SerializeField] private int numberOfBallsMaxPerGame = 6;
    [SerializeField] private int numberOfBallsLeft;
    [SerializeField] private GameObject ballSpawner;

    //Score
    private int score;
    [SerializeField] private GameObject HUD;

    //TopScores
    private List<int> bestScores;
    [SerializeField] private int maxScoreToKeep;
    [SerializeField] private GameObject[] stepCircles;
    [SerializeField] private int stepCirclesActivated = 0;
    [SerializeField] private bool doorIsOpen = false;
    [SerializeField] private GameObject door;

    //PauseMenu
    private bool isPaused = false;

    //CameraShake
    [SerializeField] GameObject mainCam;
    void Start()
    {
        setNumberOfBallsLeft(numberOfBallsMaxPerGame);
        setMultiBallModeIsActivated(false);

        // On spawn la premi�re boule
        spawnBall();

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
            setMultiBallModeIsActivated(!getMultiBallModeIsActivated());
        }

        //Pause
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pause();
        }
        stepCirclesCheck();

    }

    public void spawnBall()
    {
        if (numberOfBallsLeft > 0)
        {
            // on fait spawn une boule
            ballSpawner.GetComponent<BallSpawnerScript>().SetGameManager(this);
            ballSpawner.GetComponent<BallSpawnerScript>().SpawnBall();

            // On d�cr�mente le nombre de boules restantes
            setNumberOfBallsLeft(getNumberOfBallsLeft()-1);
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

    public void updateBallsRemainingText()
    {
        HUD.GetComponent<HUDScript>().updateBallsRemaining(getNumberOfBallsLeft());
    }

    // Setter et getter de numberOfBallsLeft
    public int getNumberOfBallsLeft()
    {
        return numberOfBallsLeft;
    }
    public void setNumberOfBallsLeft(int value)
    {
        numberOfBallsLeft = value;
        updateBallsRemainingText();
    }

    public void stepCirclesCheck()
    {
        if (getStepCirclesActivated() == stepCircles.Length)
        {
            // On a gagné incrémenter le score
            addScore(1000);

            // Le joueur gagne une boule supplémentaire
            setNumberOfBallsLeft(getNumberOfBallsLeft()+1);

            // On ouvre la porte si elle est fermée
            if (!doorIsOpen)
            {
                door.GetComponent<DoorScript>().openDoor();
                doorIsOpen = true;
            }

            // On remet les materiaux des cercles à leur état initial
            foreach (GameObject stepCircle in stepCircles)
            {
                stepCircle.GetComponent<ActivateStepCirclesScript>().changeMaterial(stepCircle.GetComponent<ActivateStepCirclesScript>().getInactiveMaterial());
            }

            // On remet le compteur de cercles activés à 0
            setStepCirclesActivated(0);
        }
    }

    public int getStepCirclesActivated()
    {
        return stepCirclesActivated;
    }

    public void setStepCirclesActivated(int value)
    {
        stepCirclesActivated = value;
    }

    public bool getMultiBallModeIsActivated()
    {
        return multiBallModeIsActivated;
    }

    public void setMultiBallModeIsActivated(bool value)
    {
        multiBallModeIsActivated = value;
        HUD.GetComponent<HUDScript>().updateMultiBallMode(multiBallModeIsActivated);
    }
}
