using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsoItens : MonoBehaviour
{
    public float CD;
    public int quantidade;
    bool lan�a;
    float contador;
    public GameObject item;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.E) && !lan�a)
        {
            contador = CD;
            lan�a = true;
          
        }
        if(contador>0 && lan�a == true)
        {
            contador -= Time.deltaTime;
        }
        if(contador<=0)
        {
            lan�a = false;
        }
    }
    IEnumerator Usou()
    {
        yield return new WaitForSeconds(CD);
    }
}
