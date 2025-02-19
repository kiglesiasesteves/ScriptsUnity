using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonControllers : MonoBehaviour {
    // Reference to the player GameObject.
    public GameObject player;
    // Sensibilidad del ratón
    public float mouseSensitivity = 100.0f;
    private float xRotation = 0.0f;
    private float yRotation = 0.0f;

    // Start is called before the first frame update
    void Start(){
        // Ocultar y bloquear el cursor en el centro de la pantalla
        Cursor.lockState = CursorLockMode.Locked;
    }

    // LateUpdate is called once per frame after all Update functions have been completed.
    void LateUpdate(){
        // Obtener el movimiento del ratón
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        yRotation += mouseX;

        // Rotar la cámara en el eje X (vertical) y Y (horizontal)
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
        // Rotar el jugador en el eje Y (horizontal)
        player.transform.Rotate(Vector3.up * mouseX);

        // Asegurarse de que la cámara siga al jugador
        transform.position = player.transform.position;
    }
}