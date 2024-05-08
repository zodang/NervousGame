using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Enums;

public class UIController : MonoBehaviour
{
    private GameManager _gameManager;

    [Header("GameStartPane")]
    [SerializeField] private GameObject gameStartPanel;
    [SerializeField] private Button startBtn;

    [Header("GamePlayPane")]
    [SerializeField] private GameObject gamePlayPanel;
    [SerializeField] private TMP_Text coinCountText;
    
    [Header("GameOverPane")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private Button restartBtn;
    [SerializeField] private TMP_Text finalCoinCountText;

    

    public void ChangeUIPanel(GameState state)
    {
        TurnOffAllPanel();
        
        switch (state)
        {
            case GameState.GameStart:
                gameStartPanel.SetActive(true);
                break;
            case GameState.GamePlay:
                gamePlayPanel.SetActive(true);
                break;
            case GameState.GameOver:
                ChangeCoinText();
                gameOverPanel.SetActive(true);
                break;
        }
    }

    private void TurnOffAllPanel()
    {
        gameStartPanel.SetActive(false);
        gamePlayPanel.SetActive(false);
        gameOverPanel.SetActive(false);
    }
    
    public void InitUI()
    {
        ChangeUIPanel(GameState.GameStart);
        startBtn.onClick.AddListener(OnClickStartBtn);
        restartBtn.onClick.AddListener(OnClickRestartBtn);
    }

    private void OnClickStartBtn()
    {
        GameManager.Instance.IsGameStart = true;
        ChangeUIPanel(GameState.GamePlay);
    }

    private void OnClickRestartBtn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ChangeCoinText()
    {
        coinCountText.text = $"Score : {GameManager.Instance.CoinCount}";
        finalCoinCountText.text = coinCountText.text;
    }
}
