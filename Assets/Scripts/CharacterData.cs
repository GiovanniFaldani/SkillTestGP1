using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "CharacterData")]
public class CharacterData : ScriptableObject
{
    public int maxHealth;
    public float moveSpeed;
    public int damage;
    public AnimatorController animController;
}
