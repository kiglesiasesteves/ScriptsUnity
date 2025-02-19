using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 1;

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            
            // Aquí puedes llamar a un método para dañar al jugador
            other.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
            Destroy(gameObject);  // El proyectil se destruye al chocar
        }
        else if (other.gameObject.CompareTag("Walls"))
        {
            Destroy(gameObject);  // El proyectil se destruye al chocar con una pared
        }
    }
}
