using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigoPatrulhaVoador : MonoBehaviour
{
    public float speed = 5f; // velocidade de movimento do inimigo

    public LayerMask groundLayer; // camada dos objetos que s�o ch�o
    public LayerMask wallLayer; // camada dos objetos que s�o parede
    public LayerMask playerLayer; // camada dos objetos que s�o parede


    public float tamanoDetec��o;
    public float tamanoDetec��oPlayer;
    bool retorno;
    Vector2 posi��oinicial;
    Vector2 movRandom;
    bool colidiuParede;
    bool playerdetectou;
    public enum op��es
    {
        patrulha,
        atack,
        retornaPosi��o
    };
    op��es select;

    public bool tematack;
    public float tempoatack;
    public float tempoanima��o;
    public int quantosataques;
    public float CDAtack;
    bool atacou;
    Animator anim;
    public GameObject projetilAtack;
    public Transform spawn;
    private Transform playertrans;
    void Start()
    {
        posi��oinicial = transform.position;
        anim = this.GetComponent<Animator>();
        playertrans = GameObject.Find("Player").transform;
    }

    void Update()
    {
        
        playertrans = GameObject.Find("Player").transform;
        switch (select)
        {
            case op��es.patrulha:
                if(!retorno)
                {
                    StartCoroutine(Posi());
                    retorno = true;
                }
                transform.position = Vector2.MoveTowards(transform.position, movRandom, speed * Time.deltaTime);
                break;
            case op��es.atack:
                if(!atacou)
                {
                    StartCoroutine(atacando());
                    atacou = true;
                }
                break;
            case op��es.retornaPosi��o:
                transform.position = Vector2.MoveTowards(transform.position, posi��oinicial, speed * Time.deltaTime);
                break;
        }
      
        if (colidiuParede && playerdetectou) { select = op��es.retornaPosi��o; }
        if (playerdetectou) { select = op��es.atack; }
        else
        {
            if (Vector2.Distance(transform.position, posi��oinicial) < 0.3f)
            {
                select = op��es.patrulha;
            }
            if (Vector2.Distance(transform.position, movRandom) < 0.3f)
            {
                select = op��es.retornaPosi��o;
            }

        }
      
    
    }
    private void FixedUpdate()
    {
        colidiuParede = Physics2D.OverlapCircle(transform.position, tamanoDetec��o, groundLayer);
        colidiuParede = Physics2D.OverlapCircle(transform.position, tamanoDetec��o, wallLayer);
        playerdetectou = Physics2D.OverlapCircle(transform.position, tamanoDetec��oPlayer, playerLayer);
    }
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, tamanoDetec��o);
        Gizmos.DrawWireSphere(transform.position, tamanoDetec��oPlayer);
    }
    IEnumerator atacando()
    {
        for(int x = 0; x <quantosataques; x ++)
        {
            anim.Play("Atack", 0);
            yield return new WaitForSeconds(tempoanima��o);
            GameObject ob2 = Instantiate(projetilAtack, spawn.position, spawn.rotation);
            yield return new WaitForSeconds(tempoatack);
        }
        yield return new WaitForSeconds(CDAtack);
       
        if(Vector2.Distance(transform.position, playertrans.position)> tamanoDetec��oPlayer)
        {
            select = op��es.retornaPosi��o;
        }
        else { select = op��es.atack; }
        atacou = false;
    }
    IEnumerator Posi()
    {
        movRandom = new Vector2(Random.Range(transform.position.x - 50, transform.position.x + 30), Random.Range(transform.position.y - 30, transform.position.y + 30));
        yield return new WaitForSeconds(8f);
        retorno = false;
    }
}