using UnityEngine;

public class SpeedOnRamp : MonoBehaviour
{
    public float speedBoost = 5f; // Cuánto se incrementará la velocidad del jugador

    private void OnTriggerEnter(Collider other)
    {
        // Comprobar si el objeto que entra en el trigger es el jugador
        if (other.CompareTag("Player"))
        {
            // Aplicar el aumento de velocidad
            other.GetComponent<Rigidbody>().velocity += new Vector3(0, 0, speedBoost);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Comprobar si el jugador sale de la rampa
        if (other.CompareTag("Player"))
        {
            // Aquí podrías añadir lógica para restablecer la velocidad si fuera necesario.
        }
    }
}
