using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocoEnemy : MonoBehaviour
{
    public Transform arma;
    public float range;
    public string playerTag = "Player";
    public float speed = 50000f;

    private Transform target;
    private Vector3 direction;
    private float atan2;

    private GameObject[] enemies;
    private float updateDelay = 0.2f;

    private void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, updateDelay);
        enemies = GameObject.FindGameObjectsWithTag(playerTag);
    }

    private void UpdateTarget()
    {
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

    private void Update()
    {
        UpdateTarget();
        if (target != null)
        {
            direction = (target.position - transform.position);
            atan2 = Mathf.Atan2(direction.y, direction.x);
            arma.transform.rotation = Quaternion.Lerp(arma.transform.rotation, Quaternion.Euler(0f, 0f, atan2 * Mathf.Rad2Deg), speed * Time.deltaTime);
        }
    }
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}