using UnityEngine;

public struct Health
{
    public int maxHealth;
    public int currentHealth;
    public bool invincibe;

    public Health(int maxHealth, bool invincible)
    {
        this.maxHealth = maxHealth;
        this.currentHealth = maxHealth;
        this.invincibe = invincible;
    }
}

public enum CollectibleEffects
{
    Heal,
    SpeedUp
}
