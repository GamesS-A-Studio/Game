using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SistemSuborno : MonoBehaviour
{
    [Header("___________Componentes________")]
    public TextMeshProUGUI textoValor;
    public Sprite[] estado;
    public Image estadoIm;
    public VisionEnemy vv;
    public MoveEnemy moveEnemy;
    GameManager gm;
    [Header("___________Variaveis________")] 
    public bool Carne;
    public bool Dinheiro;
    public bool Sonifero;
    public string menssagem;
    int x;
    int quantidade;
    public enum Estado
    {
        None,
        Passivo
    }
    
    public Estado estate;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.gmInstance;
        estadoIm.gameObject.SetActive(true);
        x = Random.Range(0, 100);
        if(Carne)
        {
            estadoIm.sprite = estado[0];
            quantidade = Random.Range(1,5);
        }
        if (Dinheiro)
        {
            quantidade = Random.Range(1, 100);
            estadoIm.sprite = estado[1];
        }
        if (Sonifero)
        {
            quantidade = Random.Range(1, 3);
            estadoIm.sprite = estado[2];
        }
        textoValor.text = quantidade.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(vv.coll != null)
        {

            if (vv.coll.gameObject.tag == "Player")
            {
               
                
            }
        }
    
    }
}
