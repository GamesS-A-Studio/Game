using System.Collections;
using UnityEngine;

public class ObjetosDestrutiveis : MonoBehaviour
{
    public int quantDestruir;
    int count;
    public Animator anim;
    IEnumerator i;
    public GameObject fumacaDestruir;
    public GameObject fumaca;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(count >= quantDestruir)
        {
            if(i == null)
            {
                StartCoroutine(dd());
                i = dd();
            }
        }
    }
    IEnumerator dd()
    {
        
        anim.Play("Destroir", 0);
        yield return new WaitForSeconds(1f);
        GameObject ob = Instantiate(fumacaDestruir, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
    public void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.CompareTag("AtackPlayer"))
        {
            GameObject ob = Instantiate(fumaca, transform.position, Quaternion.identity);
            count++;
            anim.Play("Hit", 0);
        }
    }
}
