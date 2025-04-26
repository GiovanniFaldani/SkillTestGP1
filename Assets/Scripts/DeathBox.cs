using UnityEngine;

public class DeathBox : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy") || collision.CompareTag("Ally") || collision.CompareTag("Player")){
            collision.GetComponent<Character>().TakeDamage(999); // certain kill
        }
    }
}
