using System.Collections;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5.0f; // Speed of player movement
    public int health = 100; // Player's starting health
    public int score = 0;
    public GameObject destroyEffect;
    public TextMeshProUGUI scoreText;
    public GameObject bulletPrefab; // Bullet prefab to shoot
    public Transform shootingOffset; // The position from where the bullet will be shot

    private Rigidbody2D rb;

    public HealthBar healthBar;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        healthBar.SetMaxHealth(health);
        scoreText.text = "Score: " + score.ToString();
    }

    void Update()
    {
        Move();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    public void AddScore(int scoreAmount){
        score += scoreAmount;
        scoreText.text = "Score: " + score.ToString();
    }

    void Move()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveX, moveY);
        rb.velocity = movement * speed;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        healthBar.SetHealth(health);
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Here you can add what happens when the player dies (e.g., play an animation, show game over screen)
        Instantiate(destroyEffect,transform.position,Quaternion.identity);
        FindObjectOfType<GameManager>().GameOver();
        Destroy(this.gameObject);
    }

    void Shoot()
    {
        if (bulletPrefab && shootingOffset)
        {
            // Instantiate the bullet at the shooting offset position, with no rotation
            GameObject bullet = Instantiate(bulletPrefab, shootingOffset.position, Quaternion.identity);

            // Get the Rigidbody2D component of the bullet
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();

            if (bulletRb != null)
            {
                // Apply a force to the bullet to move it upwards
                // You can adjust the force magnitude (in this case, 10) as needed
                bulletRb.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "EnemyBullet")
        {
            TakeDamage(1);
        }
    }


}
