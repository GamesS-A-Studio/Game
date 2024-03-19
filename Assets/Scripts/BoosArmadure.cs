
using UnityEngine;
using FirstGearGames.SmoothCameraShaker;
using System.Collections;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;


public class BoosArmadure : MonoBehaviour
{
    GameManager gm;
    public enum Direcao
    {
        Direita,
        Esquerda
    }
    public Direcao orientacao;
    public enum acoes
    {
        nada,
        none,
        Stun,
        Atk1,
        Atk2,
        Atk3,
        Atk4,
        morte
       
    }
    public bool a1;
    public bool a2;
    public bool a3;
    public bool a4;
    public bool a5;
    public bool a6;
    public acoes Ataques;
    [Header("________________Components___________")]
    [Header("____________________________________________________________")]
    public Animator anim;
    public Vector2 pontoInicial;
    public float pausa;

    [Header("________________Atack1___________")]
    [Header("____________________________________________________________")]
    public float Distancia;
    public float speed;
    IEnumerator Atk1Enum;
    [Header("________________Atack2___________")]
    [Header("____________________________________________________________")]
    public float duracaoATK2;
    public GameObject Particle2;
    public GameObject[] add;
    public Transform spawn2;
    public GameObject[] invocaçoes;
    IEnumerator Atk2Enum;
    [Header("________________Atack3___________")]
    [Header("____________________________________________________________")]
    public float duracaoatk3;
    IEnumerator Atk3Enum;
    public APPdamageTocar pp;
    public ShakeData hitShake;
    [Header("________________Atack4___________")]
    [Header("____________________________________________________________")]
    public Vignette vignette;
    public Volume globalVolume;
    IEnumerator Atk4Enum;
    public float duracaoatk4;
    public GameObject[] olhos;
    public ShakeData hitshaker;
    public GameObject particle4;

