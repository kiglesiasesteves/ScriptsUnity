using UnityEngine;
using UnityEngine.InputSystem;

public class CameraSwitcher : MonoBehaviour 
{
    public Camera[] cameras; // Array de cámaras configuradas en el Inspector
    private int currentCameraIndex = 0; // Índice de la cámara activa

    void Start()
    {
        // Asegúrate de que solo una cámara esté activa al inicio
        ActivateCamera(currentCameraIndex);
    }

    void Update()
    {
        // Detectar si se presiona la tecla "C"
        if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            SwitchCamera();
        }
    }

    void SwitchCamera()
    {
        // Desactivar la cámara actual
        cameras[currentCameraIndex].enabled = false;

        // Cambiar al siguiente índice (ciclo circular)
        currentCameraIndex = (currentCameraIndex + 1) % cameras.Length;

        // Activar la nueva cámara
        ActivateCamera(currentCameraIndex);
    }

    void ActivateCamera(int index)
    {
        // Activa solo la cámara especificada
        for (int i = 0; i < cameras.Length; i++)
        {
            cameras[i].enabled = i == index;
        }
    }
}