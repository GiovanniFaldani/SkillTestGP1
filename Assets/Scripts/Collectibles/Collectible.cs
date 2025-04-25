using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] private CollectibleData collectibleData;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerAction"))
        {
            Player player = other.GetComponentInParent<Player>();
            switch (collectibleData.effect)
            {
                case CollectibleEffects.Heal:
                    player.AddHealth(collectibleData.healAmount);
                    break;
                case CollectibleEffects.Invincibiliy:
                    player.SetInvincibile(true, collectibleData.duration);
                    break;
                case CollectibleEffects.SpeedUp:
                    player.ModifySpeed(collectibleData.speedBonus, collectibleData.duration);
                    break;
            }
            GameManager.instance.AddToScore(collectibleData.score);
            GameManager.instance.AddCollectible();
            Destroy(this.gameObject);
        }
    }
}
