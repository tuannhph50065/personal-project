using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] List<EnemyInfomation> listEnemyLevel;
    public Transform player;
    public GameObject slime;
    float timeSpawn;
    Vector3 pos;

    // Update is called once per frame
    void Update()
    {
        if (timeSpawn > 0)
        {
            timeSpawn -= Time.deltaTime;
        }
        else
        {
           Vector3 huong = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
           huong.Normalize();
           pos = player.position + huong * Random.Range(15f, 20f);

            GameObject s =  Instantiate(slime, pos, Quaternion.identity);
            Slime getScript = s.GetComponent<Slime>();
            getScript.player = player;
            EnemyInfomation info = listEnemyLevel[2];
            getScript.SetInfo(info.health, info.speed, info.atkDmg, info.def);

            timeSpawn = Random.Range(.1f, 1f);
        }
    }
}