using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] private CollectibleData collectibleData;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            switch (collectibleData.effect)
            {
                case CollectibleEffects.Heal:
                    player.AddToHealth(collectibleData.healAmount);
                    break;
                case CollectibleEffects.SpeedUp:
                    player.ModifySpeed(collectibleData.speedBonus, collectibleData.duration);
                    break;
            }
            GameManager.instance.AddToScore(collectibleData.score);
            Destroy(this.gameObject);
        }
    }
}
