using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ScriptInventario : MonoBehaviour
{
  
    public GameObject iv;
    bool aberto;
    public Arma arm;
    public Move2 movim;
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            abreinv();

        }
    }
   
    public void abreinv()
    {
        aberto = !aberto;
        if (aberto)
        {
            arm.estado = Estates.Menu;
            movim.estados = Estates.Menu;
            iv.SetActive(true);

        }
        else
        {
            arm.estado = Estates.Parado;
            movim.estados = Estates.Parado;
            iv.SetActive(false);

        }

    }
    
}
