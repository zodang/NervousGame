using Enums;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    #region Variables

    [SerializeField] private UIController UIController;
    
    public bool IsGameStart;

    [HideInInspector] public int DeathHeight = -10;
    private bool _isGameOver;
    public bool IsGameOver
    {
        get
        {
            return _isGameOver;
        }
        set
        {
            if (value)
            {
                UIController.ChangeUIPanel(GameState.GameOver);
            }
        }
    }
    
    private int _coinCount = 0;
    public int CoinCount
    {
        get
        {
            return _coinCount;
        }
        set
        {
            _coinCount = value;
            UIController.ChangeCoinText();
        }
    }

    #endregion
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
    }

    private void Start()
    {
        InitGame();
        UIController.InitUI();
    }

    private void InitGame()
    {
        IsGameStart = false;
        IsGameOver = false;
        CoinCount = 0;
    }
    
}
