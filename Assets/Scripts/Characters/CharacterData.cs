using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "GameData/CharacterData")]
public class CharacterData : ScriptableObject
{
    public int maxHealth;
    public float moveSpeed;
    public int damage;
    public RuntimeAnimatorController animController;
}
