using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D _rb;
    private Animator _anim;
    public float maxHealth = 10f;
    protected float currentHealth;
    public float health;
    
    public TextMesh scoreText;
    public Slider healthBar;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    public float flashDuration = 0.1f;
    public int flashCount = 3;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        health = maxHealth;
        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = maxHealth;
        }
    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        Vector2 movement = new Vector2(moveX, moveY).normalized * speed;
        _rb.linearVelocity = movement;

        bool isMoving = movement.magnitude > 0;
        _anim.SetBool("isMoving", isMoving);
        _anim.SetFloat("X", moveX);
        _anim.SetFloat("Y", moveY);
        if (moveX == 0 && moveY < 0) _anim.SetFloat("direct", 1);
        if (moveX < 0 && moveY == 0) _anim.SetFloat("direct", 2);
        if (moveX == 0 && moveY > 0) _anim.SetFloat("direct", 3);
        if (moveX > 0 && moveY == 0) _anim.SetFloat("direct", 4);
    }

    public Vector2 GetPosition()
    {
        return transform.position;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Boss"))
        {
            TakeDamage(1f);
        }

        if (other.gameObject.CompareTag("HeathPickup"))
        {
            Heal(2f);
            Destroy(other.gameObject);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log($"Health: {health}");
        if (health < 0) health = 0;
        if (healthBar != null)
        {
            healthBar.value = health;
        }
        if (health <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(Flash());
        }
    }

    void Die()
    {
        Debug.Log("playerDie");
        Destroy(gameObject);
    }

    private IEnumerator SpeedBoost()
    {
        speed *= 1.5f;
        yield return new WaitForSeconds(3f);
        speed /= 1.5f;
    }

    private void Heal(float amount)
    {
        health += amount;
        Debug.Log($"Health: {health}");
        if (health > maxHealth) health = maxHealth;
        if (healthBar != null)
        {
            healthBar.value = health;
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