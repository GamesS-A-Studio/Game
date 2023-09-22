using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Arma : MonoBehaviour
{
    public Rigidbody2D rb;
    public float TempoAnim1;
    public float cdAtk;
    bool lança2, shild;
    int numero;
    Animator anim;
    public Move2 mv;
    public float cooeficienteregenMana;
    public SpriteRenderer spPlayer;

    public Estates estado;
    public bool _menuAtivo;
    public float GastoStamina;
    public Caracteristicas cac;
    public bool dash;
    float shildDesarmer;
    private void Start()
    {

        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        string est = estado.ToString();
        switch (est)
        {
            case "Menu":
                _menuAtivo = true;
                break;
            default:
                _menuAtivo = false;
                break;
        }
        if(_menuAtivo)
        {
            anim.StopPlayback();
            anim.Play("PIdle", -1);
            
        }
        else
        {
            AtaqueMouse();
        }
        if(shild == true)
        {
            shildDesarmer += 1 * Time.deltaTime;
        }
        if(shildDesarmer >=5)
        {
            anim.SetBool("Shild", false);
            mv.speed = mv.speedPadrão;
            shild = false;
            shildDesarmer = 0;
        }
    }
    private void AtaqueMouse()
    {
        
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if(dash)
            {
                anim.SetBool("Shild", false);
                mv.speed = mv.speedPadrão;
                shild = false;
            }
            else
            {
                anim.SetBool("Shild", true);
                mv.speed = 2;
                shild = true;
            }
        }
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            anim.SetBool("Shild", false);
            mv.speed = mv.speedPadrão;
            shildDesarmer = 0;
            shild = false;
        }
        if(shild == false)
        {
            if (Input.GetKeyUp(KeyCode.Mouse0) && !lança2)
            {

                if (mv.estamina > GastoStamina)
                {

                    AtaqueMouse1();
                    StartCoroutine(Cowl2());
                    lança2 = true;
                }
            }
            else if (lança2 && Input.GetKeyUp(KeyCode.Mouse0))
            {
                if (mv.estamina > GastoStamina)
                {
                    numero++;
                    if (numero < 2)
                    {
                        AtaqueMouse1();
                    }
                }
            }
        }
    
       
    }
    
    private void AtaqueMouse1()
    {
        lança2 = true;
        mv.estamina -= GastoStamina;
    }
    private IEnumerator Cowl2()
    {
        if (numero <= 0)
        {
            anim.Play("Patack", -1);

        }
        yield return new WaitForSeconds(TempoAnim1);
        if (numero > 0)
        {
            anim.Play("Patack2", -1);
        }

        yield return new WaitForSeconds(cdAtk);
        rb.gravityScale = 10f;
        lança2 = false;
        numero = 0;
        StopCoroutine(Cowl2());
    }
}
