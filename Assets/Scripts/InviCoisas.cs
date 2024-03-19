
using UnityEngine;

public class InviCoisas : MonoBehaviour
{
    public SpriteRenderer sp;
    public Color cor;
    public Color corRetorno;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.CompareTag("Player"))
        {
            sp.color = cor;
 
        }
    }
    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {
            sp.color =corRetorno;
        }
    }
}
