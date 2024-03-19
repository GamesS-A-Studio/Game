
using UnityEngine;

public class checkpoint : MonoBehaviour
{
    GameManager gm;
    public AudioSource aa;
    // Start is called before the first frame update
    void Start()
    {
        aa.Stop();
        gm = GameManager.gmInstance;
    }
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.CompareTag("Player"))
        {
            gm.Save();
            aa.Play();
        }
    }
}
