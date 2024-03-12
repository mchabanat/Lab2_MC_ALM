using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    [SerializeField] private bool multiBallModeIsActivated;
    [SerializeField] private int numberOfBallsMaxPerGame = 6;
    [SerializeField] private int numberOfBallsLeft;
    [SerializeField] private GameObject ballSpawner;

    private int score;
    [SerializeField] private GameObject HUD;

    [SerializeField] private GameObject[] stepCircles;
    [SerializeField] private int stepCirclesActivated = 0;

    void Start()
    {
        setNumberOfBallsLeft(numberOfBallsMaxPerGame);
        setMultiBallModeIsActivated(false);

        // On spawn la premi�re boule
        spawnBall();

        score = 0;
    }

    void Update()
    {
        // Quand on appuie sur G, on active/desactive le mode multiboules
        if (Input.GetKeyDown(KeyCode.G))
        {
            setMultiBallModeIsActivated(!getMultiBallModeIsActivated());
        }

        stepCirclesCheck();

    }

    public void spawnBall()
    {
        if (numberOfBallsLeft > 0)
        {
            // on fait spawn une boule
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