    [Header("________________Descanso___________")]
    [Header("____________________________________________________________")]
    public float pausouEntreATK;
    public float TempoStun;
    IEnumerator stun;
    public Animator animCristal;
    public int n;
    IEnumerator pausou;
    [Header("________________Morte___________")]
    [Header("____________________________________________________________")]
    public Life vida;
    IEnumerator morte;
    public bool resetando;
    private void Start()
    {
        if (globalVolume.profile.TryGet(out vignette))
        { 
            //
        }
        gm = GameManager.gmInstance;
        pontoInicial = transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        a1 = (Atk1Enum == null) ? true : false; 
        a2 = (Atk2Enum == null) ? true : false; 
        a3 = (Atk3Enum == null) ? true : false; 
        a4 = (stun == null) ? true : false; 
        a5 = (pausou == null) ? true : false; 
        a6 = (morte == null) ? true : false; 
        transform.rotation = (orientacao == Direcao.Direita) ? Quaternion.Euler(0,0,0): Quaternion.Euler(0,180,0);
        if(!atacando() && !resetando && Ataques != acoes.Stun)
        {
            if (Atk1Enum == null && Ataques == acoes.Atk1)
            {
                StartCoroutine(A1());
                Atk1Enum = A1();
            }
            if (Atk2Enum == null && Ataques == acoes.Atk2)
            {
                StartCoroutine(A2());
                Atk2Enum = A2();
            }
            if (Atk3Enum == null && Ataques == acoes.Atk3)
            {
                StartCoroutine(A3());
                Atk3Enum = A3();
            }
            if (Atk4Enum == null && Ataques == acoes.Atk4)
            {
                StartCoroutine(A4());
                Atk4Enum = A4();
            }
           
      
        }
        if (stun == null && Ataques == acoes.Stun && !resetando)
        {
            StopAllCoroutines();
            StartCoroutine(Stunado());
            stun = Stunado();
        }
        if (Ataques == acoes.none && pausou == null && !resetando)
        {
            
            StartCoroutine(Pausa());
            pausou = Pausa();
        }

        if (vida.lifeAtual <= 0)
        { 
            Ataques = acoes.morte;
            if (morte == null && Ataques == acoes.morte)
            {
                StopAllCoroutines();
                StartCoroutine(morreu());
                morte = morreu();
            }
        }
        Distancia = (orientacao == Direcao.Direita) ? 12 : -12;

    }
    public  void escolhaAtk()
    {

        n++;
        switch(n)
        {
            case 1:
                Ataques = acoes.Atk1;
                break;
            case 2:
                Ataques = acoes.Atk3;
                break;
            case 3:
                Ataques = acoes.Atk2;
                break;
            case 4:
                Ataques = acoes.Atk1;
                break;
            case 5:
                Ataques = acoes.Atk2;
                break;
            case 6:
                Ataques = acoes.Atk4;
                break;
            case 7:
                Ataques = acoes.Atk2;
                break;
            case 8:
                Ataques = acoes.Atk3;
                break;
            case 9:
                Ataques = acoes.Atk1;
                break;
            case 10:
                Ataques = acoes.Atk2;
                break;
            case 11:
                Ataques = acoes.Atk4;
                break;
            case 12:
                Ataques = acoes.Atk3;
                break;
            case 13:
                Ataques = acoes.Atk3;
                break;
            case 14:
                Ataques = acoes.Atk3;
                break;
            case 15:
                Ataques = acoes.Atk4;
                break;
            default:
                n = 0;
                break;

        }
        pausou = null;
    }
    public GameObject Spawn(GameObject objeto, Transform localizacao)
    {
        return Instantiate(objeto, localizacao.position, localizacao.rotation);
    }
    public bool atacando()
    {
        return Atk1Enum != null|| Atk2Enum != null || Atk3Enum != null || Atk4Enum != null|| morte != null || stun != null || pausou != null;
    }
    public void cancelaAnimacoes()
    {
     
        anim.SetBool("Stun", false);
        anim.SetBool("Atack5", false);
        anim.SetBool("Atack4", false);
        anim.SetBool("Atack3", false);
        anim.SetBool("Atack2", false);
        anim.SetBool("Atack1", false);
       
    }
    public void StartAnim(string nome, bool acao)
    {
        anim.SetBool(nome, acao);
    }
    public void Retorno()
    {
        StopAllCoroutines();
        StartCoroutine(resetaFigth());
    }
    IEnumerator resetaFigth()
    {
        resetando = true;
        Atk4Enum = null;
        Atk3Enum = null;
        Atk2Enum = null;
        Atk1Enum = null;
        stun = null;
        vida.lifeAtual = vida.lifeMax;
        cancelaAnimacoes();
        transform.position = pontoInicial;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        orientacao = Direcao.Direita;
        anim.Play("antes", 0);
        foreach(GameObject ob in invocaçoes)
        {
            if(ob != null)
            {
                Destroy(ob.gameObject);
                
            }
        }
        yield return new WaitForSeconds(1f);
        pausou = null;
        Ataques = acoes.none;
        this.GetComponent<BoosArmadure>().enabled = false;
      
    }
    IEnumerator A1()
    {
        int v = UnityEngine.Random.Range(1, 4);
        for(int x = 0; x< v; x++)
        {
            StartAnim("Atack1", true);
            yield return new WaitForSeconds(0.5f);
            if (anim.GetBool("Atack1") == true)
            {
                float c = transform.position.x + Distancia;
                while (transform.position.x != c)
                {
                    if(resetando)
                    {
                        break;
                    }
                    transform.position = Vector2.MoveTowards(transform.position, new Vector2(c, transform.position.y), speed * Time.deltaTime);
                    yield return null;
                }
                if (resetando)
                {
                    break;
                }
                StartAnim("Atack1", false);
                yield return new WaitForSeconds(0.2f);
                orientacao = (orientacao == Direcao.Direita) ? Direcao.Esquerda : Direcao.Direita;
            }
        }
        Ataques = acoes.none; 
    }
    IEnumerator A2()
    {
       
        int v = UnityEngine.Random.Range(1, 3);
        for(int x =0; x < v; x++)
        {
            if (resetando)
            {
                break;
            }
            StartAnim("Atack2", true);
            yield return new WaitForSeconds(1.8f);
            int b = UnityEngine.Random.Range(0,3);
            Spawn(Particle2, spawn2);
            yield return new WaitForSeconds(0.2f);
            GameObject novoObjetoAdd = Spawn(add[b], spawn2);
            invocaçoes[x] = novoObjetoAdd;
            StartAnim("Atack2", false);
            yield return new WaitForSeconds(1.5f);
        }
        yield return new WaitForSeconds(duracaoATK2);
        Ataques = acoes.none;
    }
    IEnumerator A3()
    {
        pp.appDamage = false;
        StartAnim("Atack3", true);
        CameraShakerHandler.Shake(hitShake);
        yield return new WaitForSeconds(duracaoatk3);
        StartAnim("Atack3", false);
        yield return new WaitForSeconds(duracaoatk3);

        Ataques = acoes.none;
    }
    IEnumerator A4()
    {
        float original = vignette.intensity.value;
        StartAnim("Atack4", true);
        while (vignette.intensity.value < 0.4f)
        {
            vignette.intensity.value += 0.1f * Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(3f);
        int v = Random.Range(3, 7);
        int p = 0;
        for(int x = 0; x < v; x++)
        {
            int b = Random.Range(0, 6);
            while (p == b)
            {
                b = Random.Range(0, 6);
                yield return null;
            }
            Spawn(particle4, olhos[b].transform);
            yield return new WaitForSeconds(0.3f);
            olhos[b].SetActive(true);
            CameraShakerHandler.Shake(hitshaker);
            yield return new WaitForSeconds(3.5f);
            olhos[b].SetActive(false);
            p = b;

        }
        olhos[6].SetActive(true);
        CameraShakerHandler.Shake(hitshaker);
        yield return new WaitForSeconds(4f);
        olhos[6].SetActive(false);
        yield return new WaitForSeconds(2f);
        CameraShakerHandler.Shake(hitshaker);
        olhos[0].SetActive(true);
        yield return new WaitForSeconds(2f);
        CameraShakerHandler.Shake(hitshaker);
        olhos[1].SetActive(true);
        olhos[0].SetActive(false);
        yield return new WaitForSeconds(2f);
        CameraShakerHandler.Shake(hitshaker);
        olhos[2].SetActive(true);
        olhos[1].SetActive(false);
        yield return new WaitForSeconds(2f);
        CameraShakerHandler.Shake(hitshaker);
        olhos[3].SetActive(true);
        olhos[2].SetActive(false);
        yield return new WaitForSeconds(2f);
        CameraShakerHandler.Shake(hitshaker);
        olhos[4].SetActive(true);
        olhos[3].SetActive(false);
        yield return new WaitForSeconds(2f);
        CameraShakerHandler.Shake(hitshaker);
        olhos[5].SetActive(true);
        olhos[4].SetActive(false);
        yield return new WaitForSeconds(2f);
        olhos[5].SetActive(false);
        StartAnim("Atack4", false);
        yield return new WaitForSeconds(0.5f);
        while (vignette.intensity.value > original)
        {
            vignette.intensity.value -= 0.1f * Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(duracaoatk4);
        Ataques = acoes.none;
    }
    IEnumerator morreu()
    {
        anim.SetBool("Stun", false);
        anim.SetBool("Atack5", false);
        anim.SetBool("Atack4", false);
        anim.SetBool("Atack3", false);
        anim.SetBool("Atack2", false);
        anim.SetBool("Atack1", false);
        Ataques = acoes.none;
        gm.Boss1 = true;
        yield return new WaitForSeconds(1f);
        gm.hh.transponderLiberada = true;
        gm.hh.Transpoder = true;
        anim.Play("Morte",0);

    }
    IEnumerator Stunado()
    {
        cancelaAnimacoes();
        Atk4Enum = null;
        Atk3Enum = null;
        Atk2Enum = null;
        Atk1Enum = null;
        pausou = null;
        yield return new WaitForSeconds(0.5f);
        StartAnim("Stun", true);
        yield return new WaitForSeconds(TempoStun);
        animCristal.SetBool("caiu", false);
        yield return new WaitForSeconds(2f);
        StartAnim("Stun", false);
        yield return new WaitForSeconds(1.5f);
        orientacao = (pontoInicial.x != transform.position.x) ? Direcao.Esquerda : Direcao.Direita;
        StartAnim("Atack1", true);
        while (Mathf.Abs(transform.position.x - pontoInicial.x) > 0.01f)
        {
            transform.position = Vector2.MoveTowards(transform.position, pontoInicial, speed * Time.deltaTime);
            yield return null;
        }
        orientacao = Direcao.Direita;
        StartAnim("Atack1", false);
        Ataques = acoes.none;
    }
    IEnumerator Pausa()
    { 
        cancelaAnimacoes();
        Atk4Enum = null;
        Atk3Enum = null;
        Atk2Enum = null;
        Atk1Enum = null;
        stun = null;
        yield return new WaitForSeconds(pausa);
        escolhaAtk();
    }
}
