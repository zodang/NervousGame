using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Enums;

public class UIController : MonoBehaviour
{
    private GameManager _gameManager;
    private int highScore = 0;

    [Header("GameStartPane")]
    //[SerializeField] private GameObject gameStartPanel;
    [SerializeField] private Button startBtn;

    [Header("GamePlayPane")]
    [SerializeField] private GameObject gamePlayPanel;
    [SerializeField] private TMP_Text coinCountText;
    
    [Header("GameOverPane")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private Button restartBtn;
    [SerializeField] private Button quitBtn;
    [SerializeField] private TMP_Text finalCoinCountText;
    [SerializeField] private TMP_Text highScoreText;

    public void ChangeUIPanel(GameState state)
    {
        TurnOffAllPanel();
        
        switch (state)
        {
            /*case GameState.GameStart:
                gameStartPanel.SetActive(true);
                break;*/
            case GameState.GamePlay:
                gamePlayPanel.SetActive(true);
                break;
            case GameState.GameOver:
                ChangeCoinText();
                UpdateHighScoreText();
                gameOverPanel.SetActive(true);
                break;
        }
    }

    private void TurnOffAllPanel()
    {
        //gameStartPanel.SetActive(false);
        gamePlayPanel.SetActive(false);
        gameOverPanel.SetActive(false);
    }
    
    public void InitUI()
    {
        ChangeUIPanel(GameState.GameStart);
        quitBtn.onClick.AddListener(OnClickQuitBtn);
        restartBtn.onClick.AddListener(OnClickRestartBtn);
        // Commented out to prevent immediate state change
        OnClickStartBtn();
        highScore = PlayerPrefs.GetInt("HighScore", 0); 
    }

    private void OnClickStartBtn()
    {
        GameManager.Instance.IsGameStart = true;
        ChangeUIPanel(GameState.GamePlay);
    }

    private void OnClickRestartBtn()
    {
        Debug.Log("Restart button clicked");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnClickQuitBtn()
    {
        Debug.Log("Quit button clicked");
        Application.Quit(); // Quit the application
    }

    public void ChangeCoinText()
    {
        coinCountText.text = $"{GameManager.Instance.CoinCount}";
        finalCoinCountText.text = coinCountText.text;

        // Update high score if the current score is higher
        if (GameManager.Instance.CoinCount > highScore)
        {
            highScore = GameManager.Instance.CoinCount;
            PlayerPrefs.SetInt("HighScore", highScore); // Save high score to player preferences
            UpdateHighScoreText(); // Update high score text
        }
    }

    private void UpdateHighScoreText()
    {
        highScoreText.text = $"High Score: {highScore}";
    }
}
