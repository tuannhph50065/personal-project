using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D _rb;
    private Animator _anim;
    public float maxHealth = 10f;
    public float health;
    bool dead;
    public GameObject gun;

    public int atk;
    

    Vector2 movement;
    private float moveX;
    private float moveY;
  
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    public float flashDuration = 0.1f;
    public int flashCount = 3;

    void Start()
    {
        atk = 1;
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        health = maxHealth;
        GameManager.gm.ShowHeath(health, maxHealth);
        GameManager.gm.ShowSpeed(speed);
        GameManager.gm.ShowatkDmg(atk);
    }

    void Update()
    {
        if (!dead && !GameManager.gm.Pause)
        {
            moveX = Input.GetAxisRaw("Horizontal");
            moveY = Input.GetAxisRaw("Vertical");
            movement = new Vector2(moveX, moveY).normalized;
            _rb.linearVelocity = movement * speed;
            if (movement.magnitude > 0)
            {
                _anim.SetBool("isMoving", true);
                _anim.SetFloat("X", moveX);
                _anim.SetFloat("Y", moveY);

                // Lưu hướng đứng yên cuối cùng để quay mặt đúng
                if (moveX == 0 && moveY < 0) _anim.SetFloat("direct", 1);
                if (moveX < 0 && moveY == 0) _anim.SetFloat("direct", 2);
                if (moveX == 0 && moveY > 0) _anim.SetFloat("direct", 3);
                if (moveX > 0 && moveY == 0) _anim.SetFloat("direct", 4);
            }
            else
            {
                _anim.SetBool("isMoving", false);
            }
        }
        else
        {
            movement = Vector2.zero;
        }
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
            GameManager.gm.ShowHeath(health, maxHealth);
        }

        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("HeathPickup"))
        {
            Heal(2f);
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Speed"))
        {
            StartCoroutine(SpeedBoost());
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Atk"))
        {
            BoostAttack();
            Destroy(other.gameObject);
        }
    }

    public void BoostAttack()
    {
        atk += 1;
        GameManager.gm.ShowatkDmg(atk);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log($"Health: {health}");
        if (health <= 0)
        {

            StartCoroutine(DelayDead());    
        }
        else
        {
            StartCoroutine(Flash());
        }
    }

    IEnumerator DelayDead()
    {
        dead = true;
        gun.SetActive(false);
        Collider2D collider = GetComponent<Collider2D>();
        collider.enabled = false;
        _anim.Play($"PlayerDead");
        yield return new WaitForSeconds(1.5f);
        Time.timeScale = 0f;
        GameManager.gm.Playerdead();
    }


    public IEnumerator SpeedBoost()
    {
        GameManager.gm.ShowSpeed(speed);
        speed *= 1.5f;
        yield return new WaitForSeconds(3f);
        speed /= 1.5f;
        
    }

    public void Heal(float amount)
    {
        health += amount;
        Debug.Log($"Health: {health}");
        if (health > maxHealth) health = maxHealth;
       
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