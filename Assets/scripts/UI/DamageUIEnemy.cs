using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageUIEnemy : MonoBehaviour
{
    
    public GameObject damageText;
    public GameObject damageTextCrit;
    public GameObject shild;
    public Transform tr;
    public void EnemyHit(string valor)
    {

        if(damageText != null)
        {
            var damage = Instantiate(damageText, new Vector2(transform.position.x, transform.position.y + 15), Quaternion.identity);
            damage.SendMessage("SetText", valor);
        }
    }
    public void EnemyHit2(string valor2)
    {

        if (damageTextCrit != null)
        {
            var damage = Instantiate(damageTextCrit, new Vector2(transform.position.x, transform.position.y + 15), Quaternion.identity);
            damage.SendMessage("SetText2", valor2);
        }
    }
    public void QuebrouShild()
    {
        if (shild != null)
        {
            var damage = Instantiate(shild, transform.position, shild.transform.rotation);
        }
    }
}
