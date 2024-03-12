using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Drawing;
using Color = UnityEngine.Color;

public class HUDScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI ballsRemainingText;
    [SerializeField] private TextMeshProUGUI multiBallModeText;

    public void updateScore(int score)
    {
        scoreText.text = score.ToString();
    }

    public void updateBallsRemaining(int balls)
    {
        ballsRemainingText.text = "Balles restantes : "+balls.ToString();
    }

    public void updateMultiBallMode(bool multiBallMode)
    {
        if (multiBallMode)
        {
            multiBallModeText.text = "Mode multiboules activé";
            // Texte en vert
            multiBallModeText.color = Color.green;

        } else
        {
            multiBallModeText.text = "Mode multiboules désactivé";
            // Texte en rouge
            multiBallModeText.color = Color.red;
        }
    }


}
