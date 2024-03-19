
using Unity.VisualScripting;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public int value;
    public bool moveJump;
    public bool hit;
    public bool sthealt;
    public bool combat;
    public bool completed;
    public GameObject[] imagensTuto;
    GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.gmInstance;
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.CompareTag("Player"))
        {
            if(completed)
            {
                return;
            }
            else
            {
                if(!moveJump && value == 0)
                {
                    gm.mir.AtaquePronto = false;
                    imagensTuto[value].SetActive(true);
                    moveJump = true;
                }
                if (!hit && value == 1)
                {
                    gm.mir.AtaquePronto = true;
                    imagensTuto[value].SetActive(true);
                    hit = true;
                }
                if (!sthealt && value == 2)
                {
                    imagensTuto[value].SetActive(true);
                    sthealt = true;
                }
                if (!combat && value == 3)
                {
                    imagensTuto[value].SetActive(true);
                    combat = true;
                }

            }
        }
    }
    public void OnTriggerExit2D(Collider2D coll)
    {
        if(coll.CompareTag("Player"))
        {
            if(completed)
            {
                return;
            }
            else
            {
                if (moveJump)
                {
                    imagensTuto[value].SetActive(false);
                    Destroy(gameObject);

                }
                if (!hit)
                {
                    imagensTuto[value].SetActive(false);
                    Destroy(gameObject);

                }
                if (!sthealt)
                {
                    imagensTuto[value].SetActive(false);
                    Destroy(gameObject);
                }
                if (!combat)
                {
                    imagensTuto[value].SetActive(false);
                    Destroy(gameObject);
                    completed = true;
                }
            }
        }
    }
}
