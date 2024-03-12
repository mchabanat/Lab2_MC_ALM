using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawnerScript : MonoBehaviour
{
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private GameObject gamePlate;

    private GameManagerScript gameManager;

    public void SpawnBall()
    {
        // On récupère la position du spawner de boules
        Vector3 position = gameObject.transform.position;
        // On récupère la rotation du plateau de jeu
        Quaternion rotation = gamePlate.transform.rotation;

        // on fait spawn une boule
        GameObject ball = Instantiate(ballPrefab, position, rotation);
        ball.GetComponent<ballScript>().setGameManager(gameManager);
    }

    public void SetGameManager(GameManagerScript gm)
    {
        gameManager = gm;
    }
}
