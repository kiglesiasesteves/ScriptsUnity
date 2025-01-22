using UnityEngine;
using UnityEngine.InputSystem;

public class FirstPersonControllers : MonoBehaviour
{
    public float moveSpeed = 5f; // Velocidad de movimiento de la cámara

    void Update()
    {
        // Mover la cámara y ajustarla según la dirección del jugador
        MoveAndRotateCamera();
    }

    void MoveAndRotateCamera()
    {
        float moveX = 0f;
        float moveZ = 0f;

        // Detectar las teclas de movimiento (flechas)
        if (Keyboard.current.upArrowKey.isPressed) moveZ = 1f;
        if (Keyboard.current.downArrowKey.isPressed) moveZ = -1f;
        if (Keyboard.current.rightArrowKey.isPressed) moveX = 1f;
        if (Keyboard.current.leftArrowKey.isPressed) moveX = -1f;

        // Calcular la dirección de movimiento
        Vector3 moveDirection = new Vector3(moveX, 0f, moveZ);

        if (moveDirection != Vector3.zero)
        {
            // Ajustar la rotación de la cámara para que mire en la dirección del movimiento
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection); // Quaternion es una estructura que representa una rotación en 3D y sirve para almacenar y manipular rotaciones
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f); // En esta linea, se interpola entre la rotación actual y la rotación objetivo para que la transición sea suave
        }

        // Aplicar movimiento a la cámara
        transform.position += moveDirection.normalized * moveSpeed * Time.deltaTime;
    }
}