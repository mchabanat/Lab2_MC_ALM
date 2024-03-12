using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class HUDScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    private GameManagerScript gameManager;

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
}
