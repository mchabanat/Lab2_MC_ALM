using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraShake : MonoBehaviour
{
    private Transform cameraTransform; // R�f�rence � la transform de la cam�ra
    [SerializeField] float shakeDuration = 0.1f; // Dur�e de l'effet de shake
    [SerializeField] float shakeMagnitude = 0.1f; // Intensit� du shake
    private float elapsedShakeTime = 0f; // Temps �coul� depuis le d�but du shake
    private Vector3 originalPos; // Position originale de la cam�ra

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
            // G�n�rer un mouvement al�atoire dans un cube centr� sur la position originale
            Vector3 shakePos = originalPos + Random.insideUnitSphere * shakeMagnitude;
            cameraTransform.localPosition = shakePos;
            elapsedShakeTime += Time.deltaTime;
        }
        else
        {
            // R�initialiser la position de la cam�ra une fois la dur�e du shake �coul�e
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
