using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportMapas : MonoBehaviour
{
    public string cena;


    bool podemudar;
    public Animator animCanvasPlaey;

    public GameObject seta;
    public GameObject particula; 

    public string PlayerTag;
    public Transform target;
    public float range;
    // Start is called before the first frame update

    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.2f);


    }

    // Update is called once per framex
    void Update()
    {
        UpdateTarget();
        if (target== null)
        {
            particula.SetActive(true);
            if (Input.GetKey(KeyCode.F) && podemudar == true)
            {
                StartCoroutine(transiçãoCena());
            }
        }
        else { particula.SetActive(false); }
   
    }
    private void OnTriggerEnter2D(Collider2D coll)
    {
        
        if(target == null)
        {
            if (coll.CompareTag("Player"))
            {

                animCanvasPlaey = GameObject.Find("CanvasPlayer").GetComponent<Animator>();
                seta.SetActive(true);
                podemudar = true;
            }
        }
       
    }
    private void OnTriggerExit2D(Collider2D coll)
    {
        if(target == null)
        {
            if (coll.CompareTag("Player"))
            {

                seta.SetActive(false);
                podemudar = false;

            }
        }
       
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(PlayerTag);
        float shortestestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            float distanceToenemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToenemy < shortestestDistance)
            {
                shortestestDistance = distanceToenemy;
                nearestEnemy = enemy;
            }
        }
        if (nearestEnemy != null && shortestestDistance <= range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }

    }
    public void mudarcena()
    {
       
        SceneManager.LoadScene(cena);
    }
    IEnumerator transiçãoCena()
    {
        yield return new WaitForSeconds(1f);
       // animCanvasPlaey.SetBool("Troca", true);
        yield return new WaitForSeconds(1f);
        mudarcena();
        StopCoroutine(transiçãoCena());
    }
    IEnumerator transiçãoCena2()
    {
        yield return new WaitForSeconds(1f);
       // animCanvasPlaey.SetBool("Troca", true);
        yield return new WaitForSeconds(1f);
        mudarcena();
        StopCoroutine(transiçãoCena2());
    }
 
}