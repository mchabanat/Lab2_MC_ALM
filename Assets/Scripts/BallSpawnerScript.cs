using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawnerScript : MonoBehaviour
{
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private GameObject gamePlate;

    public void SpawnBall()
    {
        // On r�cup�re la position du spawner de boules
        Vector3 position = gameObject.transform.position;
        // On r�cup�re la rotation du plateau de jeu
        Quaternion rotation = gamePlate.transform.rotation;

        // on fait spawn une boule
        Instantiate(ballPrefab, position, rotation);
    }
}
