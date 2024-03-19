using UnityEngine;
using UnityEngine.UI;
using FirstGearGames.SmoothCameraShaker;
using System.Collections;

public class Life : MonoBehaviour
{
    public bool boos;
    public bool enemy;
    public Vector3 loc;
    public GameObject obpai;
    public ShakeData hitShake;
    public Image lifeBar;
    public Image lifeBarcinza;
    public int lifeMax;
    public float lifeAtual;
    float lifeAtualcinza;
    public MoveEnemy mv;
    public GameObject particleMorte;
    GameManager gm;
    IEnumerator mm;
    public Rigidbody2D rb;
    public float forcaKnook;
    bool knok;
    public VisionEnemy v;
    public destativa des;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.gmInstance;
    }

    // Update is called once per frame
    void Update()
    {
        if(lifeBar != null)
        {
            lifeBar.fillAmount = lifeAtual / lifeMax;
            lifeBarcinza.fillAmount = lifeAtualcinza / lifeMax;
        }
        if (lifeAtual > lifeMax)
        {
            lifeAtual = lifeMax;
        }
       
        if (lifeAtualcinza > lifeAtual)
        {
            lifeAtualcinza -= 15 * Time.deltaTime;

        }
        else
        {
            lifeAtualcinza = lifeAtual;
        }
        if (enemy && lifeAtual <= 0 && !boos)
        {
            if (mv != null)
            {
                mv.stopAll = true;
            }
            if (mm == null)
            {
                StartCoroutine(morte());
                mm = morte();
            }
        }
    
    }
    IEnumerator morte()
    {
        yield return new WaitForSeconds(1f);
        int c = Random.Range(0, 100);
        if (c < 50)
        {
            int valor = Random.Range(1, 3);
            gm.addRec("Carne", valor);
        }
        if (c >= 50 )
        {
            int valor = Random.Range(2, 12);
            gm.addRec ("Dinheiro", valor);
        }
        gm.vida.appheal(Random.Range(5,10));
        GameObject ob = Instantiate(particleMorte, transform.position + loc, Quaternion.identity);
        yield return new WaitForSeconds(0.2f);
        des.começa();
    }
  
    public void appDamag(float valor)
    {
        lifeAtual -= valor;
        CameraShakerHandler.Shake(hitShake);
    }
    public void appheal(float valor)
    {
        lifeAtual += valor;
    }
}
