
using System.Collections;
using UnityEngine;
using FirstGearGames.SmoothCameraShaker;
using static BoosArmadure;

public class RaioCristal : MonoBehaviour
{
    public LineRenderer line;
    public Transform trOlho;
    public int contagem;
    public int hitquant;
    public GameObject ParticleHit;
    public BoosArmadure b;
    public Animator anim;
    public Life vida;
    public bool ativaçãoBoos;
    public bool BoosFigth;
    public GameObject icon;
    public SpriteRenderer sp;
    public Material matoriginal;
    public Material matpost;

    [Header("Animação")]
    public float tempoAnimacao;
    IEnumerator en;
    public BoosArmadure bb;
    public Animator animBoos;
    public ShakeData hitShake;
    public bool boosMorto;
    GameManager gm;
    IEnumerator dest;
    public Animator rochaporta;
    public GameObject layApresentation;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.gmInstance;
        boosMorto = gm.Boss1;
        if(!boosMorto)
        {
            animBoos.Play("antes", 0);
            anim.Play("0", 0);
        }
     
    }

    // Update is called once per frame
    void Update()
    {
        boosMorto = gm.Boss1;
        if (!BoosFigth)
        {
            contagem = 0;

        }
        else
        {
            icon.SetActive(false);
        }
        if (!boosMorto)
        {
            if (vida.lifeAtual > 0)
            {
                line.SetPosition(0, transform.position);
                line.SetPosition(1, trOlho.transform.position);
                if (contagem >= hitquant)
                {
                    b.Ataques = acoes.Stun;
                    anim.SetBool("caiu", true);
                    sp.material = matpost;
                    contagem = 0;
                }
            }
            else
            {
                anim.SetBool("caiu", true);
                sp.material = matoriginal;
            }
            if (ativaçãoBoos && !BoosFigth)
            {
                if (Input.GetKey(KeyCode.E))
                {
                    icon.SetActive(false);
                    if (en == null)
                    {
                        StartCoroutine(inicia());
                        en = inicia();
                    }
                }
            }
          
        }
        else
        {
            if(dest == null)
            {
                StartCoroutine(morteBoos());
                dest = morteBoos();
            }
        }
        if(gm.vida.lifeAtual <= 0)
        {
            gm.song.boosFigth = false;
            gm.song.gameOVER = true;
        }
    }
    public void Rest()
    {
        rochaporta.SetBool("p", false);
        gm.song.musicaAmbiente.Stop();
        gm.song.musicacombat.Stop();
        gm.song.boosFigth = false;
        gm.song.gameOVER = true;
        sp.material = matoriginal;
        BoosFigth = false;
        dest = null;
        anim.Play("0", 0);
        en = null;
        contagem = 0;
        gm.song.musicaAmbiente.Play();
        gm.song.musicacombat.Play();
    }
    IEnumerator morteBoos ()
    {
        anim.Play("destruido", 0);
        gm.song.boosFigth = false;
        yield return null;
        
    }
    IEnumerator inicia()
    {
        anim.Play("começoucristal", 0);
        sp.material = matpost;
        rochaporta.SetBool("p", true);
        yield return new WaitForSeconds(tempoAnimacao+2f);
        animBoos.Play("nascimento",0);
        yield return new WaitForSeconds(1.5f);
       
        yield return new WaitForSeconds(2f);
        CameraShakerHandler.Shake(hitShake);
        yield return new WaitForSeconds(0.5f);
        gm.song.boosFigth = true;
        yield return new WaitForSeconds(1f);
        layApresentation.SetActive(true);
        yield return new WaitForSeconds(5f);
        layApresentation.SetActive(false);
        yield return new WaitForSeconds(0.55f);
        bb.enabled = true;
        yield return new WaitForSeconds(1f);
        bb.resetando = false;
        BoosFigth = true;
        bb.Ataques = acoes.none;
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {
            ativaçãoBoos = true;
            if(!BoosFigth)
            {
                icon.SetActive(true);
            }
        }
        if (coll.CompareTag("AtackPlayer"))
        {
            if (BoosFigth == true)
            {
                contagem++;
                GameObject ob = Instantiate(ParticleHit, transform.position, Quaternion.identity);
            }
 
        }
    }
    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {
            ativaçãoBoos = false;
            if (!BoosFigth)
            {
                icon.SetActive(false);
            }
        }
    }
}
