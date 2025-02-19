using UnityEngine;

public class EmpujarJUgador : MonoBehaviour
{
    // Fuerza de empuje
    public float fuerzaEmpuje = 5f;

    // Método que se llama cuando hay una colisión
    private void OnCollisionEnter(Collision collision)
    {
        // Comprobar si el objeto con el que colisionamos es el jugador
        if (collision.gameObject.CompareTag("Player"))
        {
            // Obtener la dirección de la colisión
            Vector3 direccionEmpuje = collision.transform.position - transform.position;

            // Normalizar la dirección para evitar que el empuje sea más fuerte en ciertas direcciones
            direccionEmpuje = direccionEmpuje.normalized;

            // Aplicar una fuerza de empuje en la dirección opuesta a la colisión
            Rigidbody rbJugador = collision.gameObject.GetComponent<Rigidbody>();
            if (rbJugador != null)
            {
                rbJugador.AddForce(direccionEmpuje * fuerzaEmpuje, ForceMode.Impulse);
            }
        }
    }
}

