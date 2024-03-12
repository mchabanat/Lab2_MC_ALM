using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI ballsRemainingText;

    public void updateScore(int score)
    {
        scoreText.text = score.ToString();
    }

    public void updateBallsRemaining(int balls)
    {
        ballsRemainingText.text = "Balles restantes : "+balls.ToString();
    }
}
