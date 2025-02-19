using UnityEngine;
using TMPro;  // Necesario para trabajar con TextMeshPro
using UnityEngine.SceneManagement;  // Necesario para cargar escenas

public class PlayerHealth : MonoBehaviour
{
    public int health = 3;  // Salud inicial del jugador
    public TextMeshProUGUI TextoVida;  // Referencia al componente de texto de TextMeshProUGUI
    public bool invulnerable = false;

    void Start()
    {
        UpdateHealthText(); // Actualiza el texto al iniciar
    }

    public void TakeDamage(int damage)
    {
        if (!invulnerable)
        {
            health -= damage;
            UpdateHealthText(); // 🔹 Llamamos a UpdateHealthText() después de reducir la vida

            if (health <= 0)
            {
                Die();
            }
        }
    }

    private void Die()
    {
        Debug.Log("¡El jugador ha muerto!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void UpdateHealthText()
    {
        if (TextoVida != null) // Evita errores si el texto no está asignado
        {
            TextoVida.text = "Vidas: " + health.ToString();
        }
        else
        {
            Debug.LogError("TextoVida no está asignado en el Inspector.");
        }
    }
}
