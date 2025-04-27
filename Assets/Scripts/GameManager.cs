using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Sinlgeton behavior
    public static GameManager instance {  get; private set; }

    [SerializeField] TextMeshProUGUI healthDisplay;
    [SerializeField] TextMeshProUGUI scoreDisplay;
    [SerializeField] TextMeshProUGUI collected;

    [Header("Debug variables")]
    [SerializeField] public int score;
    [SerializeField] public int bestScore;

    public int _collectibleTotal;
    public int _collectibleCount = 0;
    public bool _paused;

    private Player player;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(this.gameObject);
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
        // Get player 
        player = FindFirstObjectByType(typeof(Player)).GetComponent<Player>();
    }

    // Update UI
    void Update()
    {
        healthDisplay.text = "HP: " + player.GetCurrentHP();
        scoreDisplay.text = "Score: " + score;
        collected.text = "Collectibles: " + _collectibleCount + "/" + _collectibleTotal;
        CheckForPause();
    }

    public void GameOver()
    {
        // TODO trigger game over state
        Debug.Log("Game over!");
        UIUpdater.instance.DisplayGameOverScreen();
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

    public void CheckForPause()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (!_paused)
            {
                _paused = true;
                UIUpdater.instance.DisplayPauseMenu();
            }
            else
            {
                UIUpdater.instance.UnPause();
                _paused = false;
            }
        }
    }

    public void WinGame()
    {
        // TODO display win screen
        Debug.Log("Victory!");
        UIUpdater.instance.DisplayWinScreen();
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
