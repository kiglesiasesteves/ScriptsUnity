using UnityEngine;

public class ShootingFinalEnemy : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform shootingPoint;
    public float fireRate = 1f;  // Cuánto tiempo pasa entre cada disparo
    private float nextFireTime = 0f;

    void Update()
    {
        // Dispara el proyectil en intervalos regulares
        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

void Shoot()
{
    Debug.Log("Disparando proyectil Final Enemy");
    Instantiate(projectilePrefab, shootingPoint.position, shootingPoint.rotation);
}

}