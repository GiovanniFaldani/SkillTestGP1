using UnityEngine;

public struct Health
{
    public int maxHealth;
    public int currentHealth;
    public bool invincible;

    public Health(int maxHealth, bool invincible)
    {
        this.maxHealth = maxHealth;
        this.currentHealth = maxHealth;
        this.invincible = invincible;
    }
}

public enum CollectibleEffects
{
    Heal,
    Invincibiliy,
    SpeedUp
}
