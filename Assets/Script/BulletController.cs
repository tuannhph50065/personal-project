using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector2 huong;
    public float damage = 1f;

    private void Start()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;
        huong = pos - transform.position;
        huong.Normalize();
        Destroy(gameObject, 2f);
    }

    private void Update()
    {
        transform.Translate(huong * 12f * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Va chạm với Enemy
        if (other.gameObject.CompareTag("Enemy"))
        {
            EnemyController enemy = other.gameObject.GetComponent<EnemyController>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
        // Va chạm với Boss
        else if (other.gameObject.CompareTag("Boss"))
        {
            BossController boss = other.gameObject.GetComponent<BossController>();
            if (boss != null)
            {
                boss.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }
}