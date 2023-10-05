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
        Vector3 playerPosition = player.position;
        playerPosition.x = Mathf.Clamp(playerPosition.x, MinX, MaxX);
        playerPosition.y = Mathf.Clamp(playerPosition.y, MinY, MaxY);

        Vector3 moveTowardsPosition = Vector3.Lerp(currentPosition, playerPosition, speed * Time.deltaTime);
        Vector3 smoothedPosition = Vector3.SmoothDamp(moveTowardsPosition, playerPosition, ref velocity, smoothTime);
        transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, -10);

    }

}
