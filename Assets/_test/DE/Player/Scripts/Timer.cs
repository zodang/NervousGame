using System;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public static Timer Instance;

    [Header("Timer")] 
    private float _timer;
    private float _timerCycle = 0f;
    [SerializeField] private float cycle = 2f;
    public float forwardTimer = 0f;

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

    [Header("UI Elements")]
    [SerializeField] private Image gaugeFill; // Reference to the UI Image component for the fill gauge

    [Header("Settings")]
    [SerializeField] private float maxSwayGauge = 100f; // The maximum value of the sway gauge
    [SerializeField] private float swayMultiplier = 10f; // Multiplier to control the speed of sway gauge increase

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
        UpdateGaugeFill();
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
            swayGauge += Time.deltaTime * swayMultiplier; // Increase multiplier to make it increase faster

            if (swayGauge > maxSwayGauge)
            {
                swayGauge = maxSwayGauge; // Clamp the sway gauge to its max value
            }

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
            forwardTimer = 0;
            currentState = 0;
            swayGauge = 0; // Reset the sway gauge when the key is released
        }
    }

    private void UpdateGaugeFill()
    {
        if (gaugeFill != null)
        {
            // Normalize swayGauge to [0, 1] range
            gaugeFill.fillAmount = Mathf.Clamp01(swayGauge / maxSwayGauge);
        }
    }
}
