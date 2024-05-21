using System.Collections;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private vThirdPersonCamera _vThirdPersonCamera;

    private void Update()
    {
        // Stop following player
        if (transform.position.y < GameManager.Instance.DeathHeight)
        {
            _vThirdPersonCamera.enabled = false;
        }

        Shake(Timer.Instance.forwardTimer * 0.01f);

    }

    Vector3 originPos;

    void Start()
    {
        originPos = transform.localPosition;
    }

    public IEnumerator Shake(float _amount, float _duration)
    {
        float timer = 0;
        while (timer <= _duration)
        {
            transform.localPosition = (Vector3)Random.insideUnitCircle * _amount + originPos;

            timer += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originPos;
    }

    public void Shake(float amount)
    {
        transform.localPosition += (Vector3)Random.insideUnitCircle * amount;
        if (amount == 0)
        {
            //transform.localPosition = originPos;
        }
    }
}