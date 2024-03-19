
using UnityEngine;

public class Lookat : MonoBehaviour
{
    GameManager gm;
    public MoveEnemy mv;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.gmInstance;
    }

    // Update is called once per frame
    void Update()
    {
        if(mv != null)
        {
            if(mv.começouAtaque)
            {
                transform.LookAt(gm.movimentPlayer.transform);
            }

        }
        else
        {
            transform.LookAt(gm.movimentPlayer.transform.position);
        }
        
    }
}
