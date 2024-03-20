
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering;
using System.Collections;
using UnityEngine.SceneManagement;



public class GameManager : MonoBehaviour
{
    public EstadoJogo estadosGame;
    public bool Boss1;
    public bool terminouHistoria;
    [Header("___________Estados________")]
    public static GameManager gmInstance;
    public VisibilidadePlayer visibilidadePlayer;
    public DirecaoPlayer direcaoPlayer;
    public Animator animatorui;
    public Image tutorialIm;
    public Sprite[] im;
    public string[] dicas;
    public GameObject menuHabiliti;
    public bool open;
    public TextMeshProUGUI textdicas;
    int  imindex;

    [Header("_________RefPlayer_________")]
    public RaioCristal raio;
    public TextMeshProUGUI textosCanvas;
    public checkPoint cc = new checkPoint();
    public soungManager song;
    IEnumerator Texto;
    public GameObject obAura;
    public GameObject arma1;
    public GameObject arma2;
    public GameObject colider;
    public Animator Recanim;
    public Image iconSthealt;
    public Mira mir;
    public Volume global;
    public Habilities hh;
    public MovimentPlayer movimentPlayer;
    public Life vida;
    public Image barraVida;
    public Image barraEstamina;
    public Image iconHabilidade;
    public Image barraCDHabilidade;
    public Camera cam;
    public TextMeshProUGUI textoCarne;
    public TextMeshProUGUI textoGrana;
    public GameObject miraIcon;
    [Header("___________Variaveis________")]
    public int carne;
    public int dinheiro;
    [Header("_________Menu_________")]
    public GameObject logo;
    public GameObject menuGameOver;
    public float tempoComeo;
    IEnumerator num;
    IEnumerator num2;
    public infos inf = new infos();
    bool menu;
    // Start is called before the first frame update
    void Awake()
    {
        if(gmInstance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }
        gmInstance = this;
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.visible = (estadosGame == EstadoJogo.jogo)? false:true;
        if (movimentPlayer.horizontal>0)
        {
            direcaoPlayer = DirecaoPlayer.direita;
        }else if (movimentPlayer.horizontal < 0) { direcaoPlayer = DirecaoPlayer.esquerda; }
        if(vida.lifeAtual <=0)
        {
            if(num2 == null)
            {
                StartCoroutine(gmeov());
                num2 = gmeov();
            }
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            open = !open;
            menuHabiliti.SetActive(open);
            estadosGame = (open) ? EstadoJogo.pause : EstadoJogo.jogo;
        }
        if(!menu)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                StartGame();
             
            }
        }
    }
    public void cena()
    {
        SceneManager.LoadScene("Cena1");
    }
    public void abreMenu()
    {
        open = !open;
        menuHabiliti.SetActive(open);
        estadosGame = (open) ? EstadoJogo.pause : EstadoJogo.jogo;
    }
    public void nextIm()
    {
        imindex++;
        if (imindex >= im.Length)
        {
            imindex = 0;
        }
        textdicas.text = dicas[imindex];
        tutorialIm.sprite = im[imindex];
    }
    public void returntIm()
    {
        imindex--;
        if (imindex <= 0)
        {
            imindex = im.Length-1;
        }
        tutorialIm.sprite = im[imindex];
    }
    IEnumerator gmeov()
    {
        menuGameOver.SetActive(true);
        yield return new WaitForSeconds(1f);
        Time.timeScale = 0;
    }
    public void invocaTexto(float duracao, string texto, traponderSc tr)
    {
        if (Texto == null)
        {
            StartCoroutine(textosTela(duracao, texto, tr));
            Texto = textosTela(duracao, texto, tr);
        }
    }
    public void addRec(string animName, int quant)
    {
        switch (animName)
        {
            case "Carne":
                if (carne <= 9999)
                {
                    carne += quant;
                }
                break;
            case "Dinheiro":
                if (dinheiro <= 9999)
                {
                    dinheiro += quant;
                }
                break;

        }
        textoCarne.text = carne.ToString();
        textoGrana.text = dinheiro.ToString();
        Recanim.Play(animName, 0);
    }
    IEnumerator textosTela(float tempo, string texto, traponderSc tr)
    {
        textosCanvas.text = texto.ToString();
        yield return new WaitForSeconds(0.2f);
        textosCanvas.gameObject.SetActive(true);
        yield return new WaitForSeconds(tempo);
        textosCanvas.gameObject.SetActive(false);
        tr.index++;
        Texto = null;
    }
    public void Save()
    {
        inf.grana = dinheiro;
        inf.carne = carne;
        inf.position = movimentPlayer.transform.position;
        inf.vidaatual = vida.lifeAtual;
        inf.boosmorto = Boss1;
    }
    public void Load()
    {
        Reset();
        raio.bb.cancelaAnimacoes();
        raio.bb.Retorno();
        dinheiro = inf.grana;
        carne= inf.carne;
        movimentPlayer.transform.position=inf.position;
        vida.lifeAtual = inf.vidaatual;
        Boss1 = inf.boosmorto;
        Time.timeScale = 1.0f;
        menuGameOver.SetActive(false);
        song.gameOVER = false;
        num2 = null;
    }
    public void Reset()
    {
        raio.Rest();
        raio.bb.Retorno();

    }
    public void StartGame()
    {
        if(num == null)
        {
            StartCoroutine(Starta());
            num = Starta();
            menu = true;
            num2 = null;
        }
    }
    IEnumerator Starta()
    {
        yield return new WaitForSeconds(tempoComeo);
        logo.SetActive(false);
        yield return new WaitForSeconds(0.4f);
        movimentPlayer.enabled = true;
        mir.enabled = true;
        num = null;
    }
 
}
public enum EstadoJogo
{
    menu,
    pause,
    jogo
}
public class infos
{
    public Vector2 position;
    public int grana;
    public int carne;
    public float vidaatual;
    public bool boosmorto;
    public bool habilidade1;
}
public class checkPoint
{
    public Vector2 posio;
    public bool boos1;
    public float vidaatual;
}
public enum VisibilidadePlayer
{
    Visivel,
    NaoVisivel
}
public enum DirecaoPlayer
{
    direita,
    esquerda
}