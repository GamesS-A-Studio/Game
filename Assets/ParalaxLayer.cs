
using UnityEngine;

[ExecuteInEditMode]
public class ParalaxLayer : MonoBehaviour
{
    public float speedHorizontal;
    public float speedVertical;
    public float distMaxHori;
    public float distMinHori;
    public float distMaxVer;
    public float distMinVer;

    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.localPosition;
    }

    public void Move(float deltaX, float deltaY)
    {
        Vector3 newPos = transform.localPosition;
        float maxX = initialPosition.x + distMaxHori;
        float minX = initialPosition.x + distMinHori;
        float maxY = initialPosition.y + distMaxVer;
        float minY = initialPosition.y + distMinVer;

        float newX = newPos.x - (deltaX * speedHorizontal);
        float newY = newPos.y - (deltaY * speedVertical);

        float clampedX = Mathf.Clamp(newX, minX, maxX);
        float clampedY = Mathf.Clamp(newY, minY, maxY);

        // Se o movimento horizontal ultrapassou os limites, muda a direção horizontal
        if (newX != clampedX)
        {
            speedHorizontal *= -1; // Inverte a direção horizontal
            newX = newPos.x - (deltaX * speedHorizontal); // Recalcula a posição horizontal
            clampedX = Mathf.Clamp(newX, minX, maxX); // Garante que o novo valor esteja dentro dos limites
        }

        // Se o movimento vertical ultrapassou os limites, muda a direção vertical
        if (newY != clampedY)
        {
            speedVertical *= -1; // Inverte a direção vertical
            newY = newPos.y - (deltaY * speedVertical); // Recalcula a posição vertical
            clampedY = Mathf.Clamp(newY, minY, maxY); // Garante que o novo valor esteja dentro dos limites
        }

        transform.localPosition = new Vector3(clampedX, clampedY, newPos.z);
    }

}
