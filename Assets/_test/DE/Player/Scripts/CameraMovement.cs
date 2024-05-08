using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private vThirdPersonCamera _vThirdPersonCamera;
    private void LateUpdate()
    {
        // Stop following player
        if (transform.position.y < GameManager.Instance.DeathHeight)
        {
            _vThirdPersonCamera.enabled = false;
        }
    }
}
