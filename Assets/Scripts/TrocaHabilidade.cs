using System.Collections;
using UnityEngine;


public class TrocaHabilidade : MonoBehaviour
{
    public GameObject smokeTroca;
    GameManager gm;
    public float tempoExplodir;
    public float vel;
    IEnumerator tt;
    Rigidbody2D rb;
    bool ground;
    public LayerMask groundLayer;
    private Vector3 currentVelocity;
    public bool naomovimenta;
    public float forAtacao;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gm = GameManager.gmInstance;
        StartCoroutine(troca2());

    }

    // Update is called once per frame
    void Update()
    {
      
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {

            if(tt == null)
            {
                StartCoroutine(troca());
                tt = troca();
                naomovimenta = true;
            }
        }
       
     
    }
    private void FixedUpdate()
    {
        ground = Physics2D.OverlapCircle(transform.position, 0.3f, groundLayer);
        if (!naomovimenta)
        {
            if (!ground)
            {
                rb.velocity += Vector2.down * 5f * Time.deltaTime;
                transform.Translate(Vector2.right * vel * Time.deltaTime);

            }
            else
            {

                vel = 1.5f;

            }
        }
        else
        {
            rb.velocity = Vector2.zero;
            if(Vector2.Distance(gm.movimentPlayer.transform.position, transform.position)>1.5f)
            {
                gm.movimentPlayer.transform.position = Vector2.Lerp(gm.movimentPlayer.transform.position, transform.position, forAtacao * Time.deltaTime);
                Destroy(this.gameObject,0.5f);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
        
    }
    IEnumerator troca ()
    {
        GameObject ob = Instantiate(smokeTroca, gm.movimentPlayer.transform.position, Quaternion.identity);
        GameObject ob2 = Instantiate(smokeTroca, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.1f);

      
    }
    IEnumerator troca2()
    {
        if(tt == null)
        {
            yield return new WaitForSeconds(tempoExplodir);
            GameObject ob2 = Instantiate(smokeTroca, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
            Destroy(this.gameObject);
        }
        else
        {
            yield return null;
        }
       
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, 0.3f);
    }
}
