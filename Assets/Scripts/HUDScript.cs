using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using System.Drawing;
using Color = UnityEngine.Color;

public class HUDScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    private GameManagerScript gameManager;
    [SerializeField] private TextMeshProUGUI ballsRemainingText;
    [SerializeField] private TextMeshProUGUI multiBallModeText;

    //Menu Pause
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private TextMeshProUGUI txtTopBestScore;
    public void updateScore(int score)
    {
        scoreText.text = score.ToString();
    }
    public void showPauseMenu(bool show)
    {
        pauseMenu.SetActive(show);
    }
    public void updateTopBestScore(List<int> topFiveScores)
    {
        txtTopBestScore.text = "";
        if (topFiveScores.Count == 0)
        {
            txtTopBestScore.text = "No best score yet";
            return;
        }
        for (int i = 0; i < topFiveScores.Count; i++)
        {
            txtTopBestScore.text += i + 1.ToString() + " : " + topFiveScores[i] + "\n";
        }
    }
    public void btnContinue()
    {
        gameManager.pause();
    }
    public void btnRetry()
    {
        gameManager.retry();
    }
    public void btnQuit()
    {
        gameManager.saveActualScore();
        Application.Quit();
    }
    public void setGameManager(GameManagerScript gm)
    {
        gameManager = gm;
    }

    public void updateBallsRemaining(int balls)
    {
        ballsRemainingText.text = "Balles restantes : "+balls.ToString();
    }

    public void updateMultiBallMode(bool multiBallMode)
    {
        if (multiBallMode)
        {
            multiBallModeText.text = "Mode multiboules activ�";
            // Texte en vert
            multiBallModeText.color = Color.green;

        } else
        {
            multiBallModeText.text = "Mode multiboules d�sactiv�";
            // Texte en rouge
            multiBallModeText.color = Color.red;
        }
    }


}
