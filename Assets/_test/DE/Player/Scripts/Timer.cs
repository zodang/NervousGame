using System;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public static Timer Instance;

    [Header("Timer")] 
    private float _timer;
    private float _timerCycle = 0f;
    [SerializeField] private float cycle = 2f;
    [SerializeField] private float forwardTimer = 0f;

    [Header("State")]
    private int _currentState;
    public int currentState
    {
        get { return _currentState; }
        set
        {
            _currentState = value;
            OnStateChangedAction?.Invoke(_currentState);
        }
    }
    
    public event Action<int> OnStateChangedAction;

    [Header("Gauge")]
    private float _swayGauge;
    public float swayGauge
    {
        get { return _swayGauge; }
        set
        {
            _swayGauge = value;
            OnGaugeChangedAction?.Invoke(_swayGauge);
        }
    }
    
    public event Action<float> OnGaugeChangedAction;

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

    private void Update()
    {
        GameTimer();
    }

    private void GameTimer()
    {
        if (Input.GetKey(KeyCode.W))
        {
            // Start Timer
            _timer += Time.deltaTime;
            _timerCycle += Time.deltaTime;
            
            forwardTimer = (int)_timer % 60;

            // Increase the sway gauge based on how long the key is pressed
            swayGauge += Time.deltaTime * 5; // Increase multiplier to make it spin faster

            if (_timerCycle >= cycle)
            {
                Debug.Log($"@@DE ---> {cycle} Seconds elapsed");
                _timerCycle = 0f;
                currentState++;
            }
        }
        else
        {
            // Stop Timer
            _timer = 0;
            currentState = 0;
            swayGauge = 0; // Reset the sway gauge when the key is released
        }
    }
}
