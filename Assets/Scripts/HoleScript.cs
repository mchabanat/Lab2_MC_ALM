using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleScript : MonoBehaviour
{
    [SerializeField] private GameObject gameManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ball")
        {
            // On détruit la boule
            Destroy(other.gameObject);

            // On fait spawn une nouvelle boule
            gameManager.GetComponent<GameManagerScript>().spawnBall();
        }
    }
}
