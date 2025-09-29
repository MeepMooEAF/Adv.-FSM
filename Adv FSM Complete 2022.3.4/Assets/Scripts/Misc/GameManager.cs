using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Game Speed / Physics")]
    public float gameSpeed = 1.0f;

    [Header("Death UI")]
    [SerializeField] private GameObject deathPanel; // assign in Inspector
    [SerializeField] private TMP_Text deathText;    // optional: "You Died"
    [SerializeField] private Button restartButton;  // assign in Inspector

    private bool isGameOver;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        if (deathPanel) deathPanel.SetActive(false);
        isGameOver = false;
    }

    void Start()
    {
        // Keep the original gravity setting
        Physics.gravity = new Vector3(0, -500.0f, 0);
    }

    void Update()
    {
        // Keep the original timescale control
        Time.timeScale = gameSpeed;

        if (isGameOver && Input.GetKeyDown(KeyCode.R))
            RestartLevel();
    }

    public void ShowDeathScreen()
    {
        if (isGameOver) return;
        isGameOver = true;

        if (deathPanel) deathPanel.SetActive(true);
        if (deathText) deathText.text = "You Died";
        if (restartButton)
        {
            restartButton.onClick.RemoveAllListeners();
            restartButton.onClick.AddListener(RestartLevel);
        }

        // Pause gameplay while showing the overlay
        Time.timeScale = 0f;
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f; // unpause
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // (Optional) use this when player WINS to reuse the same overlay
    public void ShowWinScreen()
    {
        if (isGameOver) return;
        isGameOver = true;

        if (deathPanel) deathPanel.SetActive(true);
        if (deathText) deathText.text = "You Win!";
        if (restartButton)
        {
            restartButton.onClick.RemoveAllListeners();
            restartButton.onClick.AddListener(RestartLevel);
        }
        Time.timeScale = 0f;
    }
}
