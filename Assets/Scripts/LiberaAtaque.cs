using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiberaAtaque : MonoBehaviour
{
    public bool libera;
    public GameObject icon;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.CompareTag("Player"))
        {
            Mira m = coll.GetComponentInChildren<Mira>();
            if(m!= null)
            {
                m.AtaquePronto = libera;
                if(icon != null)
                {
                    icon.SetActive(false);
                }
            }
        }
    }
}
