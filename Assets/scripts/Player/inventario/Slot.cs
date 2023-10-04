using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Slot : MonoBehaviour
{
    Referencias refinstance;
    public int meuId;
    public bool fuiEquipago;
    public bool desbloqueado;
    public Image minhaimage;
    bool porcima;
    public GameObject descrição;
    public Image imagemdesc;
    public TextMeshProUGUI descritext;
    public GameObject[] slotsMenosEu;
    public string meunomeItem;
    public string minhadescrição;
    public GameObject ponteiro;
    public GameObject meuPonteiro;
    private void Start()
    {
        refinstance = Referencias.refInstance;
    }
    void Update()
    {
        if(desbloqueado && fuiEquipago)
        {
            minhaimage.color = Color.white;
        }
        else if(desbloqueado)
        {
            minhaimage.color = new Color(0.130f, 0.130f, 0.130f);
        }else if (!desbloqueado) { minhaimage.color = Color.black; }
        if(porcima == true)
        {
            if(imagemdesc != null)
            {
                descritext.text = meunomeItem + ":" +  minhadescrição;
                imagemdesc.sprite = minhaimage.sprite;
            }

        }
    }
    public void OnMouseExit()
    {
        porcima = false;
        descrição.SetActive(false);
    }
    public void OnMouseEnter()
    {
        if (desbloqueado)
        {
            descrição.SetActive(true);
            porcima = true;
        }
        else
        {
            descrição.SetActive(false);
            porcima = false;
        }

    }
    public void OnMouseDown()
    {
        if(desbloqueado)
        {
            for (int x = 0; x < slotsMenosEu.Length; x++)
            {
                if (slotsMenosEu[x] != null)
                {
                    if (slotsMenosEu[x].GetComponent<Slot>().fuiEquipago)
                    {
                        slotsMenosEu[x].GetComponent<Slot>().fuiEquipago = false;
                        Destroy(slotsMenosEu[x].GetComponent<Slot>().meuPonteiro);
                    }
                }
                else
                {
                    break;
                }
            }
            if (meuPonteiro == null)
            {
                GameObject ob = Instantiate(ponteiro, transform.localPosition, Quaternion.identity);
                meuPonteiro = ob;
                RectTransform meuRectTransform = GetComponent<RectTransform>();
                ob.transform.SetParent(meuRectTransform);
                ob.transform.localPosition = Vector3.zero;
                ob.transform.Rotate(0, 0, 200f);
            }
            if(meuId ==0)
            {
                refinstance.look.enabled = true;
            }
            else
            {
                refinstance.look.cancela();
                refinstance.look.enabled = false;
            }
            fuiEquipago = true;
        }
    }

}