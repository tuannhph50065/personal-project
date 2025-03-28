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
    public float EnemiPerSpawn = 3;
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
                for (int i = 0; EnemiPerSpawn > 0; i++)
                {
                    SpawnEnemy(); // Sinh một quái thường
                    spawnTimer = spawnInterval; // Đặt lại bộ đếm
                }
            }

            if (EnemyController.EnemiesKilled >= 10 && !bossSpawned) // Nếu đã tiêu diệt 20 quái
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

        if (bossPrefab != null)
        {
            Vector2 randomCircle = Random.insideUnitCircle.normalized * spawnRadius;
            Vector3 spawnPosition = player.position + new Vector3(randomCircle.x, randomCircle.y, 0f);
            GameObject Boss = Instantiate(bossPrefab, spawnPosition, Quaternion.identity);
            BossController enemyController = Boss.GetComponent<BossController>(); // Lấy script của quái
            enemyController.target = player; // Gán mục tiêu là nhân vật
            int rd = Random.Range(0, 2);
        }
    }
}