using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private int count;
    private int winLevel = 5;

    private float movementX;
    private float movementY;
    public string nextSceneName;
    public float jumpForce = 5f;
    private bool isGrounded;
    public PlayerState currentState = PlayerState.Inactive;

    public float speed = 10f;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public Transform cameraTransform; // Transform de la cámara para orientar el movimiento.
    private Animator animator; // Referencia al Animator del jugador

    public enum PlayerState
    {
        Inactive,
        Moving,
        Jumping,
        Win
    }

    void Start()
    {
        winTextObject.SetActive(false);
        SetCountText();
        count = 0;
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        
        if (animator == null)
        {
            Debug.LogError("No se encontró el componente Animator en el objeto Player");
        }

        // Si la cámara principal está asignada correctamente en la escena, la buscamos automáticamente.
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;
        }
    }

    void OnMove(InputValue movementValue)
    {
        // Detectar las teclas de flecha
        if (Keyboard.current.upArrowKey.isPressed || Keyboard.current.downArrowKey.isPressed ||
            Keyboard.current.leftArrowKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
        {
            Vector2 movementVector = movementValue.Get<Vector2>();
            movementX = movementVector.x;
            movementY = movementVector.y;
        }
        else
        {
            // No se mueve si no se presionan las teclas de flecha
            movementX = 0;
            movementY = 0;
        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= winLevel)
        {
            winTextObject.SetActive(true);
            currentState = PlayerState.Win;  // El jugador ha ganado
            Debug.Log("Estado del jugador: Win");
            animator.SetTrigger("HasWon");  // Activar la animación de "Win"
            StartCoroutine(WaitAndLoadScene());
        }
    }

    // Corrutina para esperar 5 segundos antes de cambiar de nivel
    IEnumerator WaitAndLoadScene()
    {
        yield return new WaitForSeconds(5f); // Esperar 5 segundos
        SceneManager.LoadScene(nextSceneName); // Cargar la siguiente escena
    }

    void FixedUpdate()
    {
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        // Movimiento controlado por acelerómetro
        Vector3 dir = Vector3.zero;
        dir.x = -Input.acceleration.y;  // Movimiento en el eje X (de izquierda a derecha)
        dir.z = Input.acceleration.x;   // Movimiento en el eje Z (hacia adelante y hacia atrás)

        // Asegúrate de que el valor de dir no sea mayor que 1 en su magnitud
        if (dir.sqrMagnitude > 1)
            dir.Normalize();

        dir *= Time.deltaTime;
        transform.Translate(dir * speed, Space.World);

        // Control de animaciones según el estado
        if (dir.magnitude > 0)
        {
            if (currentState != PlayerState.Moving)
            {
                currentState = PlayerState.Moving;
                Debug.Log("Estado del jugador: Moving");
                animator.SetBool("IsInactive", false);  
            }
        }
        else
        {
            if (currentState != PlayerState.Inactive)
            {
                currentState = PlayerState.Inactive;
                Debug.Log("Estado del jugador: Inactive");
                animator.SetBool("IsInactive", true); 
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count += 1;
            SetCountText();
            Debug.Log("Objeto recogido. Count: " + count);
        }
    }
}
