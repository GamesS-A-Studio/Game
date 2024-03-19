using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class CameraSeguir : MonoBehaviour
{
    public Vector3 offset;
    public Vector3 offsetOriginal;
    public Transform playerTrans;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        playerTrans = GameObject.FindGameObjectWithTag("Player").transform;
        offsetOriginal = transform.position - playerTrans.position;
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 targetPosition = playerTrans.position + offset;
        targetPosition.y = transform.position.y;
        transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);

    }
}
