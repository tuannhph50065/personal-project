using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public enum ItemType { Health, Speed, Attack }
    public ItemType itemType;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController player = collision.GetComponent<PlayerController>();

            if (player != null)
            {
                switch (itemType)
                {
                    case ItemType.Health:
                        player.Heal(2f);
                        break;
                    case ItemType.Speed:
                        player.StartCoroutine(player.SpeedBoost());
                        break;
                    case ItemType.Attack:
                        player.BoostAttack();
                        break;
                }
                Destroy(gameObject);
            }
        }
    }
}