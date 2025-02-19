using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // Asegura que usas TMPro

public class WinScript : MonoBehaviour
{
    public TextMeshProUGUI winText; // Asegúrate de que usas TextMeshProUGUI para UI
    private bool hasWon = false;

    void Start()
    {
        winText.gameObject.SetActive(false); // Oculta el mensaje al inicio
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasWon)
        {
            hasWon = true;
            winText.gameObject.SetActive(true); // Muestra el mensaje de victoria
            winText.text = "¡Has ganado el juego!";

            // Desactiva la muerte del jugador
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.invulnerable = true;
            }

            // Inicia la espera de 5 segundos y reinicia el juego
            StartCoroutine(RestartGame());
        }
    }

    private IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(5); // Espera 5 segundos
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reinicia la escena
    }
}
