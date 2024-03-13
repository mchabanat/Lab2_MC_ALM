using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateStepCirclesScript : MonoBehaviour
{
    [SerializeField] private Material activeMaterial;
    [SerializeField] private Material inactiveMaterial;
    [SerializeField] private GameObject gameManager;

    private bool isActivated = false;

    private void Start()
    {
        isActivated = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            if (!isActivated)
            {
                isActivated = true;
                // Changer le matériel du sprite du cercle avec le matériel activeMaterial
                changeMaterial(getActiveMaterial());
                // Incrémenter le nombre de cercles activés
                gameManager.GetComponent<GameManagerScript>().setStepCirclesActivated(gameManager.GetComponent<GameManagerScript>().getStepCirclesActivated() + 1);
            }
        }
    }

    public void changeMaterial(Material material)
    {
        GetComponent<SpriteRenderer>().material = material;
    }

    public Material getActiveMaterial()
    {
        return activeMaterial;
    }

    public Material getInactiveMaterial()
    {
        return inactiveMaterial;
    }

    public void desactivate()
    {
        isActivated = false;
        changeMaterial(getInactiveMaterial());
    }
    
}
