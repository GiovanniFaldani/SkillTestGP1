using UnityEngine;


[CreateAssetMenu(fileName = "CollectibleData", menuName = "GameData/CollectibleData")]
public class CollectibleData : ScriptableObject
{
    public string collectibleName;
    public int score;
    public CollectibleEffects effect;
    public int healAmount;
    public float duration;
    public float speedBonus;
}
