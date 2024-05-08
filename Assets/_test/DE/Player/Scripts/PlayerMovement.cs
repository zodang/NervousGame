using System;
using Invector.vCharacterController;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerMovement: MonoBehaviour
{
    [SerializeField] private vThirdPersonInput _vThirdPersonInput;
    [SerializeField] private Road _road;
    
    void LateUpdate()
    {
        // Disable Player Movement
        if (!GameManager.Instance.IsGameStart || GameManager.Instance.IsGameOver)
        {
            _vThirdPersonInput.enabled = false;
            return;
        }
        
        // Enable Player Movement
        _vThirdPersonInput.enabled = true;
        
        // Check if Player is Dead
        CheckGameOver();
    }
    
    private void CheckGameOver()
    {
        if (transform.position.y < GameManager.Instance.DeathHeight)
        {
            GameManager.Instance.IsGameOver = true;
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        // Spawn New Road
        if (other.gameObject.CompareTag("RoadSpawner"))
        {
            _road.SpawnRoad(other);
        }

        // Coin System
        if (other.gameObject.CompareTag("Coin"))
        {
            GameManager.Instance.CoinCount++;
            Destroy(other.gameObject);
        }
    }
}
