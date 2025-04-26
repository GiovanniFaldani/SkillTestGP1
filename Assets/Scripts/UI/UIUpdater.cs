using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UIUpdater : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI healthDisplay;
    [SerializeField] TextMeshProUGUI scoreDisplay;
    [SerializeField] TextMeshProUGUI collected;

    private Player player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindFirstObjectByType(typeof(Player)).GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        healthDisplay.text = "HP: " + player.GetCurrentHP();
        scoreDisplay.text = "Score: " + GameManager.instance.score;
        collected.text = "Collectibles: " + GameManager.instance._collectibleCount + "/" + GameManager.instance._collectibleTotal;
    }
}
