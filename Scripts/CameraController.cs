using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Referencia al objeto jugador.
    public GameObject player;

    // La distancia entre la cámara y el jugador.
    private Vector3 offset;

    // Start se llama antes de la primera actualización del frame.
    void Start()
{
    if (player == null) // Si no está asignado manualmente
    {
        player = GameObject.FindWithTag("Player"); // Busca el objeto con el tag "Player"
    }

    if (player != null)
    {
        offset = transform.position - player.transform.position;
    }
    else
    {
        Debug.LogError("No se encontró un objeto con la etiqueta 'Player'. Asigna el objeto en el Inspector o revisa el tag.");
    }
}
    // LateUpdate se llama una vez por frame después de que todas las funciones Update hayan sido completadas.
    void LateUpdate()
    {
        // Mantiene el mismo desplazamiento entre la cámara y el jugador durante todo el juego.
        // Se ajusta la posición de la cámara sumando el desplazamiento calculado al objeto jugador.
        transform.position = player.transform.position + offset;  
    }
}