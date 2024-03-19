
using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    public GameObject uiText;

    // Start is called before the first frame update

    public void APPDamageText(GameObject ob, string damageText, Color cor)
    {
        Vector3 vec = new Vector3(Random.Range(transform.position.x - 0.5f, transform.position.x + 0.5f), transform.position.y, transform.position.z);
        GameObject oi = Instantiate(ob, vec, Quaternion.identity);
        oi.GetComponentInChildren<TextMeshProUGUI>().text = damageText;
        oi.GetComponentInChildren<TextMeshProUGUI>().color = cor;
        Destroy(oi.gameObject, 1.5f);


    }
}
