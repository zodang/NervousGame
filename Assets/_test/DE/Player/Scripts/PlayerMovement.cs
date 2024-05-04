using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float swayRange = 0.0f;
    private bool _isGrounded;
    private Rigidbody _rigidbody;

    [Header("Timer")]
    private float _timer;
    [SerializeField] private float forwardTimer;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Timer();
        Movement();
    }
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
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = true;
        }
    }

    private void Timer()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            _timer += Time.deltaTime;
            forwardTimer = (int)_timer % 60;
        }
        else
        {
            forwardTimer = 0;
        }
    }
    
    
}
