using System.Collections;
using TMPro;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public bool enemy;
    public bool segue;
    public bool appDamage;
    public Transform playertr;
    GameManager gm;
    public int damage;
    Vector2 vec;
    public GameObject uidamage;
    public GameObject particleHit;
    [SerializeField] float speed = 7f;
    float vel = 0f;

    public float tempoSpeed;
    Vector2 distanciainicial;
    public float distancia;
    public string[] msg;
 
    private void Start()
    {
        gm = GameManager.gmInstance;
     
        if(segue)
        {
            GameObject ob = Instantiate(particleHit, transform.position, Quaternion.identity);
        }
        if (playertr == null && gm != null)
        {
            StartCoroutine(Vai());
            pegapo();
            playertr = gm.movimentPlayer.transform;
            vec = playertr.position; 
        }
    }
    void Update()
    {
        if(gm == null)
        {
            gm = GameManager.gmInstance;
            return;
        }
        if (playertr == null && gm != null)
        {
            playertr = gm.movimentPlayer.transform;
            vec = playertr.position;
        }
        if (segue)
        {
            transform.position = Vector2.MoveTowards(transform.position, vec, vel * Time.deltaTime);
            if (Vector2.Distance(transform.position, distanciainicial) >distancia)
            {
                GameObject ob = Instantiate(particleHit, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
            if (Vector2.Distance(transform.position, vec) < 0.2f)
            {
                GameObject ob = Instantiate(particleHit, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
        else
        {
            if (Vector2.Distance(distanciainicial, transform.position) > distancia)
            {
                Destroy(gameObject);
            }

            transform.Translate(Vector2.right * vel * Time.deltaTime);
        }
    }
    public void OnTriggerEnter2D(Collider2D coll)
    {
        Life vida = coll.GetComponent<Life>(); 
        if (vida == null)
        {
            vida = coll.GetComponentInChildren<Life>(); 
        }
        if (vida == null)
        {
            vida = coll.GetComponentInParent<Life>(); 
        }

        VisionEnemy vv = null;
      
        if(coll.CompareTag("Player") || coll.CompareTag("Inimigo"))
        {
            if (vv == null && vida != null)
            {
                vv = vida.v;
            }
            if (!appDamage)
            {
                if (vida != null)
                {
                    int s = damage;
                    damage = Random.Range(damage, damage * 2);
                    if (vv != null)
                    {
                        if (!enemy)
                        {
                                if (!vida.boos)
                                {
                                    if (vv.transPlayer == null)
                                    {
            
                                        GameObject ob = Instantiate(uidamage, new Vector2(Random.Range(coll.transform.position.x - 1, coll.transform.position.x + 1), coll.transform.position.y + 1.5f), Quaternion.identity);
                                        vida.appDamag(vida.lifeMax);
                                      
                                        ob.GetComponentInChildren<TextMeshProUGUI>().color = Color.red;
                                        int v = Random.Range(0, 100);
                                        if (v < 30)
                                        {
                                            ob.GetComponentInChildren<TextMeshProUGUI>().text = msg[0];
                                        }
                                        if (v >= 30 & v < 60)
                                        {
                                            ob.GetComponentInChildren<TextMeshProUGUI>().text = msg[1];
                                        }
                                        if (v >= 60)
                                        {
                                            ob.GetComponentInChildren<TextMeshProUGUI>().text = msg[2];
                                        }

                                    }
                                    else
                                    {
                                        GameObject ob = Instantiate(uidamage, new Vector2(Random.Range(coll.transform.position.x - 1, coll.transform.position.x + 1), coll.transform.position.y + 1.5f), Quaternion.identity);
                                        if (damage > s * 2)
                                        {

                                            ob.GetComponentInChildren<TextMeshProUGUI>().color = Color.red;
                                            ob.GetComponentInChildren<TextMeshProUGUI>().text = damage.ToString();
                                        }
                                        else
                                        {
                                            ob.GetComponentInChildren<TextMeshProUGUI>().text = damage.ToString();
                                        }
                                          vida.appDamag(damage);
                                      

                                    }
                                }
                                else
                                {
                                    if (damage > s * 2)
                                    {
                                        GameObject ob = Instantiate(uidamage, new Vector2(Random.Range(coll.transform.position.x - 1, coll.transform.position.x + 1), coll.transform.position.y + 1.5f), Quaternion.identity);

                                        ob.GetComponentInChildren<TextMeshProUGUI>().color = Color.red;
                                        ob.GetComponentInChildren<TextMeshProUGUI>().text = damage.ToString();
                                    }
                                    else
                                    {
                                        GameObject ob = Instantiate(uidamage, new Vector2(Random.Range(coll.transform.position.x - 1, coll.transform.position.x + 1), coll.transform.position.y + 1.5f), Quaternion.identity);
                                        ob.GetComponentInChildren<TextMeshProUGUI>().text = damage.ToString();
                                    }
                                     vida.appDamag(damage);
                                  
                                }
                        }
                        else
                        {
                             GameObject ob = Instantiate(uidamage, new Vector2(Random.Range(coll.transform.position.x - 1, coll.transform.position.x + 1), coll.transform.position.y + 1.5f), Quaternion.identity);
                             if (damage > s + 5)
                             {

                                ob.GetComponentInChildren<TextMeshProUGUI>().color = Color.red;
                                ob.GetComponentInChildren<TextMeshProUGUI>().text = damage.ToString();
                             }
                             else
                             {
                                ob.GetComponentInChildren<TextMeshProUGUI>().text = damage.ToString();
                             }
                              
                                vida.appDamag(damage);
                               
                        }
                         GameObject ob2 = Instantiate(particleHit, coll.transform.position, Quaternion.identity);

                    }
                    else
                    {
                        if (damage > s + 8)
                        {
                            GameObject ob = Instantiate(uidamage, new Vector2(Random.Range(coll.transform.position.x - 1, coll.transform.position.x + 1), coll.transform.position.y + 1.5f), Quaternion.identity);
                            ob.GetComponentInChildren<TextMeshProUGUI>().color = Color.red;
                            ob.GetComponentInChildren<TextMeshProUGUI>().text = damage.ToString();
                        }
                        else
                        {
                            GameObject ob = Instantiate(uidamage, new Vector2(Random.Range(coll.transform.position.x - 1, coll.transform.position.x + 1), coll.transform.position.y + 1.5f), Quaternion.identity);
                            ob.GetComponentInChildren<TextMeshProUGUI>().color = Color.yellow;
                            ob.GetComponentInChildren<TextMeshProUGUI>().text = damage.ToString();
                        }
                        gm.animatorui.Play("layDmg");
                        vida.appDamag(damage);
                    
                        GameObject ob2 = Instantiate(particleHit, coll.transform.position, Quaternion.identity);
                    }

                    Destroy(gameObject, 0.3f);
                }
                appDamage = true;
            }
        }
      
        
    }
    public void pegapo()
    {
        distanciainicial = transform.position;
    }
    IEnumerator Vai()
    {
        yield return new WaitForSeconds(tempoSpeed);
        vec = playertr.position;
        vel = speed;
    }
}
