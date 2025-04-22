using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Sinlgeton behavior
    public static GameManager instance {  get; private set; }

    [Header("Debug variables")]
    [SerializeField] private int score;
    [SerializeField] private int bestScore;

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

    public void GameOver()
    {
        // TODO trigger game over state
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
}
