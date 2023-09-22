using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Life : MonoBehaviour
{

    public static Life instance;
    public Caracteristicas cac;
    public Image lifebar;
    public Image ArmorBarra;
    public GameObject barras;
    public bool souPlayer;

    [SerializeField]
    public int vidamax = 150;
    [SerializeField]
    public float vidaAtual = 150;
    public float Armadura;
    public float maxArmor;
    public bool regenArmor;
    public float CDArmor;
    float regenvida;
    bool ativaregen;



    public Vector2 boxsize;
    public LayerMask layerplayer;
    bool player;
    bool morreu;
    IEnumerator Morte;
    public bool temDrop;
    public GameObject[] drop;
    public GameObject ExplosionMorte;
    public int xpMin;
    public int xpMax;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        vidamax += cac.vida;
        vidaAtual = vidamax;
        Armadura = maxArmor;

    }
    public void Update()
    {
        if (ativaregen == true)
        {
            vidaAtual = vidaAtual + regenvida * Time.deltaTime;

        }
        if (vidaAtual < vidamax)
        {
            ativaregen = true;
        }
        if (vidaAtual >= vidamax)
        {
            vidaAtual = vidamax;
            ativaregen = false;
        }
        if (vidaAtual < 0)
        {
            morreu = true;
            vidaAtual = 0;
        }
     
        if (Armadura > maxArmor)
        {
            Armadura = maxArmor;
        }
        if (Armadura <= 0 && regenArmor == false)
        {
            StartCoroutine(RegenArmor());
            regenArmor= true;
        }  
        if(morreu)
        {
            Morte = morte();
            if(!temDrop)
            {
                StartCoroutine(Morte);
                temDrop = true;
            }
        }
  
    }
    private void FixedUpdate()
    {
        atualizaBarras();
        if (!souPlayer)
        {
            areaAtivaçãoBarra();
        }  
    }
    public void atualizaBarras()
    {
        if (vidaAtual > 0)
        {
            lifebar.fillAmount = (float)vidaAtual / vidamax;
            ArmorBarra.fillAmount = (float)Armadura / maxArmor;

        }
    }
    IEnumerator RegenArmor()
    {
        yield return new WaitForSeconds(CDArmor);
        for(int x = 0; x < maxArmor; x++)
        {
            Armadura += 5;
            yield return new WaitForSeconds(0.1f);
        }
        regenArmor = false;
    }
    public void areaAtivaçãoBarra()
    {
        player = Physics2D.OverlapBox(transform.position, boxsize, 0, layerplayer);
        if(player)
        {
            barras.SetActive(true);
        }
        else
        {
            barras.SetActive(false);
        }
    }
    IEnumerator morte()
    {
        int n = Random.Range(0, drop.Length);
        GameObject ob = Instantiate(drop[n], transform.position, Quaternion.identity);
        ColetaIten cc = ob.GetComponent<ColetaIten>();
        int q = Random.Range(1, 6);
        gameObject.GetComponent<MoveEnemy>().enabled = false;
        GameObject ex = Instantiate(ExplosionMorte, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.3f);
        int xp = Random.Range(xpMin, xpMax);
        Caracteristicas c = GameObject.Find("Player").GetComponent<Caracteristicas>();
        yield return new WaitForSeconds(0.2f);
        c.xpGanho = xp;
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, boxsize);
    }

}
