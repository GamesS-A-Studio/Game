
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ParalaxBackGround : MonoBehaviour
{
    public Paralax parallaxCamera;
    List<ParalaxLayer> parallaxLayers = new List<ParalaxLayer>();
    GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.gmInstance;
        if (parallaxCamera == null)
            parallaxCamera = gameManager.cam.GetComponent<Paralax>();

        if (parallaxCamera != null)
            parallaxCamera.onCameraTranslate += Move;

        SetLayers();
    }

    void SetLayers()
    {
        parallaxLayers.Clear();

        for (int i = 0; i < transform.childCount; i++)
        {
            ParalaxLayer layer = transform.GetChild(i).GetComponent<ParalaxLayer>();

            if (layer != null)
            {
                layer.name = "Layer-" + i;
                parallaxLayers.Add(layer);
            }
        }
    }

    void Move(float deltaX, float deltaY)
    {
        foreach (ParalaxLayer layer in parallaxLayers)
        {
            layer.Move(deltaX, deltaY);
        }
    }
}
