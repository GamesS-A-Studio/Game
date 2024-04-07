using System.Collections;
using UnityEngine;

public class Mira : MonoBehaviour
{
    GameManager gm;
    [Header("Ataque Melle")]
    [Header("_______________________________________")]
    public GameObject prefabAtack1;
    public GameObject prefabAtack2;
    public GameObject prefabAtack3;
    public float AtackMelleCD;
    public float AtackMelleTempo;
    public Animator anim;
    public float TempoAntesSpawn;
    public int quantidadeDeAtaques;
    public GameObject arma1;
    public GameObject arma2;

    public bool AtaquePronto;
    bool atacklivre;
    Coroutine attackCoroutine; // Reference to the attack coroutine.
    
    [Header("_______________________________________")]
    [Header("Mira")]
    [Header("_______________________________________")]
    public GameObject aro;
    public Transform target;

    public string enemyTag = "Inimigo";

    public Transform arma;
    public Transform Player;
    public Camera cam;
    public Rigidbody2D rb;

    void Start()
    {
        AtaquePronto = true;
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
            Aim();
            if (AtaquePronto)
            {
                if (AtackMelleCD < 0)
                {
                    AtackMelleCD = 0;
                }
                if (Input.GetKeyDown(KeyCode.X))
                {
                    if (AtackMelleTempo <= 0)
                    {
                        // If an attack coroutine is already running, stop it to allow for a new attack sequence.
                        if (attackCoroutine != null)
                        {
                            StopCoroutine(attackCoroutine);
                        }
                        // Start a new attack coroutine.
                        attackCoroutine = StartCoroutine(AttackSequence());
                    }
                }
                if (AtackMelleTempo > 0)
                {
                    AtackMelleTempo -= Time.deltaTime;
                }
            }
            arma1.SetActive(AtaquePronto);
            arma2.SetActive(AtaquePronto);
        }
    }

    IEnumerator AttackSequence()
    {
        for (int x = 0; x < quantidadeDeAtaques; x++)
        {
            yield return new WaitForSeconds(TempoAntesSpawn);
            // Determine which attack animation to play.
            int o = Random.Range(0, 100);
            if (o <= 30)
            {
                anim.Play("ATKPlayer3", 0);
                Instantiate(prefabAtack1, arma.transform.position, arma.transform.rotation);
            }
            else if (o > 30 && o <= 60)
            {
                anim.Play("ATKPlayer1", 0);
                Instantiate(prefabAtack2, arma.transform.position, arma.transform.rotation);
            }
            else
            {
                anim.Play("ATKPlayer2", 0);
                Instantiate(prefabAtack3, arma.transform.position, arma.transform.rotation);
            }
            yield return new WaitForSeconds(0.3f);
        }
        yield return new WaitForSeconds(0.1f);
        AtackMelleTempo = AtackMelleCD;
        atacklivre = false;
        attackCoroutine = null; // Reset the attack coroutine reference.
    }

    void Aim()
    {
        Vector3 mouse = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));
        Vector3 directionToMouse = mouse - arma.transform.position;
        gm.miraIcon.transform.position = mouse;
        float angle = Mathf.Atan2(directionToMouse.y, directionToMouse.x) * Mathf.Rad2Deg;
        arma.transform.rotation = Quaternion.Euler(0f, 0f, angle);
        if(gm.movimentPlayer.horizontal == 0)
        {
            if (cam.WorldToScreenPoint(Player.transform.position).x > Input.mousePosition.x)
            {
                Player.transform.rotation = Quaternion.Euler(0, 180, 0);
                gm.direcaoPlayer = DirecaoPlayer.esquerda;
            }
            else
            {
                Player.transform.rotation = Quaternion.Euler(0, 0, 0);
                gm.direcaoPlayer = DirecaoPlayer.direita;
            }
        }
    }
}
