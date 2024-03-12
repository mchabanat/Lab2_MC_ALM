using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawnerScript : MonoBehaviour
{
    [SerializeField] private GameObject ballPrefab;
    public void SpawnBall()
    {
        // on fait spawn une boule
        Instantiate(ballPrefab, gameObject.transform.position, Quaternion.identity);
    }
}
