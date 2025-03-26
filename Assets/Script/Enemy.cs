
using System.Collections;
using UnityEngine;


public class EnemyController : MonoBehaviour
{
    public Transform target;
    public float moveSpeed = 2f;
    public float maxHealth = 3f;
    private float currentHealth;
    public float damage = 1f;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    public static int EnemiesKilled = 0;

    // Thêm biến cho hiệu ứng nhấp nháy
    private Color originalColor;
    public float flashDuration = 0.1f;
    public int flashCount = 3;

    void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color; // Lưu màu gốc
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        MoveTowardsPlayer();
    }

    void MoveTowardsPlayer()
    {
        if (target == null) return;

        Vector2 direction = (target.position - transform.position).normalized;
        rb.linearVelocity = direction * moveSpeed;

        spriteRenderer.flipX = direction.x < 0;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log($"Enemy HP: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(Flash()); // Kích hoạt hiệu ứng nhấp nháy
        }
    }

    void Die()
    {
        EnemiesKilled++;
        Debug.Log($"Enemy died! Total killed: {EnemiesKilled}");
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                player.TakeDamage(damage);
            }
        }
    }

    private IEnumerator Flash()
    {
        for (int i = 0; i < flashCount; i++)
        {
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(flashDuration);
            spriteRenderer.color = originalColor;
            yield return new WaitForSeconds(flashDuration);
        }
    }
}