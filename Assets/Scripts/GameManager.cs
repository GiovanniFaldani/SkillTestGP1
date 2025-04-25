using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Sinlgeton behavior
    public static GameManager instance {  get; private set; }

    [Header("Debug variables")]
    [SerializeField] private int score;
    [SerializeField] private int bestScore;

    private int _collectibleTotal;
    private int _collectibleCount = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        // Initialize values
        score = 0;
        if (PlayerPrefs.HasKey("BestScore"))
        {
            bestScore = PlayerPrefs.GetInt("BestScore");
        }
    }

    public void Start()
    {
        // Enumerate number of colectibles for victory
        _collectibleTotal = FindObjectsByType<Collectible>(FindObjectsSortMode.None).Length;
    }

    public void GameOver()
    {
        // TODO trigger game over state
        Debug.Log("Game over!");
    }

    public void AddToScore(int scoreToAdd)
    {
        score += scoreToAdd;
        if (score > bestScore)
        {
            bestScore = score;
        }
    }

    public void SaveBestScore(int newBestScore)
    {
        if (newBestScore > bestScore)
        {
            bestScore = newBestScore;
            PlayerPrefs.SetInt("BestScore", bestScore);
        }
    }

    public void WinGame()
    {
        // TODO display win screen
    }

    public void AddCollectible()
    {
        _collectibleCount++;
        if(_collectibleCount == _collectibleTotal)
        {
            WinGame();
        }
    }
}
