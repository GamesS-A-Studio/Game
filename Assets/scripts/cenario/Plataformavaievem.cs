using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataformavaievem : MonoBehaviour
{
    public SliderJoint2D slider;
    public JointMotor2D aux;
    public int veldesce;
    public int velup;
    public AudioSource som;
    public bool pancada;
    // Start is called before the first frame update
    void Start()
    {
        aux = slider.motor;
    }

    // Update is called once per frame
    void Update()
    {
        if(slider.limitState == JointLimitState2D.LowerLimit)
        {
            aux.motorSpeed = veldesce;
            if(pancada == false)
            {
                som.enabled = false;
            }
           
            slider.motor = aux;
        }
        if (slider.limitState == JointLimitState2D.UpperLimit)
        {
            aux.motorSpeed = velup;
            if (pancada == false)
            {
                som.enabled = true;
            }    
            slider.motor = aux;
        }

    }

}
