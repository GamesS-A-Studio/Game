using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destativa : MonoBehaviour
{
    public APPdamageTocar pp;
    public Life vida;
    public GameObject skin;
    public GameObject detection;
    public MoveEnemy mv;
    IEnumerator rot;
    public Collider2D coll;
    AtaqueEnemy atk;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void Update()
    {
        if(atk == null)
        {
            atk = GetComponentInParent<AtaqueEnemy>();
        }
    }
    // Update is called once per frame
    public void começa()
    {
        if(rot == null)
        {
            StartCoroutine(Rrotina());
            rot = Rrotina();    
        }
    }
    IEnumerator Rrotina()
    {
        pp.enabled = false;
        mv.enabled = false;
        atk.enabled = false;
        skin.SetActive(false);
        coll.enabled = false;
        detection.SetActive(false);
        yield return new WaitForSeconds(180f);
        vida.lifeAtual = vida.lifeMax;
        pp.enabled = true;
        atk.enabled =true;
        coll.enabled = true;
        mv.enabled = true;
        skin.SetActive(true);
        detection.SetActive(true);
        rot = null;
    }
}
