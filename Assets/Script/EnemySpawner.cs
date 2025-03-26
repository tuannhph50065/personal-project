using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // Mảng prefab quái thường (Normal, Fast, Tank)
    public GameObject bossPrefab; // Prefab của boss
    public GameObject fencePrefab; // Prefab của hàng rào
    public Transform player; // Tham chiếu đến nhân vật
    public float spawnInterval = 2f; // Thời gian giữa các lần sinh quái
    public float spawnRadius = 10f; // Bán kính sinh quái quanh nhân vật
    private float spawnTimer; // Bộ đếm thời gian sinh quái
    private bool bossSpawned = false; // Cờ kiểm tra boss đã sinh chưa
    private GameObject[] fences; // Mảng lưu các hàng rào

    void Start()
    {
        spawnTimer = spawnInterval; // Khởi tạo bộ đếm thời gian
    }

    void Update()
    {
        if (!bossSpawned) // Chỉ sinh quái thường khi chưa có boss
        {
            spawnTimer -= Time.deltaTime; // Giảm thời gian mỗi frame
            if (spawnTimer <= 0f) // Nếu hết thời gian chờ
            {
                SpawnEnemy(); // Sinh một quái thường
                spawnTimer = spawnInterval; // Đặt lại bộ đếm
            }

            if (EnemyController.EnemiesKilled >= 5 && !bossSpawned) // Nếu đã tiêu diệt 20 quái
            {
                SpawnBossAndFences(); // Sinh boss và hàng rào
                bossSpawned = true; // Đánh dấu boss đã xuất hiện
            }
        }
    }

    void SpawnEnemy()
    {
        if (enemyPrefabs.Length > 0 && player != null) // Kiểm tra mảng prefab và nhân vật
        {
            int randomIndex = Random.Range(0, enemyPrefabs.Length); // Chọn ngẫu nhiên một loại quái
            GameObject enemyPrefab = enemyPrefabs[randomIndex]; // Lấy prefab tương ứng
            Vector2 randomCircle = Random.insideUnitCircle.normalized * spawnRadius; // Tạo vị trí ngẫu nhiên
            Vector3 spawnPosition = player.position + new Vector3(randomCircle.x, randomCircle.y, 0f); // Tính vị trí sinh
            GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity); // Sinh quái
            EnemyController enemyController = enemy.GetComponent<EnemyController>(); // Lấy script của quái
            enemyController.target = player; // Gán mục tiêu là nhân vật
            int rd = Random.Range(0, 2);

            
        }
    }

    void SpawnBossAndFences()
    {
        GameObject[] existingEnemy = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in existingEnemy)
        {
         Destroy(enemy);   
        }
        

        if (fencePrefab != null) // Kiểm tra prefab hàng rào
        {
            float fenceSize = 20f; // Kích thước khu vực hàng rào
            fences = new GameObject[4]; // Tạo mảng lưu 4 hàng rào
            fences[0] = Instantiate(fencePrefab, player.position + new Vector3(0, fenceSize / 2, 0), Quaternion.identity); // Hàng rào trên
            fences[1] = Instantiate(fencePrefab, player.position + new Vector3(0, -fenceSize / 2, 0), Quaternion.identity); // Hàng rào dưới
            fences[2] = Instantiate(fencePrefab, player.position + new Vector3(-fenceSize / 2, 0, 0), Quaternion.Euler(0, 0, 90)); // Hàng rào trái
            fences[3] = Instantiate(fencePrefab, player.position + new Vector3(fenceSize / 2, 0, 0), Quaternion.Euler(0, 0, 90)); // Hàng rào phải

            foreach (GameObject fence in fences) // Đặt kích thước cho từng hàng rào
            {
                fence.transform.localScale = new Vector3(fenceSize, 1f, 1f); // Kéo dài hàng rào
            }
        }

        if (bossPrefab != null && player != null)
        {
            Vector3 bossPosition = player.position;
            GameObject boss = Instantiate(bossPrefab, bossPosition, Quaternion.identity);
            BossController bossController = boss.GetComponent<BossController>();
            bossController.target = player;
            Debug.Log("bossSpawned");
        }
    }
}