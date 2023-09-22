using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DamageText : MonoBehaviour
{
   
    public TextMeshProUGUI damage;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 2f);
    }

    // Update is called once per frame
    public void SetText(string value)
    {
        damage.text = value;
    }
    public void SetText2(string value2)
    {
        damage.text = value2;
    }
}
