using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    //luôn chạy về phía player
    //tới gần player (cách 1 khoảng 2 ô) tấn công player
    //bị player tấn công thì chết

    public Transform player;
    public Animator animator;
    public float atkRange;
    //thuộc tính
    public float health;
    public float speedMove;
    public float atkDmg;
    public float def;
    public float atkCd;

    public List<GameObject> items;
    
    //bộ đếm
    float time;

    bool die;

    public void SetInfo(float health, float speedMove, float atkDmg, float def)
    {
        this.health = health;
        this.speedMove = speedMove;
        this.atkDmg = atkDmg;
        this.def = def;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameManager.gm.player;
    }

    // Update is called once per frame
    void Update()
    {
        if (!die)
        {
            if (Vector3.Distance(transform.position, player.position) > atkRange)
            {
                //di chuyển
                Vector3 huongToiPlayer = player.position - transform.position;
                huongToiPlayer.Normalize();
                transform.Translate(huongToiPlayer * speedMove * Time.deltaTime);
                animator.SetBool("run", true);
            }
            else
            {
                animator.SetBool("run", false);
                //tấn công mỗi 2s
                if (time > 0)
                {
                    time -= Time.deltaTime;
                }
                else
                {
                    //tấn công
                    animator.Play("SlimeAtk");
                    time = atkCd;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("sword"))
        {
            die = true;
            animator.SetTrigger("die");
            Destroy(gameObject,.3f);
        }

        if (collision.CompareTag("Bullet"))
        {
            die = true;
            Destroy(collision.gameObject);
            
            GameManager.gm.AddScore(100);
            float Droprate = Random.Range(0, 100f);
            if (Droprate < 30)
            {
                float rd = Random.Range(0, 100);
                if (rd < 5) Instantiate(items[2], transform.position, Quaternion.identity);
                if (rd < 10) Instantiate(items[1], transform.position, Quaternion.identity);
                if (rd < 5) Instantiate(items[0], transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }
}
