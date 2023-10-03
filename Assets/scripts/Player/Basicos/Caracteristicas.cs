using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Caracteristicas : MonoBehaviour
{
    public static Caracteristicas instance;
    public Life vit;
    public Move2 mov;
    public Image BarraXP;
    public Image barramana;
    public Text textogold;
    public TextMeshProUGUI lvl;
    public TextMeshProUGUI[] Stts;
  
    
    public int dinheiro;

    public int Dano;
    public float moveSpeed;
    public float reducesColdown;
    public float armor;
    public int vida;
    public float critico;
    public float mana;
    public float jumpForce;

    public int baseDano;
    public float basemoveSpeed;
    public float basereducesColdown;
    public float basearmor;
    public int basevida;
    public float basecritico;
    public float basemana;
    public float basejumpForce;

    public float manaMax = 100;

    int lvlAtual;
    public float xpAtual;
    public float xpGanho;
    public float xpMax = 100;
    public GameObject Upou;


    // Start is called before the first frame update
    void Awake()
    {
 
         lvl.text = "LVL:" + lvlAtual;
         instance = this;
         
    }

    private void FixedUpdate()
    {

         textogold.text = dinheiro.ToString();
         barramana.fillAmount = mana / manaMax;
         UpdadePlayer();
         if(mana <manaMax)
            {
                mana += 1;
            }
         if(mana <0)
            {
                mana = 0;
            }
         if(mana>manaMax)
            {
                mana = manaMax;
            }
        
        Status();
    }
    public void UpdadePlayer()
    {
        if(xpAtual>=xpMax)
        {
            Dano += 10;
            vida += 30;
            critico += 0.3f;
            armor += 5f;
            moveSpeed += 0.1f;
            reducesColdown += 0.1f;
            mana += 50;
            lvlAtual++;
            lvl.text = "LVL:" + lvlAtual;
            GameObject ob = Instantiate(Upou, transform.position, Quaternion.identity);
            xpMax += 100;
            xpAtual = 0;
        }

    }
    public void XPupgrade(float xp)
    {
        xpGanho += xp;
        while(xpGanho>0)
        {
            BarraXP.fillAmount = xpAtual / xpMax;
            float xpToAdd = 50 * Time.deltaTime;
            xpAtual += xpToAdd;
            xpGanho -= xpToAdd;
            UpdadePlayer();
        }
      
    }
    public void Status()
    {
        Stts[0].text = "VitMax:" + vit.vidamax;
        Stts[1].text = "ChanCrit:" + critico.ToString();
        Stts[2].text = "Damage:" + Dano.ToString();
        Stts[3].text = "ManaMax:" + manaMax.ToString();
        Stts[4].text = "MoveSpeed:" + moveSpeed.ToString();
        Stts[5].text = "ReducesCowldown:" + reducesColdown.ToString();
        Stts[6].text = "ArmorMax:" + armor.ToString();

    }
}
