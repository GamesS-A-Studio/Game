
using System.Collections;
using FirstGearGames.SmoothCameraShaker;
using UnityEngine;
using TMPro;

public class fim : MonoBehaviour
{
    public TextMeshProUGUI text;
    public string[] menssagemtexto;
    public GameObject imagemFim;
    public Animator animPlayer;
    public Animator animCena;
    public float TempoAnimação;
    bool startou;
    IEnumerator eu;
    public ShakeData hitShake;
    public ShakeData hitShake2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(startou)
        {
            if(eu == null)
            {
                StartCoroutine(finalizou());
                eu = finalizou();
            }
        }
    }
    IEnumerator finalizou()
    {
        animPlayer.Play("fimDoGame",0);
        text.gameObject.SetActive(true);
        text.text = menssagemtexto[0];
        yield return new WaitForSeconds(6f);
        animCena.Play("BoosSurge", 0);
        yield return new WaitForSeconds(1.5f);
        CameraShakerHandler.Shake(hitShake2);
        yield return new WaitForSeconds(1.5f);
        CameraShakerHandler.Shake(hitShake2);
        yield return new WaitForSeconds(1.5f);
        CameraShakerHandler.Shake(hitShake2);
        yield return new WaitForSeconds(1f);
        CameraShakerHandler.Shake(hitShake);
        text.text = menssagemtexto[1];
        yield return new WaitForSeconds(TempoAnimação);
        text.gameObject.SetActive(false);
        imagemFim.SetActive(true);
        Time.timeScale = 0;
    }
    public void OnTriggerEnter2D(Collider2D coll)
    {
        MovimentPlayer mv = coll.GetComponent<MovimentPlayer>();
        if(coll.CompareTag( "Player"))
        {
            if(mv != null)
            {
                mv.enabled = false;
            }
            startou = true; 
        }
    }
}
