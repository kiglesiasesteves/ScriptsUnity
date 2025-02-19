using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllers : MonoBehaviour
{
    public GameObject player; 

    // Configuración para la vista en tercera persona.
    private Vector3 thirdPersonOffset; // Offset calculado dinámicamente al iniciar.
    public float thirdPersonHeight = 10f; // Altura de la cámara en tercera persona.
    public float thirdPersonDistance = 10f; // Distancia de la cámara al jugador.
    public float thirdPersonAngle = 90f; // Ángulo de inclinación.

    // Configuración para la vista en primera persona.
    public float rotationSpeed = 100f; // Velocidad de rotación para la cámara en primera persona.
    public float firstPersonHeightOffset = 0.5f; // Altura de la cámara en primera persona.

    private float rotationX = 0f; // Rotación acumulada en el eje vertical (primera persona).
    private float rotationY = 0f; // Rotación acumulada en el eje horizontal (primera persona).

    private bool isFirstPerson = false; // Indica si está en modo primera persona.

    void Start()
    {
        // Configurar el offset inicial para la vista en tercera persona.
        thirdPersonOffset = new Vector3(0, thirdPersonHeight, -thirdPersonDistance);

        // Iniciar en vista de tercera persona.
        isFirstPerson = false;
    }

    void Update()
    {
        // Alternar entre cámaras con las teclas 1 y 2.
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            isFirstPerson = false;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            isFirstPerson = true;
        }

        // Controlar la rotación si está en primera persona.
        if (isFirstPerson)
        {
            CamaraPrimeraPersona();
        }
    }

    void LateUpdate()
    {
        if (isFirstPerson)
        {
            // Mantener la cámara en la posición del jugador con un offset en altura.
            transform.position = player.transform.position + Vector3.up * firstPersonHeightOffset;
        }
        else
        {
            // Configurar la cámara en tercera persona.
            Vector3 desiredPosition = player.transform.position + thirdPersonOffset;
            transform.position = desiredPosition;

            // Hacer que la cámara mire al jugador.
            transform.LookAt(player.transform.position);
        }
    }

    void CamaraPrimeraPersona()
    {
        // Rotación horizontal con A y D.
        if (Input.GetKey(KeyCode.A))
        {
            rotationY -= rotationSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            rotationY += rotationSpeed * Time.deltaTime;
        }

        // Rotación vertical con W y S.
        if (Input.GetKey(KeyCode.W))
        {
            rotationX -= rotationSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            rotationX += rotationSpeed * Time.deltaTime;
        }

        // Limitar la rotación vertical.
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        // Aplicar la rotación calculada.
        Quaternion rotation = Quaternion.Euler(rotationX, rotationY, 0f);
        transform.rotation = rotation;
    }
}