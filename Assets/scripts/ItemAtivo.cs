using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAtivo : MonoBehaviour
{
    public int idItem;
    int c;
    public GameObject[] itens;
    void Start()
    {
        c = idItem;
    }

    // Update is called once per frame
    void Update()
    {
      
        if(c != idItem)
        {
            ativadorIten();
            c = idItem;
        }
    }
    void ativadorIten()
    {
        for(int x =0; x < itens.Length; x++)
        {
            if (itens[x] != null)
            {
                if (x == idItem)
                {

                    itens[x].SetActive(true);
                }
                else
                {
                    itens[x].SetActive(false);
                }
            }
            else { break; }
 

        }
    }
}
