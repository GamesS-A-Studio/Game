using System.Collections;
using UnityEngine;

public class VisionEnemy : MonoBehaviour
{
    public bool ativaGizmos;
    GameManager gm;
    public Collider2D coll;
    public Collider2D coll2;
    public Vector2 boxNormal;
    public Vector2 box2;
    Vector2 boxalterado;
    public float alcance;
    float tempoReset =6;
    public LayerMask layerPlayer;
    public Transform transPlayer;
    IEnumerator reset;
    public MoveEnemy mv;

    bool achou;
    public GameObject iconAlerta;
    public GameObject iconPerdeu;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.gmInstance;
        boxalterado = boxNormal;
    }

    // Update is called once per frame
    void Update()
    {

        
    }
    private void FixedUpdate()
    {
        coll = Physics2D.OverlapBox(transform.position, boxNormal, 6, layerPlayer);
        coll2 = Physics2D.OverlapBox(new Vector2(transform.position.x, transform.position.y), box2, 6, layerPlayer);

        if (coll != null && coll.tag == "Player")
        {
            if (gm.visibilidadePlayer == VisibilidadePlayer.Visivel)
            {
                
                if (!achou)
                {
                
                    if (coll2 != null && coll2.tag == "Player")
                    {
                        invocaCoisas(iconAlerta, new Vector2(transform.position.x, transform.position.y + 2f));
                        box2.x = 18;
                        box2.y = 10;
                        transPlayer = coll2.transform;
                        achou = true;
                    }
                }
            }
            else 
            {
                transPlayer = null;

            }

        }
        if (coll2 == null && transPlayer != null)
        {
            
            if(reset == null)
            {
                StartCoroutine(resetando());
                reset = resetando();
            }
           
        }
    }

    public void invocaCoisas(GameObject ob, Vector2 vec)
    {
        GameObject oo = Instantiate(ob, vec, Quaternion.identity);
        oo.transform.SetParent(this.transform);
    }

    IEnumerator resetando()
    {
        mv.rb.velocity = Vector2.zero;
        mv.stopAll = true;

        yield return new WaitForSeconds(1f);
        mv.transform.rotation = Quaternion.Euler(0, 180, 0);
        yield return new WaitForSeconds(1f);
        mv.transform.rotation = Quaternion.Euler(0, 0, 0);
        yield return new WaitForSeconds(1f);
        mv.transform.rotation = Quaternion.Euler(0, 180, 0);
        invocaCoisas(iconPerdeu, new Vector2(transform.position.x, transform.position.y + 2f));
        yield return new WaitForSeconds(tempoReset);

        boxNormal = boxalterado;
        achou = false;
        coll2 = null;
        mv.stopAll = false;
        transPlayer = null;
        reset = null;
    }
    public void OnDrawGizmos()
    {
        if(ativaGizmos)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(transform.position, boxNormal);

            if (mv.orientacao == MoveEnemy.direção.direita)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawWireCube(new Vector2(transform.position.x, transform.position.y),box2);
            }
            else
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawWireCube(new Vector2(transform.position.x , transform.position.y),box2);
            }

        }


    }
}
