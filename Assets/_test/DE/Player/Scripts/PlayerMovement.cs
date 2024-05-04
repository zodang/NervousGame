using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float swayRange = 0.0f;
    private bool _isGrounded;
    private Rigidbody _rigidbody;

    [SerializeField] private Timer _timer;
    [SerializeField] private Road _road;
    
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _timer.OnStateChangedAction += ChangePlayerMovement;
    }
    
    void Update()
    {
        Movement();
    }

    #region Movement
    private void Movement()
    {
        var horizontalInput = Input.GetAxis("Horizontal");
        var verticalInput = Input.GetAxis("Vertical");
        var moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        // 좌우로 흔들리는 효과 추가
        var swayAmount = Random.Range(-swayRange, swayRange);
        moveDirection += new Vector3(swayAmount, 0f, 0f);
        
        // player movement
        transform.Translate(moveDirection * (moveSpeed * Time.deltaTime));

        // player jump
        if (!Input.GetKeyDown(KeyCode.Space) || !_isGrounded) return;
        _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        _isGrounded = false;
    }
    

    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Road"))
        {
            _isGrounded = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("RoadSpawner"))
        {
            _road.SpawnRoad(other);
        }
    }

    #endregion
    
    private void ChangePlayerMovement(int state)
    {
        swayRange = state;
    }
    
    
}
