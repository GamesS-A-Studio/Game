using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Transform[] backgrounds;
    private float[] parallaxScales;
    public float smoothing = 1f;

    public float horizontalSpeed = 2f;
    public float verticalSpeed = 2f;

    private Transform cam;
    private Vector3 previousCamPos;

    void Start()
    {
        cam = GameObject.Find("CameraMan").transform;

        previousCamPos = cam.position;

        parallaxScales = new float[backgrounds.Length];

        for (int i = 0; i < backgrounds.Length; i++)
        {
            parallaxScales[i] = backgrounds[i].position.z * -1;
        }
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontal * horizontalSpeed, vertical * verticalSpeed, 0);
        if(cam!= null)
        {
            cam.position += direction * Time.deltaTime;
        }
     
        if(backgrounds!=null)
        {
            for (int i = 0; i < backgrounds.Length; i++)
            {
                float parallax = (previousCamPos.x - cam.position.x) * parallaxScales[i];

                float backgroundTargetPosX = backgrounds[i].position.x + parallax;
                float backgroundTargetPosY = backgrounds[i].position.y + (vertical * parallaxScales[i]);

                Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgroundTargetPosY, backgrounds[i].position.z);

                backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
            }
        }
        if (cam != null)
        {
            previousCamPos = cam.position;
        }
    }
}