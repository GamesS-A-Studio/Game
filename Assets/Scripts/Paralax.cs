
using UnityEngine;


[ExecuteInEditMode]
public class Paralax : MonoBehaviour
{
    public delegate void ParallaxCameraDelegate(float deltaMovementX, float deltaMovementY);
    public ParallaxCameraDelegate onCameraTranslate;

    private Vector3 previousCameraPosition;

    void Start()
    {
        previousCameraPosition = transform.position;
    }

    void Update()
    {
        if (transform.position != previousCameraPosition)
        {
            if (onCameraTranslate != null)
            {
                float deltaX = transform.position.x - previousCameraPosition.x;
                float deltaY = transform.position.y - previousCameraPosition.y;
                onCameraTranslate(deltaX, deltaY);
            }

            previousCameraPosition = transform.position;
        }
    }
}
       