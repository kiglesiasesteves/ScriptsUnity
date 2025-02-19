using UnityEngine;
using UnityEngine.SceneManagement; 

public class BallController : MonoBehaviour
{
    public float positionY = -50f;
    private void Update()
    {
        if (transform.position.y <= positionY)
        {
            RestartGame();
        }
    }

    // Función para reiniciar el juego
    private void RestartGame()
    {
        // Reiniciar la escena actual
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
