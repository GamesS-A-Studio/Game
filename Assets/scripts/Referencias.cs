using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Referencias : MonoBehaviour
{
    public static Referencias refInstance;
    public Move2 mv;
    public Arma arm;
    public Caracteristicas cac;
    private void Awake()
    {
        refInstance = this;
    }
}
