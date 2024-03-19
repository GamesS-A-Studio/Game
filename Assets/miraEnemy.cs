using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miraEnemy : MonoBehaviour
{
    GameManager gm;
    [Header("_______________________________________")]
    [Header("Mira")]
    [Header("_______________________________________")]
    public Transform target;
    public float rotationSpeed = 5f;
    void Start()
    {
        gm = GameManager.gmInstance;
    }
    void Update()
    {
        if(gm == null)
        {
            gm = GameManager.gmInstance;
        }
        else
        {
            if (target == null)
            {
                target = gm.movimentPlayer.transform;
            }
            else
            {
                Aim();
            }
        }
        
        
    }
  
    void Aim()
    {
        Vector3 mouse = new Vector3(target.position.x, target.position.y, 10f);
        Vector3 directionToMouse = mouse - transform.position;
        float angle = Mathf.Atan2(directionToMouse.y, directionToMouse.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);


    }
}
