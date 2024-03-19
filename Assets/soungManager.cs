using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soungManager : MonoBehaviour
{
    public AudioClip musicaBoos;
    public AudioClip musicaCave;
    public AudioClip musicaPosBatleBoos;
    public AudioClip musicaMenu;
    public AudioClip musicaGameOver;

    public AudioSource musicaAmbiente;
    public AudioSource musicacombat;

    public bool boosFigth;
    public float velocity;
    public bool gameOVER;
    public bool menu;
    public bool começou;
    GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.gmInstance;
    }

    // Update is called once per frame
    void Update()
    {
        if(!começou)
        {
            musicaAmbiente.Stop();
        }
        else
        {
            if (menu)
            {
                musicaAmbiente.clip = musicaMenu;

            }
            else
            {
                if (gameOVER)
                {
                    musicaAmbiente.clip = musicaGameOver;

                }
                else
                {
                    if (boosFigth)
                    {
                        if (musicaAmbiente.volume > 0)
                        {
                            musicaAmbiente.volume -= velocity * Time.deltaTime;
                        }
                        if(musicacombat.volume <= 0.1f)
                        {
                            musicacombat.volume += velocity * Time.deltaTime;
                        }
                    }
                    else
                    {
                        if(!gm.Boss1)
                        {
                            musicaAmbiente.clip = musicaCave;
                        }
                        else
                        {
                            musicaAmbiente.clip = musicaPosBatleBoos;
                        }
                       
                        if (musicacombat.volume > 0)
                        {
                            musicacombat.volume -= velocity * Time.deltaTime;
                        }
                        if (musicaAmbiente.volume <= 0.1f)
                        {
                            musicaAmbiente.volume += velocity * Time.deltaTime;
                        }
                    }
                }
            }
            if (musicaAmbiente.clip != null && !musicaAmbiente.isPlaying)
            {
                musicaAmbiente.Play();
            }
        }
    }
    IEnumerator ComeçouBossFigth()
    {
        yield return null;
    }
    IEnumerator terminouBossFigth()
    {
        yield return null;
    }
}
