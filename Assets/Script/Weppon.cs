using System;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    void Start()
    {
    }

    private void Update()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 dir = pos - transform.position;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, dir);
        if(dir.x > 0 ) transform.localScale = new Vector3(1, 1, 1);
        if(dir.x < 0 ) transform.localScale = new Vector3(-1, 1, 1);

        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
        }
    }
    
}