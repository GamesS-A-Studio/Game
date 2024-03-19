
using UnityEngine;

public class PlatformControler : MonoBehaviour
{
    public PlatformEffector2D plat;
    bool vai;
    float tempo;
    public float tempoAtravessar;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (vai)
        {
            if (Input.GetKey(KeyCode.S))
            {
                plat.surfaceArc = 0;
                rb.velocity += Vector2.down * 10f * Time.deltaTime;
            }
            else { plat.surfaceArc = 215; }
        }
        else { plat.surfaceArc = 215; }
       
    }
    private void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {
            vai = true;
            rb = coll.GetComponent<Rigidbody2D>();
        }
    }
    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {
            vai = false;
        }
    }
   
}
