using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class traponderSc : MonoBehaviour
{
    public string[] historia;
    public Transform[] waypoints;
    public GameObject icon;
    public float speed;
    bool click;
    GameManager gm;
    IEnumerator u;

    public Rigidbody2D rb;
    public MovimentPlayer mv;
    public Mira mir;
    public SpriteRenderer sp;
    public GameObject obplayer;
    public float duracaoTextos;
    public float distance;
    Vector2 original;
    public int index;
    bool podeir;
    int s;
    IEnumerator n;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.gmInstance;
        s = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(obplayer == null)
        {
            obplayer = gm.obAura.gameObject;
            return;
        }
        if(mir == null)
        {
            mir = gm.mir;
            return;
        }
        if(mv == null)
        {
            mv = gm.movimentPlayer;
            return;
        }
        if(rb == null)
        {
            rb = gm.movimentPlayer.rb;
            return;
        }
        if(click)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {

                if (gm.hh.Transpoder)
                {
                    StartCoroutine(Vai());
                    podeir = true;
                    u = Vai();
                }
                else
                {
                    if (n == null)
                    {
                        StartCoroutine(nao());
                        n = nao();
                    }
                }
            }
          
        }
        if(!gm.terminouHistoria && podeir)
        {
            if(historia.Length>index)
            {
                if (s != index)
                {
                    gm.invocaTexto(duracaoTextos, historia[index], this.GetComponent<traponderSc>());
                    s = index;
                }
            }
            else
            {
                gm.terminouHistoria = true;
            }

        }
      
    }
    IEnumerator nao()
    {
        gm.invocaTexto(3f, "\r\nYou do not have the required item", this.GetComponent<traponderSc>());
        yield return null;
    }
    IEnumerator Vai()
    {
        rb.gravityScale = 0;
        mv.enabled = false;
        mir.enabled = false;
        sp.enabled = false;
        obplayer.SetActive(true);
        gm.arma1.SetActive(false);
        gm.arma2.SetActive(false);
        gm.colider.SetActive(false);
        sp.gameObject.layer = 4;
        for(int x = 0; x < waypoints.Length; x++)
        {
            float distance = Vector2.Distance(gm.movimentPlayer.transform.position, waypoints[x].transform.position);
            while (distance > 1f)
            {
                gm.movimentPlayer.transform.position = Vector2.MoveTowards(gm.movimentPlayer.transform.position, waypoints[x].transform.position, speed * Time.deltaTime);
                distance = Vector2.Distance(gm.movimentPlayer.transform.position, waypoints[x].transform.position);
                yield return null;
            }
        }
        yield return new WaitForSeconds(2f);
        u = null;
        obplayer.SetActive(false);
        gm.arma1.SetActive(true);
        gm.arma2.SetActive(true);
        gm.colider.SetActive(true);
        sp.gameObject.layer = 6;
        mir.enabled = true;
        sp.enabled = true;
        mv.enabled = true;
        rb.gravityScale = 3;

        
    }
    private void OnTriggerEnter2D(Collider2D coll)
    {

        if (coll.CompareTag("Player"))
        {
         

            if (!click)
            {
                sp = coll.GetComponent<SpriteRenderer>();
                icon.SetActive(true);
                click = true;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {
            icon.SetActive(false);
            click = false;
        }
    }
}
