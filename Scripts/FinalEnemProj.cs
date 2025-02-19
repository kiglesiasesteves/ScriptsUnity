using UnityEngine;

public class FinalEnemyProjectile : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 1;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (rb == null)
        {
            Debug.LogError("No se encontró un Rigidbody en " + gameObject.name);
            return;
        }

        rb.freezeRotation = true;
        rb.velocity = transform.forward * speed; // Mueve el proyectil usando físicas
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("Colisión con: " + other.gameObject.name);

         if (other.gameObject.CompareTag("Player"))
        {
            
            other.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
            Destroy(gameObject);  
            
            Debug.LogWarning("El objeto impactado no tiene PlayerHealth: " + other.gameObject.name);

        }
        else if (other.gameObject.CompareTag("Walls"))
        {
            Destroy(gameObject);  
        }
    }
}
