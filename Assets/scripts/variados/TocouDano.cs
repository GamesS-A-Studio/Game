using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TocouDano : MonoBehaviour
{
    public Caracteristicas cac;
    int damageMax;
    int damageMin;
    public GameObject hit;
    public GameObject damageUI, damageCritUI;
    float chanCrit;
    float number;
    public float cdEntreAtaques;

    // Start is called before the first frame update
    void Start()
    {
        damageMax = cac.Dano * 2;
        damageMin = cac.Dano;
        chanCrit = cac.critico;
    }
    private void Update()
    {
        if(number>0)
        {
            number -= 1 * Time.deltaTime;
        }
        if(number<0)
        {
            number = 0;
        }
    }
    public void OnTriggerEnter2D(Collider2D coll)
    {
        Life vida = coll.GetComponent<Life>();
     
        if (coll.CompareTag("Player")|| coll.CompareTag("Inimigo"))
        {
            if(number <= 0)
            {
                GameObject ob2 = Instantiate(hit, coll.transform.position, Quaternion.identity);
                if (vida != null)
                {
                    float ctr = Random.Range(0, 100);
                    int dano = Random.Range(damageMin, damageMax);
                    if (ctr <= chanCrit)
                    {
                        dano += damageMax;
                        GameObject ob = Instantiate(damageCritUI, coll.transform.position, Quaternion.identity);
                        TextMeshProUGUI textodano = ob.GetComponentInChildren<TextMeshProUGUI>();
                        textodano.text = dano.ToString();
                    }
                    else
                    {
                        GameObject ob = Instantiate(damageUI, coll.transform.position, Quaternion.identity);
                        TextMeshProUGUI textodano = ob.GetComponentInChildren<TextMeshProUGUI>();
                        textodano.text = dano.ToString();
                    }
                    for (int x = 0; x < dano; x++)
                    {
                        if (vida.Armadura > 0)
                        {
                            vida.Armadura -= 1;
                        }
                        else if (vida.Armadura <= 0)
                        {
                            vida.Armadura = 0;
                            vida.vidaAtual -= 1;
                        }
                    }
                    number = cdEntreAtaques;
                }
            }
          

        }

    }
  

}
