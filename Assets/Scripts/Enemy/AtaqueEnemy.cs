using System.Collections;
using UnityEngine;

public class AtaqueEnemy : MonoBehaviour
{
    public Vector3 posSpaw;
    public Vector3 posSpaw2;
    public Vector3 posSpaw3;
    public GameObject[] prefbAtk;
    public Transform spwnatk;
    public float tempoAteSpawn;
    [Header("___________Componentes________")]
    public MoveEnemy mv;
    IEnumerator atk;
    public Animator anim;
    [Header("___________Variaveis________")]
    int quantosInvocarVez;
    public float cdAtk;
    public float cdentreAtks;
    int quantidadeAtaques;
    GameManager gm;
    public GameObject alertaAtak;
    public Life vida;
    GameObject o = null;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.gmInstance; 
    }

    // Update is called once per frame
    void Update()
    {
        if(mv.comeouAtaque)
        {
            if (atk == null && vida.lifeAtual>0)
            {
                StartCoroutine(atacando());
                atk = atacando();
            }
        
        }
  
  
    }
    IEnumerator atacando()
    {
        int x = 0;
       
        quantosInvocarVez = Random.Range(1,2);
        quantidadeAtaques = Random.Range(1, 2);
        if (anim!=null)
        {
            for (int i = 0; i < quantidadeAtaques; i++)
            {
                if(vida.lifeAtual<=0)
                {
                    break;
                }
                x = Random.Range(0, 60);
          
                if (x < 30)
                {
   
                    for (int c = 0; c < quantosInvocarVez; c++)
                    {
                        Vector3 direcaoJogador = gm.movimentPlayer.transform.position - transform.position;
                        direcaoJogador.y = 0;
                        if (direcaoJogador.x >= 0)
                        {
                            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                        }
                        else
                        {
                            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                        }
                        if(alertaAtak!= null)
                        {
                            GameObject oo = Instantiate(alertaAtak, new Vector2(transform.position.x, transform.position.y +2.5f), Quaternion.identity);
                        }
                      
                        yield return new WaitForSeconds(0.7f);
 
                        anim.Play("Atack0", 0);
                        yield return new WaitForSeconds(tempoAteSpawn);
                        GameObject ob = Instantiate(prefbAtk[0], spwnatk.transform.position + posSpaw, spwnatk.rotation);
                        if (mv.orientacao == MoveEnemy.direo.direita)
                        {
                            ob.transform.rotation = Quaternion.Euler(0, 0, ob.transform.rotation.z);
                        }
                        else
                        {
                            ob.transform.rotation = Quaternion.Euler(0, 180, ob.transform.rotation.z);
                        }
                        yield return new WaitForSeconds(cdentreAtks/2);
                    }
             
                }
                if (x > 29 && x <= 60)
                {
                  

                    for (int c = 0; c < quantosInvocarVez; c++)
                    {
                        Vector3 direcaoJogador = gm.movimentPlayer.transform.position - transform.position;
                        direcaoJogador.y = 0;
                        if (direcaoJogador.x >= 0)
                        {
                            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                        }
                        else
                        {
                            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                        }
                        if (alertaAtak != null)
                        {
                            GameObject oo = Instantiate(alertaAtak, new Vector2(transform.position.x, transform.position.y + 2.5f), Quaternion.identity);
                        }
                       
                        yield return new WaitForSeconds(0.7f);
                        anim.Play("Atack1", 0);
                        yield return new WaitForSeconds(tempoAteSpawn);
                      
                        if (prefbAtk[1] == null)
                        {
                            GameObject ob = Instantiate(prefbAtk[0], spwnatk.transform.position + posSpaw2, spwnatk.rotation);
                            o = ob;
                        }
                        else
                        {
                            GameObject ob = Instantiate(prefbAtk[1], spwnatk.transform.position + posSpaw2, spwnatk.rotation);
                            o = ob;
                        }
                    
                        if (mv.orientacao == MoveEnemy.direo.direita)
                        {
                            o.transform.rotation = Quaternion.Euler(0, 0, o.transform.rotation.z);
                        }
                        else
                        {
                            o.transform.rotation = Quaternion.Euler(0, 180, o.transform.rotation.z);
                        }
                        yield return new WaitForSeconds(cdentreAtks / 2);
                    }
   
                }
                yield return new WaitForSeconds(cdentreAtks);
            }
        }
        yield return new WaitForSeconds(cdAtk);
        mv.comeouAtaque = false;
        atk = null;
    }
}
