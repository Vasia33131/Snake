using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI finalScoreText;
    [SerializeField] private Button restartButton;

    private int score = 0;
    private bool isGameOver = false;

    private void Start()
    {
        AudioManager.Instance.PlayBackgroundMusic();
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        gameOverPanel.SetActive(false);
        restartButton.onClick.AddListener(RestartGame);
    }

    public void AddScore(int points)
    {
        if (isGameOver) return;

        score += points;
        scoreText.text = $"����: {score}";
    }

    public void GameOver()
    {
        if (isGameOver) return; // ������ �� ���������� ������

        isGameOver = true;
        finalScoreText.text = $"����: {score}";
        gameOverPanel.SetActive(true);

        var snakeMovements = FindObjectsOfType<SnakeKeyboardInputHandler>();
        foreach (var movement in snakeMovements)
        {
            movement.enabled = false;
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0f; // ������������� �����
        AudioListener.pause = true; // ��������� ����� (�����������)
    }

    private void ResumeGame()
    {
        Time.timeScale = 1f; // ������������ �����
        AudioListener.pause = false; // �������� ����� �������
    }

    private void RestartGame()
    {
        ResumeGame(); // ������� ����� ����� �������������
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }
}