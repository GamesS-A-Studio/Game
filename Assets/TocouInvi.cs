using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TocouInvi : MonoBehaviour
{
    public Color cor;
    Color cororiginal;
    Color cororiginal2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.CompareTag("Player"))
        {
            SpriteRenderer[] sp  = this.gameObject.GetComponents<SpriteRenderer>();
            SpriteRenderer[] sp2  = this.gameObject.GetComponentsInChildren<SpriteRenderer>();
            foreach(SpriteRenderer ss in sp)
            {
                if(ss != null)
                {
                    cororiginal = ss.color;
                    ss.color = cor;
                }
            }
            foreach (SpriteRenderer s2 in sp2)
            {
                if (s2 != null)
                {
                    cororiginal2 = s2.color;
                    s2.color = cor;
                }
            }
        }
    }
    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {
            SpriteRenderer[] sp = this.gameObject.GetComponents<SpriteRenderer>();
            SpriteRenderer[] sp2 = this.gameObject.GetComponentsInChildren<SpriteRenderer>();
            foreach (SpriteRenderer ss in sp)
            {
                if (ss != null)
                {
                    ss.color = cororiginal;
                }
            }
            foreach (SpriteRenderer s2 in sp2)
            {
                if (s2 != null)
                {
                    s2.color = cororiginal2;
                }
            }
        }
    }
}
