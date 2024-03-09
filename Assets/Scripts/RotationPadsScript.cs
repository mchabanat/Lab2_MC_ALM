using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class RotationPadsScript : MonoBehaviour
{
    [SerializeField] private float restPosition = 0f;
    [SerializeField] private float pressedPosition = 45f;
    [SerializeField] private float hitStrength = 10000f;
    [SerializeField] private float flipperDamper = 150f;

    [SerializeField] private string inputName;
    private HingeJoint hinge;
    private JointSpring spring;
    void Start()
    {
        hinge = GetComponent<HingeJoint>();
        hinge.useSpring = true;
        spring = new JointSpring();
    }


    void Update()
    {
        spring.spring = hitStrength;
        spring.damper = flipperDamper;

        if (Input.GetAxis(inputName) == 1)
        {
            spring.targetPosition = pressedPosition;
        }
        else
        {
            spring.targetPosition = restPosition;
        }

        hinge.spring = spring;
        hinge.useLimits = true;
    }
}
