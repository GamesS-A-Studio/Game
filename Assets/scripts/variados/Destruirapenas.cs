using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destruirapenas : MonoBehaviour
{
    public float tempo;
    
    // Start is called before the first frame update
    void Start()
    {
        
            Destroy(gameObject, tempo);
        
      
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(1, 1, 1);
    }
}
