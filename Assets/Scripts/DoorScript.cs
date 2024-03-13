using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    [SerializeField] private float restPosition = 0f;
    [SerializeField] private float pressedPosition = -90f;
    [SerializeField] private float hitStrength = 10000f;
    [SerializeField] private float flipperDamper = 150f;

    private HingeJoint hinge;
    private JointSpring spring;

    void Start()
    {
        hinge = GetComponent<HingeJoint>();
        hinge.useSpring = true;
        spring = new JointSpring();
    }

    public void openDoor()
    {
        spring.targetPosition = pressedPosition;
    }

    public void closeDoor()
    {
        spring.targetPosition = restPosition;
    }


    void Update()
    {
        spring.spring = hitStrength;
        spring.damper = flipperDamper;

        hinge.spring = spring;
        hinge.useLimits = true;
    }
}
