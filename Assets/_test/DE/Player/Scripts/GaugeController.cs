using UnityEngine;
using UnityEngine.UI;

public class GaugeController : MonoBehaviour
{
    [SerializeField] private RectTransform needleRectTransform;
    [SerializeField] private Timer timer;

    [SerializeField] private float maxRotation = 180f; // The range of rotation from 90 to -90 degrees
    [SerializeField] private float initialRotation = 90f; // Initial rotation offset
    //[SerializeField] private float rotationSpeedMultiplier = 2f; // Multiplier to make it spin faster

    private void Start()
    {
        if (timer != null)
        {
            timer.OnGaugeChangedAction += UpdateNeedle;
        }

        // Set the initial rotation of the needle
        needleRectTransform.localRotation = Quaternion.Euler(0, 0, initialRotation);
    }

    private void UpdateNeedle(float gaugeValue)
    {
        // Assuming gaugeValue ranges from 0 to some maximum value
        float clampedValue = Mathf.Clamp01(gaugeValue / 10f); // Adjust the divisor to scale gaugeValue appropriately
        float rotation = Mathf.Clamp(initialRotation - (clampedValue * maxRotation), -90f, 90f);// Subtract to rotate from 90 to -90
        needleRectTransform.localRotation = Quaternion.Euler(0, 0, rotation); // Rotate needle based on gauge value
    }
}
