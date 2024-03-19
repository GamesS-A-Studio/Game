using FirstGearGames.SmoothCameraShaker;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Habilities : MonoBehaviour
{
    GameManager gm;
    public Image imageHabiliti;
    public Image imageHabilitiCD;
    public Sprite[] iconHabi;
    float CDcontador;

    public Animator anim;
    [Header("___________Liberadas______________")]
   
    public bool Transpoder;


    public GameObject descrição;

   
    [Header("_____________SmokeBomb_____________")]
    [Header("_____________SmokeBomb_____________")]
    public string descriçãoesq;
   
    public bool transponderLiberada;


    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.gmInstance;
    }

    // Update is called once per frame
    void Update()
    {
      
        
        if(CDcontador>0)
        {
            CDcontador -= Time.deltaTime;
        }
        //Esquiva
        if(Transpoder)
        {
            imageHabiliti.sprite = iconHabi[0];
            imageHabiliti.color = Color.white;        
        }
        if(gm.estadosGame == EstadoJogo.jogo)
        {
            descrição.SetActive(false);
        }
      

    }
    public void sobre(string desc)
    {

        descrição.GetComponent<TextMeshProUGUI>().text = desc;
        descrição.SetActive(true);
    }
    public void saiu()
    {

        descrição.SetActive(false);
    }
    public void selectEsquiva()
    {
        if(transponderLiberada)
        {
            Transpoder = true;
        }
   
    }
}
