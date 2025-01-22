using UnityEngine;
using TMPro;  // Necesario para trabajar con TextMeshPro
using UnityEngine.SceneManagement;  // Necesario para cargar escenas

public class PlayerHealth : MonoBehaviour
{
    public int health = 3;  // Salud inicial del jugador
    public TextMeshProUGUI TextoVida;  // Referencia al componente de texto de TextMeshProUGUI

    // Método llamado al principio, cuando inicia el juego
    void Start()
    {
        // Actualizar el texto con la cantidad de vidas inicial
        UpdateHealthText();
    }

    // Método para recibir daño
    public void TakeDamage(int damage)
    {
        health -= damage;

        // Actualizar el texto con la nueva cantidad de vidas
        UpdateHealthText();

        // Verificar si la salud llega a 0
        if (health <= 0)
        {
            Die();
        }
    }

    // Método para manejar la muerte del jugador
    private void Die()
    {
        Debug.Log("¡El jugador ha muerto!");

        // Aquí puedes agregar una pequeña espera antes de reiniciar, si lo deseas
        // Llamar a SceneManager para recargar la escena actual
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Método para actualizar el texto de las vidas
    private void UpdateHealthText()
    {
        TextoVida.text = "Vidas: " + health.ToString();  // Actualiza el texto para mostrar las vidas
    }
}
