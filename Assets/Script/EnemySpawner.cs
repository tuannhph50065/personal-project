using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] List<EnemyInfomation> listEnemyLevel;
    public Transform player;
    
    public GameObject slime;   // Quái loại 1
    public GameObject goblin;  // Quái loại 2 (mới)
    public GameObject ghost;   // Quái loại 3 (mới)
    public GameObject boss;    // Boss
    
    float timeSpawn;
    Vector3 pos;

    int enemyCount = 0; // Đếm số quái đã spawn

    // Update is called once per frame
    void Update()
    {
        if (timeSpawn > 0)
        {
            timeSpawn -= Time.deltaTime;
        }
        else
        {
            SpawnEnemyUnit();

            timeSpawn = Random.Range(0.5f, 2f); // Thời gian spawn ngẫu nhiên
        }
    }

    void SpawnEnemyUnit()
    {
        Vector3 huong = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
        huong.Normalize();
        pos = player.position + huong * Random.Range(15f, 20f);

        GameObject enemyToSpawn;
        EnemyInfomation info;

        float randomValue = Random.Range(0f, 100f);

        if (randomValue < 33) // 50% spawn Slime
        {
            enemyToSpawn = slime;
            info = listEnemyLevel[0]; // Thông tin quái Slime
        }
        else if (randomValue < 33) // 30% spawn Goblin
        {
            enemyToSpawn = goblin;
            info = listEnemyLevel[1]; // Thông tin quái Goblin
        }
        else // 20% spawn Ghost
        {
            enemyToSpawn = ghost;
            info = listEnemyLevel[2]; // Thông tin quái Ghost
        }

        GameObject enemy = Instantiate(enemyToSpawn, pos, Quaternion.identity);
        Slime getScript = enemy.GetComponent<Slime>();
        getScript.player = player;
        getScript.SetInfo(info.health, info.speed, info.atkDmg, info.def);

        enemyCount++;

        // Spawn boss sau khi có đủ 20 quái
        if (enemyCount >= 20)
        {
            SpawnBoss();
            enemyCount = 0; // Reset bộ đếm
        }
    }

    void SpawnBoss()
    {
        Vector3 huong = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
        huong.Normalize();
        pos = player.position + huong * 25f;

        GameObject bossEnemy = Instantiate(boss, pos, Quaternion.identity);
        Slime bossScript = bossEnemy.GetComponent<Slime>();
        EnemyInfomation bossInfo = listEnemyLevel[3]; // Boss có chỉ số riêng
        bossScript.SetInfo(bossInfo.health, bossInfo.speed, bossInfo.atkDmg, bossInfo.def);
    }
}
