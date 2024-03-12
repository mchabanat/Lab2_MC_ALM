using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraShake : MonoBehaviour
{
    private Transform cameraTransform; // Référence à la transform de la caméra
    [SerializeField] float shakeDuration = 0.1f; // Durée de l'effet de shake
    [SerializeField] float shakeMagnitude = 0.1f; // Intensité du shake
    private float elapsedShakeTime = 0f; // Temps écoulé depuis le début du shake
    private Vector3 originalPos; // Position originale de la caméra

    [SerializeField] float maxMagnitude = 0.1f;
    [SerializeField] float minMagnitude = 0.01f;

    private void Start()
    {
        if (cameraTransform == null)
        {
            cameraTransform = GetComponent<Transform>();
        }
        originalPos = cameraTransform.localPosition;
        elapsedShakeTime = shakeDuration;
    }

    void Update()
    {
        if (elapsedShakeTime < shakeDuration)
        {
            // Générer un mouvement aléatoire dans un cube centré sur la position originale
            Vector3 shakePos = originalPos + Random.insideUnitSphere * shakeMagnitude;
            cameraTransform.localPosition = shakePos;
            elapsedShakeTime += Time.deltaTime;
        }
        else
        {
            // Réinitialiser la position de la caméra une fois la durée du shake écoulée
            cameraTransform.localPosition = originalPos;
        }
    }

    public void Shake(float force)
    {
        shakeMagnitude = minMagnitude * force;
        if (shakeMagnitude > maxMagnitude)
        {
            shakeMagnitude = maxMagnitude;
        }
        elapsedShakeTime = 0f;
    }
}
