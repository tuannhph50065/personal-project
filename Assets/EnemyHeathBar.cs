using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public Slider slider;
    public Vector3 offset;

    public void SetMaxHealth(float maxHealth)
    {
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
    }

    public void SetHealth(float health)
    {
        slider.value = health;
    }

    void Update()
    {
        // Luôn nhìn vào camera (nếu cần)
        transform.rotation = Quaternion.identity;
    }
}