using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cura : MonoBehaviour
{
    public enum tipoRecurso
    {
        carne,
        dinheiro,
        sonifero
    }
    public tipoRecurso rec;
    int quantidadeRec;
    public bool cura;
    public bool recurso;
    bool podecurar = false;
    float quantidadeCura;
    public SpriteRenderer sp;
    public GameObject particle;
    GameManager gm;
    IEnumerator r;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.gmInstance;
        quantidadeCura = Random.Range(10, 30);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator cd()
    {
        yield return new WaitForSeconds(60f);
        sp.color = Color.red;
        particle.SetActive(true);
        podecurar =false;
        r = null;
    }
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(!podecurar)
        {
            if (coll.CompareTag("Player"))
            {
                if(cura)
                {
                    Life vida = coll.GetComponent<Life>();
                    if (vida != null)
                    {
                        gm.animatorui.Play("layoutHeal",0);
                        vida.lifeAtual += quantidadeCura;
                        sp.color = Color.black;
                        particle.SetActive(false);
                        if(r==null)
                        {
                            StartCoroutine(cd());
                            r = cd();
                        }
                        podecurar = true;
                    }
                }
                if(recurso)
                {
                    switch (rec)
                    {
                        case tipoRecurso.carne:
                            quantidadeRec = Random.Range(1, 3);
                            gm.addRec("Carne", quantidadeRec);
                            break;
                        case tipoRecurso.dinheiro:
                            quantidadeRec = Random.Range(2, 15);
                            gm.addRec("Dinheiro", quantidadeRec);
                            break;
                        case tipoRecurso.sonifero:
                            quantidadeRec = Random.Range(1, 2);
                            gm.addRec("Sonifero",quantidadeRec);
                            break;
                    }
                    Destroy(gameObject);
                }
            }
        }
   

    }
}
