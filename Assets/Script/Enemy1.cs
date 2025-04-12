using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    public Transform player;
    public Animator animator;
    public float atkRange;

    public float health;
    public float speedMove;
    public float atkDmg;
    public float def;
    public float atkCd;

    public List<GameObject> items;

    public EnemyHealthBar healthBar; // Thanh máu gắn ngoài prefab

    float time;
    bool die;

    public void SetInfo(float health, float speedMove, float atkDmg, float def)
    {
        this.health = health;
        this.speedMove = speedMove;
        this.atkDmg = atkDmg;
        this.def = def;

        if (healthBar != null)
        {
            healthBar.SetMaxHealth(health);
        }
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameManager.gm.player;
    }

    void Update()
    {
        if (!die)
        {
            float distance = Vector3.Distance(transform.position, player.position);

            if (distance > atkRange)
            {
                Vector3 huongToiPlayer = (player.position - transform.position).normalized;
                transform.Translate(huongToiPlayer * speedMove * Time.deltaTime);
                animator.SetBool("run", true);
            }
            else
            {
                animator.SetBool("run", false);
                if (time > 0)
                {
                    time -= Time.deltaTime;
                }
                else
                {
                    animator.Play("SlimeAtk");
                    time = atkCd;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (die) return;

        if (collision.CompareTag("sword"))
        {
            TakeDamage(3f); // Sát thương cố định từ kiếm
        }

        if (collision.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
            TakeDamage(3f); // Sát thương cố định từ đạn
        }
    }

    private void TakeDamage(float damage)
    {
        health -= damage;

        if (healthBar != null)
        {
            healthBar.SetHealth(health);
        }

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        die = true;

        GameManager.gm.AddScore(100);

        float dropRate = Random.Range(0f, 100f);
        if (dropRate < 30) // 30% rơi item
        {
            float rd = Random.Range(0f, 100f);
            if (rd < 5) Instantiate(items[2], transform.position, Quaternion.identity);
            else if (rd < 15) Instantiate(items[1], transform.position, Quaternion.identity);
            else Instantiate(items[0], transform.position, Quaternion.identity);
        }

        Destroy(gameObject, 0.3f);
    }
}
