using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 100; // Enemy's starting health
    public float attackSpeed = 1f; // Time between attacks in seconds
    public int scoreToAdd;
    public GameObject bulletPrefab; // Bullet prefab to shoot
    public GameObject explosionPrefab; // Explosion effect prefab
    public Transform shootingOffset; // The position from where the bullet will be shot

    private float attackTimer;

    Player player;

    private void Start() {
        player = FindObjectOfType<Player>();
    }

    void Update()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer >= attackSpeed)
        {
            Shoot();
            attackTimer = 0;
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Instantiate explosion effect at the enemy's position
        if (explosionPrefab)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }
        
        player.AddScore(scoreToAdd);
        FindObjectOfType<CameraShaker>().ShakeCamera(0.35f,0.2f);
        Destroy(gameObject); // Destroy the enemy object
    }

    void Shoot()
    {
        if (bulletPrefab && shootingOffset)
        {
            // Instantiate the bullet at the shooting offset position, with the same rotation as the enemy
            GameObject bullet = Instantiate(bulletPrefab, shootingOffset.position, transform.rotation);

            // Get the Rigidbody2D component of the bullet
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();

            if (bulletRb != null)
            {
                // Apply a force to the bullet in the direction the enemy is facing
                // Adjust the force magnitude (e.g., 10) as needed for appropriate bullet speed
                bulletRb.AddForce(shootingOffset.transform.up * 7, ForceMode2D.Impulse);
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Bullet"){
            TakeDamage(1);
        }
    }

}
