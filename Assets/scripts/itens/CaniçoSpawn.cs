using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaniçoSpawn : MonoBehaviour
{
    Animator anim;
    public GameObject[] frutas;
    int index;
    int index2;
    public Transform[] pontoSpawn;

    BoxCollider2D box;
    public float CD;
    public bool pega;
    public bool dois;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        if(!dois)
        {
            index = Random.Range(0, frutas.Length);
            GameObject ob = Instantiate(frutas[index], pontoSpawn[0].position, Quaternion.identity);
        }
        else if(dois)
        {
            index = Random.Range(0, frutas.Length);
            index2 = Random.Range(0, frutas.Length);
            GameObject ob = Instantiate(frutas[index], pontoSpawn[0].position, Quaternion.identity);
            GameObject ob2 = Instantiate(frutas[index2], pontoSpawn[1].position, Quaternion.identity);
        }
      
        box = GetComponent<BoxCollider2D>();
       
    }

    // Update is called once per frame
    void Update()
    {


    }
    public void OnTriggerStay2D(Collider2D coll)
    {
        if(coll.CompareTag("Player"))
        {
            anim.Play("TocouPlayer");
        }
        if (coll.CompareTag("AtackPlayer"))
        {
            
            anim.Play("PlantaAtack");
            StartCoroutine(Atack());
            box.enabled = false;
        }

    }
  
    IEnumerator Atack()
    {
        pega = true;
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(Spaw());
        StopCoroutine(Atack());
    }
    IEnumerator Spaw()
    {
       
        yield return new WaitForSeconds(CD);
        pega = false;
        if (!dois)
        {
            index = Random.Range(0, frutas.Length);
            GameObject ob = Instantiate(frutas[index], pontoSpawn[index].position, Quaternion.identity);
        }
        else
        {
            index = Random.Range(0, frutas.Length);
            index2 = Random.Range(0, frutas.Length);
            GameObject ob = Instantiate(frutas[index], pontoSpawn[index2].position, Quaternion.identity);
            GameObject ob2 = Instantiate(frutas[index2], pontoSpawn[index2].position, Quaternion.identity);
        }
        box.enabled = true;
        StopCoroutine(Spaw());
    }
}
