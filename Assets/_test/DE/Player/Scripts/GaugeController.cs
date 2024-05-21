using UnityEngine;
using UnityEngine.UI;

public class FillGaugeController : MonoBehaviour
{
    public Image gaugeFill; // Reference to the UI Image component that represents the fill

    public float swayDuration = 5.0f; // Duration in seconds for one complete sway cycle
    private float elapsedTime = 0.0f;

    void Start()
    {
        if (gaugeFill == null)
        {
            Debug.LogError("Gauge Fill image is not assigned.");
        }
    }

    void Update()
    {
        // Update the elapsed time
        elapsedTime += Time.deltaTime;

        // Calculate the fill amount based on elapsed time and sway duration
        float fillAmount = Mathf.PingPong(elapsedTime / swayDuration, 1.0f);

        // Update the UI fill amount
        gaugeFill.fillAmount = fillAmount;
    }
}
