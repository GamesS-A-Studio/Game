using System.Collections;
using UnityEngine;
using FirstGearGames.SmoothCameraShaker;

public class ColissionParticle : MonoBehaviour
{
    public GameObject particla;
    public Transform pontoSpawn;
    public bool a;
    public LayerMask layer;
    public float tempo;
    IEnumerator num;
    public ShakeData hitShake;
    // Start is called before the first frame update
    void Start()
    {
        StopAllCoroutines();
        num = null;
    }
    public void FixedUpdate()
    {
        a = Physics2D.OverlapCircle(pontoSpawn.position,0.5f, layer);
        if(a)
        {
            if (num == null)
            {
                StartCoroutine(ativa());
                num = ativa();
            }
        }
    }
    // Update is called once per frame

    IEnumerator ativa()
    {
        GameObject ob = Instantiate(particla, pontoSpawn.position, Quaternion.identity);
        CameraShakerHandler.Shake(hitShake);
        yield return new WaitForSeconds(tempo);
        num = null;
    }
}
