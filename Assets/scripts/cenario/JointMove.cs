using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointMove : MonoBehaviour
{
    public SliderJoint2D slider;
    public JointMotor2D aux;


    void Start()
    {
        aux = slider.motor;  
    }

    // Update is called once per frame
    void Update()
    {
        if (slider.limitState == JointLimitState2D.LowerLimit)
        {
            aux.motorSpeed = 30;
            slider.motor = aux;
        }

        if (slider.limitState == JointLimitState2D.UpperLimit)
        {
            aux.motorSpeed = -20;
            slider.motor = aux;
        }
    }
}
