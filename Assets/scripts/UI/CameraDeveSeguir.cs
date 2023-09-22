using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDeveSeguir : MonoBehaviour
{
    [SerializeField] Transform player;

    public float MinX, MaxX, MinY, MaxY;
    public float smoothTime;
    public float speed;
    private Vector3 velocity = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 currentPosition = transform.position;

        // Obt�m a posi��o atual do jogador
        Vector3 playerPosition = player.position;

        // Limita a posi��o do jogador dentro dos limites estabelecidos
        playerPosition.x = Mathf.Clamp(playerPosition.x, MinX, MaxX);
        playerPosition.y = Mathf.Clamp(playerPosition.y, MinY, MaxY);

        // Move a c�mera em dire��o � posi��o do jogador usando MoveTowards
        Vector3 moveTowardsPosition = Vector3.MoveTowards(currentPosition, playerPosition, speed * Time.deltaTime);

        // Suaviza ainda mais a posi��o da c�mera usando SmoothDamp
        Vector3 smoothedPosition = Vector3.SmoothDamp(moveTowardsPosition, playerPosition, ref velocity, smoothTime);

        // Atualiza a posi��o da c�mera, mantendo a coordenada z a -10 para profundidade em um ambiente 3D
        transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, -10);


    }

}
