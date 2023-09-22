using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicial : MonoBehaviour
{
    public GameObject Player;
    public PosiçãoPlayer pos;
    public string cena;
    public Vector2 position;
    bool pode;
    public GameObject cam;
    // Start is called before the first frame update
    void Start()
    {
        pode = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void startgame()
    {
        if (pode)
        {
            StartCoroutine(StartJogo());
            pode = false;
        }
    }
    IEnumerator StartJogo()
    {
        cam.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        Player.SetActive(true);
        pos.posição = position;
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene(cena);
        StopCoroutine(StartJogo());
    }
}
