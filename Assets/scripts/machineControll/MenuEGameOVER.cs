using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuEGameOVER : MonoBehaviour
{
    private string nomeCenaAtual;
    public PosiçãoPlayer pos;
    public Life life;
    public GameObject gameover;
    public bool musicagameover;
    int musicaatual;
    Transform chekpont;
    bool altoriza;
    Animator anim;
    Move2 mov;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        mov = GetComponent<Move2>();
    }

    // Update is called once per frame
    void Update()
    {
        if (life.vidaAtual <= 0)
        {
            if (musicagameover == false)
            {
                StartCoroutine(Gameover());
            }
        }
    }
    public void OnTriggerStay2D(Collider2D coll)
    {
        if(coll.CompareTag("CheckPoint"))
        {
            Transform point = coll.GetComponent<Transform>();
            if(point != null)
            {
                chekpont = point;
            }
        }
    }
    public void Reestar()
    {
        if(altoriza)
        {
           // musica.indexCena = musicaatual;
            musicagameover = false;
            transform.position = chekpont.transform.position;
            gameover.SetActive(false);
            life.vidaAtual = life.vidamax;
            Time.timeScale = 1;

            anim.Play("PlayerIdle", 0);
            SceneManager.LoadScene(nomeCenaAtual);
        }

    }
    IEnumerator Gameover()
    {
        nomeCenaAtual = SceneManager.GetActiveScene().name;
        musicagameover = true;
        yield return new WaitForSeconds(0.3f);
        gameover.SetActive(true);
        Time.timeScale = 0;
        altoriza = true;
        StopCoroutine(Gameover());
    }
}
