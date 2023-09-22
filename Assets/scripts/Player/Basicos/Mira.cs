using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mira : MonoBehaviour
{
    public Transform target;
    public float range = 5f;
    public string enemyTag = "Inimigo";


    private Vector3 direção;
    private float atan2;
    float speed = 50000f;

    public Transform arma;
    public bool miraautomatica;

    void Start()
    {

        InvokeRepeating("UpdateTarget", 0f, 0.2f);
    }
    void Update()
    {
        UpdateTarget();
       // Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        
        if(target != null)
        {
            if (miraautomatica == true)
            {
                direção = (target.position - transform.position);
                atan2 = Mathf.Atan2(direção.y, direção.x);
                arma.transform.rotation = Quaternion.Lerp(arma.transform.rotation, Quaternion.Euler(0f, 0f, atan2 * Mathf.Rad2Deg), speed * Time.deltaTime);
            }

        }

        if (miraautomatica == false)
        {
            Aim();
        }


    }
    void Aim()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 gunPos = Camera.main.WorldToScreenPoint(arma.transform.position);
        Vector3 g = mousePos - gunPos;
        atan2 = Mathf.Atan2(g.y, g.x);
        arma.transform.rotation = Quaternion.Lerp(arma.transform.rotation, Quaternion.Euler(0f, 0f, atan2 * Mathf.Rad2Deg), speed * Time.deltaTime);
    }
    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
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

}