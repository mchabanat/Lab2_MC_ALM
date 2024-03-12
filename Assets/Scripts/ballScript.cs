using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballScript : MonoBehaviour
{
    GameManagerScript gameManager;
    public void setGameManager(GameManagerScript gm)
    {
        gameManager = gm;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "obstacle")
        {
            gameManager.shakeCamera(GetComponent<Rigidbody>().velocity.magnitude);
        }
    }
}
