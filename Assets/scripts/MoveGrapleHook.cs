using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveGrapleHook : MonoBehaviour
{
    Referencias refinstance;
    public Transform bola;
    bool bater;
    float tempo;
    public bool volta;
    // Start is called before the first frame update
    void Start()
    {
        refinstance = Referencias.refInstance;
    }

    // Update is called once per frame
    void Update()
    {
       
        if(!bater)
        {
            if(tempo < 0.7f)
            {
                tempo += Time.deltaTime;
                transform.Translate(Vector2.right * Time.deltaTime * 70f);
            }
            else
            {
                retorno();
            }
        }
        if(volta)
        {
            retorno();
        }
        GetComponent<LineRenderer>().SetPosition(0, transform.position);
        GetComponent<LineRenderer>().SetPosition(1, bola.transform.position);
    }
    void retorno()
    {
        transform.position = Vector2.MoveTowards(transform.position, bola.position, 120f * Time.deltaTime);
        if (transform.position.x - bola.position.x < 5 && transform.position.y - bola.position.y < 5)
        {
            bola.GetComponentInParent<LookMira>().tocou = true;
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Player")) return;
        transform.position = transform.position;
        bater = true;
        Rigidbody2D rb = this.gameObject.GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
        refinstance.mv.jumpQuant = 1;
        bola.GetComponentInParent<SpringJoint2D>().connectedBody = rb;
        bola.GetComponentInParent<SpringJoint2D>().enabled = true;

    }
}
