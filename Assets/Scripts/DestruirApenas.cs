
using UnityEngine;

public class DestruirApenas : MonoBehaviour
{
    public float tempo;
 
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, tempo);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
