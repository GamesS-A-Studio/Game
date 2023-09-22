using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PosiçãoPlayer : MonoBehaviour
{
    public string currentLevel;
    public Vector2 posição;
    public GameObject playerposição;
    public Animator animCancas;

    public Vector2 posiçãoChekPoint;
    void Start()
    {    
        currentLevel = SceneManager.GetActiveScene().name;
        DontDestroyOnLoad(transform.gameObject);
        StartCoroutine(Transição());
    }
    void Update()
    {  
        if (!currentLevel.Equals(SceneManager.GetActiveScene().name))
        {
            posição = GameObject.Find("PosInicia").transform.position;
            currentLevel = SceneManager.GetActiveScene().name;
            playerposição.transform.position = posição;
            animCancas.SetBool("Troca", false);
           
            StartCoroutine(Transição());
        }
 
    }
 
    IEnumerator Transição()
    {
       // animCancas.SetBool("Abre", true);
        yield return new WaitForSeconds(2f);
      //  animCancas.SetBool("Abre", false);
        StopCoroutine(Transição());
    }

  
 

}
