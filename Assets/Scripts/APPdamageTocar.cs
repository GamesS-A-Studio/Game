using System.Collections;
using TMPro;
using UnityEngine;


public class APPdamageTocar : MonoBehaviour
{
    public bool appDamage;
    public int damage;
    public GameObject uidamage;
    public GameObject particleHit;
    public GameObject[] ponto;
    public bool retorna;
    GameManager gm;
    private void Start()
    {
        gm = GameManager.gmInstance;
    }
    public void OnTriggerStay2D(Collider2D coll)
    {
 
        Life vida = null;
        
        if (vida == null)
        {
            vida = coll.GetComponentInChildren<Life>();
            vida = coll.GetComponent<Life>();
        }
        if (coll.CompareTag("Player"))
        {
            if (!appDamage)
            {
                if (vida != null)
                {
                    int s = damage;
                    damage = Random.Range(damage, damage * 2);

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
                    StartCoroutine(app());
                    if(retorna)
                    {
                        coll.transform.position = (transform.position.x > coll.transform.position.x)? ponto[0].gameObject.transform.position: ponto[1].gameObject.transform.position;
                    }
                }
            }
        }
    }
    public void OnParticleCollision(GameObject coll)
    {
        Life vida = null;
        if (vida == null)
        {
           
            vida = coll.GetComponent<Life>();
        }
        if (coll.CompareTag("Player"))
        {
            if (!appDamage)
            {
                if (vida != null)
                {
                    int s = damage;
                    damage = Random.Range(damage, damage * 2);

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
                    StartCoroutine(app());
                    if (retorna)
                    {
                        coll.transform.position = (transform.position.x > coll.transform.position.x) ? ponto[0].gameObject.transform.position : ponto[1].gameObject.transform.position;
                    }
                }
            }
        }
    }
    IEnumerator app()
    {
        appDamage = true;
        yield return new WaitForSeconds(0.6f);
        appDamage = false;
    }
}
