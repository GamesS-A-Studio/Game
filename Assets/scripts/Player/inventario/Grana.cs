using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Grana : MonoBehaviour
{
    Arma inst;
    Life instV;
    Caracteristicas dolg;
    Move2 instM;
    public BoxCollider2D box;
    public int quantidademax;
    public int quantidademin;
    [SerializeField] float tamanho;
    [SerializeField] LayerMask playerlayer;
    public bool move;
    Transform playerpos;
    public float speed;

    public Animator pontoMana;
    public Animator pontoStamina;
    public Animator pontoVida;

    public bool Moeda;
    TextMeshProUGUI M;
    public bool Maninha;
    TextMeshProUGUI MA;
    public bool Stam;
    TextMeshProUGUI s;
    public bool Vidona;
    TextMeshProUGUI V;
    Rigidbody2D eb;
    public bool Podemover;
    CaniçoSpawn cani;
    // Start is called before the first frame update
    void Start()
    {
        instV = Life.instance;

        box.enabled = false;
        eb = GetComponent<Rigidbody2D>();
        cani = GameObject.Find("plantaConsumiveis").GetComponent<CaniçoSpawn>();
    }

    // Update is called once per frame
    void Update()
    {
        if(cani != null)
        {
            Podemover = cani.pega;
        }

        if(move)
        {
            playerpos= GameObject.Find("Player").GetComponent<Transform>();
            if(playerpos!= null)
            {
                transform.position = Vector2.MoveTowards(transform.position, playerpos.position, speed * Time.deltaTime);
            }
        }
    }
    private void FixedUpdate()
    {

        move = Physics2D.OverlapCircle(transform.position,tamanho, playerlayer);
    }
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.CompareTag("Player") && this.enabled)
        {
            dolg = coll.GetComponent<Caracteristicas>();
            if(Moeda)
            {
                if (dolg != null)
                {
                    int numero = Random.Range(quantidademin, quantidademax);
                    dolg.dinheiro = dolg.dinheiro + numero;
                    Destroy(gameObject);
                }
            }
            if(Maninha)
            {
              
                StartCoroutine(PegaM());
   
            }
            if(Vidona)
            {
                StartCoroutine(PegaV());

            }
            if(Stam)
            {
                StartCoroutine(PegaS());

            }

           
        }
    
    }
    IEnumerator PegaM()
    {
        eb.gravityScale = 0.5f;
        int numero = Random.Range(quantidademin, quantidademax);
        MA = GameObject.Find("PontoM").GetComponent<TextMeshProUGUI>();
        pontoMana = GameObject.Find("PontoM").GetComponent<Animator>();
        MA.text = numero.ToString();
        pontoMana.Play("PontoMA");
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
    IEnumerator PegaV()
    {
        eb.gravityScale = 0.5f;
        int numero = Random.Range(quantidademin, quantidademax);
        V = GameObject.Find("PontoV").GetComponent<TextMeshProUGUI>();
        pontoVida = GameObject.Find("PontoV").GetComponent<Animator>();
        V.text = numero.ToString();
        pontoVida.Play("PotoV");
        instV.vidaAtual = instV.vidaAtual + numero;
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
    IEnumerator PegaS()
    {
        eb.gravityScale = 0.5f;
        int numero = Random.Range(quantidademin, quantidademax);
        s = GameObject.Find("PontoS").GetComponent<TextMeshProUGUI>();
        pontoStamina = GameObject.Find("PontoS").GetComponent<Animator>();
        s.text = numero.ToString();
        pontoStamina.Play("pontoS");

        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
