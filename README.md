### SCRIPTS DE JUGADOR Y CÁMARA

## JUGADOR

![Screenshot_20250122_123138](https://github.com/user-attachments/assets/e54cdb58-a4d9-49bb-8c2c-4e912505f59e)

Nuestro jugador se mueve con las flechas del teclado y para realizar este movimiento hemos realizado este script
```
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    // Rigidbody del jugador.
    private Rigidbody rb; 

    // Variable para llevar un registro de los objetos "PickUp" recogidos.
    private int count;

    // Movimiento en los ejes X e Y.
    private float movementX;
    private float movementY;

    // Velocidad a la que se mueve el jugador.
    public float speed = 0;

    // Componente de texto de la UI para mostrar la cantidad de objetos "PickUp" recogidos.
    public TextMeshProUGUI countText;

    // Objeto de la UI para mostrar el texto de victoria.
    public GameObject winTextObject;

    // Start se llama antes de la primera actualización del frame.
    void Start()
    {
        // Obtener y almacenar el componente Rigidbody adjunto al jugador.
        rb = GetComponent<Rigidbody>();

        // Inicializar el contador a cero.
        count = 0;

        // Actualizar la visualización del contador.
        SetCountText();

        // Inicialmente, establecer el texto de victoria como inactivo.
        winTextObject.SetActive(false);
    }

    // Esta función se llama cuando se detecta una entrada de movimiento.
    void OnMove(InputValue movementValue)
    {
        // Convertir el valor de entrada en un Vector2 para el movimiento.
        Vector2 movementVector = movementValue.Get<Vector2>();

        // Almacenar los componentes X e Y del movimiento.
        movementX = movementVector.x; 
        movementY = movementVector.y; 
    }

    // FixedUpdate se llama una vez por cada frame con frecuencia fija.
    private void FixedUpdate() 
    {
        // Crear un vector de movimiento 3D utilizando las entradas X e Y.
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        // Aplicar una fuerza al Rigidbody para mover al jugador.
        rb.AddForce(movement * speed); 
    }

    // Esta función se llama cuando el jugador entra en contacto con un objeto en el trigger.
    void OnTriggerEnter(Collider other) 
    {
        // Comprobar si el objeto con el que el jugador ha colisionado tiene la etiqueta "PickUp".
        if (other.gameObject.CompareTag("PickUp")) 
        {
            // Desactivar el objeto colisionado (haciendo que desaparezca).
            other.gameObject.SetActive(false);

            // Incrementar el contador de objetos "PickUp" recogidos.
            count = count + 1;

            // Actualizar la visualización del contador.
            SetCountText();
        }
    }

    // Función para actualizar el texto que muestra la cantidad de objetos "PickUp" recogidos.
    void SetCountText() 
    {
        // Actualizar el texto del contador con el valor actual.
        countText.text = "Count: " + count.ToString();

        // Comprobar si el contador ha alcanzado o superado la condición de victoria.
        if (count >= 9)
        {
            // Mostrar el texto de victoria.
            winTextObject.SetActive(true);

            // Destruir el objeto con la etiqueta "Enemy".
            Destroy(GameObject.FindGameObjectWithTag("Enemy"));
        }
    }

    // Esta función se llama cuando el jugador colisiona con otro objeto.
    private void OnCollisionEnter(Collision collision)
    {
        // Si el objeto con el que colisionamos tiene la etiqueta "Enemy".
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Destruir el objeto actual (el jugador).
            Destroy(gameObject); 

            // Mostrar el texto de derrota.
            winTextObject.gameObject.SetActive(true);
            winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose!";
        }
    }

    // Update se llama una vez por frame.
    void Update()
    {
        // Si detectamos que las teclas WASD están siendo presionadas, evitamos que el jugador se mueva.
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            movementX = 0;
            movementY = 0;
        }
    }
}
}


```

En este script al final tenemos un método que bloquea la funcionalidad de awsd para una opción de script de cámaras de primera persona. 

## CÁMARA PRINCIPAL 

![image](https://github.com/user-attachments/assets/98d94094-57a0-4450-bf60-5da445f57282)

Nuestra cámara principal al inicio se coloca en el punto donde se encuentra nuestra pelota así que después lo sigue en sus movimientos para que nunca perdamos de vista la pelota.

public class CameraController : MonoBehaviour
{
    // Referencia al objeto jugador.
    public GameObject player;

    // La distancia entre la cámara y el jugador.
    private Vector3 offset;

    // Start se llama antes de la primera actualización del frame.
    void Start()
    {
        // Calcula el desplazamiento inicial entre la posición de la cámara y la posición del jugador.
        // Esto se hace restando la posición del jugador de la posición de la cámara.
        offset = transform.position - player.transform.position; 
    }

    // LateUpdate se llama una vez por frame después de que todas las funciones Update hayan sido completadas.
    void LateUpdate()
    {
        // Mantiene el mismo desplazamiento entre la cámara y el jugador durante todo el juego.
        // Se ajusta la posición de la cámara sumando el desplazamiento calculado al objeto jugador.
        transform.position = player.transform.position + offset;  
    }
}

## CÁMARA PRIMERA PERSONA

![image](https://github.com/user-attachments/assets/5a513cde-bbcf-44f1-bcbd-643c2184cdf1)

Esta cámara nos permite ver lo que la pelota está viendo en primera persona así que movemos las teclas y la cámara se moverá al son del movimiento


public class FirstPersonControllers : MonoBehaviour
{
    // Velocidad de movimiento de la cámara
    public float moveSpeed = 5f; 

    // Update se llama una vez por frame
    void Update()
    {
        // Mover la cámara y ajustarla según la dirección del jugador
        MoveAndRotateCamera();
    }

    // Función para mover y rotar la cámara
    void MoveAndRotateCamera()
    {
        // Inicializamos las variables de movimiento en los ejes X y Z
        float moveX = 0f;
        float moveZ = 0f;

        // Detectar las teclas de movimiento (flechas del teclado)
        // Si se presiona la flecha hacia arriba, se mueve en el eje Z positivo
        if (Keyboard.current.upArrowKey.isPressed) moveZ = 1f;
        // Si se presiona la flecha hacia abajo, se mueve en el eje Z negativo
        if (Keyboard.current.downArrowKey.isPressed) moveZ = -1f;
        // Si se presiona la flecha hacia la derecha, se mueve en el eje X positivo
        if (Keyboard.current.rightArrowKey.isPressed) moveX = 1f;
        // Si se presiona la flecha hacia la izquierda, se mueve en el eje X negativo
        if (Keyboard.current.leftArrowKey.isPressed) moveX = -1f;

        // Crear un vector que represente la dirección de movimiento
        Vector3 moveDirection = new Vector3(moveX, 0f, moveZ);

        // Si el jugador está moviéndose en alguna dirección (el vector de movimiento no es cero)
        if (moveDirection != Vector3.zero)
        {
            // Ajustar la rotación de la cámara para que mire en la dirección del movimiento
            // Quaternion.LookRotation crea una rotación hacia una dirección dada
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection); 
            // Interpolación esférica entre la rotación actual y la rotación objetivo para una transición suave
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f); 
        }

        // Aplicar el movimiento a la cámara
        // Normalizamos el vector de movimiento para mantener una velocidad constante en todas las direcciones
        transform.position += moveDirection.normalized * moveSpeed * Time.deltaTime;
    }
}

## CÁMARA HORIZONTAL ELÍPTICA

![image](https://github.com/user-attachments/assets/bf83aff4-d53e-4af1-853c-075dff47091f)

![image](https://github.com/user-attachments/assets/b10f1e44-e4a6-4dfa-9d14-f709d011df18)

Esta cámara nos permite crear una cámara en movimiento horizontal que no pare de moverse. 

public class CameraControllerHorizontal : MonoBehaviour
{
    // El objeto (por ejemplo, el jugador) alrededor del cual la cámara rotará.
    public Transform target;

    // Velocidad de la rotación de la cámara.
    public float rotationSpeed = 30f;

    // Start se llama antes de la primera actualización del frame.
    void Start()
    {
        // Si no se ha asignado un objetivo (target) en el Inspector, se asigna uno por defecto.
        if (target == null)
        {
            // Busca el objeto con la etiqueta "Player" y asigna su Transform como objetivo (puedes cambiar "Player" por otra etiqueta si lo necesitas).
            target = GameObject.FindWithTag("Player").transform;
        }
    }

    // Update se llama una vez por frame.
    void Update()
    {
        // Rota la cámara alrededor del objetivo (target) a la velocidad especificada.
        // La cámara rota alrededor de la posición del objetivo, sobre el eje Y (Vector3.up).
        transform.RotateAround(target.position, Vector3.up, rotationSpeed * Time.deltaTime);
        
        // Hace que la cámara siempre mire hacia el objetivo mientras rota.
        transform.LookAt(target);
    }
}
## CAMBIO DE CÁMARA 

El cambio de cámara nos permite cambiar de una cámara a otra simplemente por clicar el botón "C". Así podremos cambiar la cámara que se estea ejecutando en ese momento dependiendo de lo que sea más óptimo para el jugador. 

public class CameraSwitcher : MonoBehaviour 
{
    // Array de cámaras configuradas en el Inspector. Esto nos permitirá tener varias cámaras en la escena.
    public Camera[] cameras; 

    // Índice de la cámara activa actualmente. Comienza en 0 (la primera cámara en el array).
    private int currentCameraIndex = 0; 

    // Start se llama antes de la primera actualización del frame.
    void Start()
    {
        // Asegúrate de que solo una cámara esté activa al inicio.
        // Llama a la función ActivateCamera con el índice de la cámara activa (actualmente la primera cámara).
        ActivateCamera(currentCameraIndex);
    }

    // Update se llama una vez por frame.
    void Update()
    {
        // Detectar si se presiona la tecla "C" del teclado (usando el sistema de entrada de nuevo).
        if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            // Si se presiona "C", cambia la cámara.
            SwitchCamera();
        }
    }

    // Función para cambiar la cámara.
    void SwitchCamera()
    {
        // Desactivar la cámara actual utilizando el índice de la cámara activa.
        cameras[currentCameraIndex].enabled = false;

        // Cambiar al siguiente índice de cámara de manera cíclica (es decir, si está en la última, vuelve a la primera).
        currentCameraIndex = (currentCameraIndex + 1) % cameras.Length;

        // Activar la nueva cámara correspondiente al índice actual.
        ActivateCamera(currentCameraIndex);
    }

    // Función para activar la cámara en el índice especificado.
    void ActivateCamera(int index)
    {
        // Recorre todas las cámaras en el array y activa solo la cámara en el índice especificado.
        for (int i = 0; i < cameras.Length; i++)
        {
            cameras[i].enabled = i == index; // Solo activa la cámara cuyo índice es igual al pasado como parámetro.
        }
    }
}












