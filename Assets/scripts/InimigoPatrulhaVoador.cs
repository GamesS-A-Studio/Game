using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigoPatrulhaVoador : MonoBehaviour
{
    public float speed = 5f; // velocidade de movimento do inimigo

    public LayerMask groundLayer; // camada dos objetos que são chão
    public LayerMask wallLayer; // camada dos objetos que são parede
    public LayerMask playerLayer; // camada dos objetos que são parede


    public float tamanoDetecção;
    public float tamanoDetecçãoPlayer;
    bool retorno;
    Vector2 posiçãoinicial;
    Vector2 movRandom;
    bool colidiuParede;
    bool playerdetectou;
    public enum opções
    {
        patrulha,
        atack,
        retornaPosição
    };
    opções select;

    public bool tematack;
    public float tempoatack;
    public float tempoanimação;
    public int quantosataques;
    public float CDAtack;
    bool atacou;
    Animator anim;
    public GameObject projetilAtack;
    public Transform spawn;
    private Transform playertrans;
    void Start()
    {
        posiçãoinicial = transform.position;
        anim = this.GetComponent<Animator>();
        playertrans = GameObject.Find("Player").transform;
    }

    void Update()
    {
        
        playertrans = GameObject.Find("Player").transform;
        switch (select)
        {
            case opções.patrulha:
                if(!retorno)
                {
                    StartCoroutine(Posi());
                    retorno = true;
                }
                transform.position = Vector2.MoveTowards(transform.position, movRandom, speed * Time.deltaTime);
                break;
            case opções.atack:
                if(!atacou)
                {
                    StartCoroutine(atacando());
                    atacou = true;
                }
                break;
            case opções.retornaPosição:
                transform.position = Vector2.MoveTowards(transform.position, posiçãoinicial, speed * Time.deltaTime);
                break;
        }
      
        if (colidiuParede && playerdetectou) { select = opções.retornaPosição; }
        if (playerdetectou) { select = opções.atack; }
        else
        {
            if (Vector2.Distance(transform.position, posiçãoinicial) < 0.3f)
            {
                select = opções.patrulha;
            }
            if (Vector2.Distance(transform.position, movRandom) < 0.3f)
            {
                select = opções.retornaPosição;
            }

        }
      
    
    }
    private void FixedUpdate()
    {
        colidiuParede = Physics2D.OverlapCircle(transform.position, tamanoDetecção, groundLayer);
        colidiuParede = Physics2D.OverlapCircle(transform.position, tamanoDetecção, wallLayer);
        playerdetectou = Physics2D.OverlapCircle(transform.position, tamanoDetecçãoPlayer, playerLayer);
    }
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, tamanoDetecção);
        Gizmos.DrawWireSphere(transform.position, tamanoDetecçãoPlayer);
    }
    IEnumerator atacando()
    {
        for(int x = 0; x <quantosataques; x ++)
        {
            anim.Play("Atack", 0);
            yield return new WaitForSeconds(tempoanimação);
            GameObject ob2 = Instantiate(projetilAtack, spawn.position, spawn.rotation);
            yield return new WaitForSeconds(tempoatack);
        }
        yield return new WaitForSeconds(CDAtack);
       
        if(Vector2.Distance(transform.position, playertrans.position)> tamanoDetecçãoPlayer)
        {
            select = opções.retornaPosição;
        }
        else { select = opções.atack; }
        atacou = false;
    }
    IEnumerator Posi()
    {
        movRandom = new Vector2(Random.Range(transform.position.x - 50, transform.position.x + 30), Random.Range(transform.position.y - 30, transform.position.y + 30));
        yield return new WaitForSeconds(8f);
        retorno = false;
    }
}