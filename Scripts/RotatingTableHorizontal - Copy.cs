using UnityEngine;

public class RotatingTable : MonoBehaviour
{
    // Velocidad de rotación en grados por segundo
    public float rotationSpeed = 30f;

    void Update()
    {
        // Rotar en torno al eje Y (horizontal)
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
}
